
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class Dialog: MonoBehaviour {
	
	
	static Hashtable variables= new Hashtable();
	private Hashtable locals= new Hashtable();
	
	private int areaHeight= 180;
	private int areaWidth= 450;
	private SelectionAnswers answers;
	
	public string characterName= "";
	public string filePath= "";
	public List<DialogNode> nodes;
	public int startNode;
	
	//Data object used for loading/saving
	public class DialogData {
		public	string characterName= "";
		public	List<DialogNode> nodes;
		public	int startNode;
	}
	
	//DialogData dialogData;
	
	public Vector3 nametagOffset;
	public float maximumDistance = 2.0f;
	
	public bool allowMouseSelection= true;
	
	
	public string[] registeredTargets= new string[0];
	public int registeredTargetsSize= 0;
	public bool foldoutTargets= false;
	
	Dialog[] registeredTargetsGO;
	
	
	private int currentNode;
	private ChoiceController choice;
	private int selectionGridInt= 0;
	private DialogNode node;
	private string text;
	
	private GameObject player;
	
	private Regex reg= new Regex("\\[([a-zA-Z0-9_]*)\\]");
	private Regex reg2= new Regex("\\{(?<content>[^\\{\\}|]*)(\\|(?<content>[^\\{\\}|]*))*\\}", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
	//private Regex reg3= new Regex("[ ]*([a-zA-Z0-9_]*)\\(([^\\(\\)]*)\\)[ ]*");
	
	public enum DialogNodeType {
		Text = 0,
		Choice = 1,
		Answer = 2,
		Switch = 3,
		Case = 4,
		Script = 5,
		Variable = 6,
		Pass = 7
	}
	
	public class Answer {
		public int answerID;
		public Rect editorRect;
		public int[] nodesOut;
		public	string text;
		public	bool  enabled;
		public	string showIf;
	}
	
	public	class Case {
		public	int caseID;
		public	Rect editorRect;
		public	int[] nodesOut;
		public	int method;
		public	float caseValue;
		public bool  enabled;
	}
	
	public class DialogNode {
		//Saves the node's position in the editor window
		public Rect editorRect;
		
		public int nodeID;
		public int[] nodesIn;
		public int[] nodesOut;
		
		public Answer[] answers;
		public Case[] cases;
		
		public string variable;
		public int method;
		
		//Reference to this node's target
		//(ie the character that's supposed to be talking)
		public int target;
		//Text content of this node
		public string text;
		
		
		//Disabled nodes are ignored in branching
		public bool  enabled = true;
		
		//Nodes that are set to 'pass' directly continue with the
		//following nodes and are not displayed. This is useful for
		//nodes with special functions.
		public 	bool  pass = false;
		
		//The type of this node (ie 'Text', 'Choice', etc.)
		public DialogNodeType type= DialogNodeType.Text;
		
	}
	
	public	bool LoadDialog (){
		if(filePath!=null && filePath != "") {
			DialogData dialogData= DialogIO.Load(filePath, characterName);
			nodes = dialogData.nodes;
			characterName = dialogData.characterName;
			startNode = dialogData.startNode;
			return true;
		} else
		{	
			// Auto-generate default file path
			characterName = gameObject.name;	
			//		 DialogIO.SetCharacterName(characterName); 
			filePath = "Dialogs/"+ gameObject.name+ ".xml";
			return false;
		}
	}
	
	void  Start (){
		//	if(LoadDialog()) {
		LoadDialog();
		//		DialogData dialogData= DialogIO.Load(filePath);
		//		
		//		nodes = dialogData.nodes;
		//		characterName = dialogData.characterName;
		//		startNode = dialogData.startNode;
		
		player = GameObject.FindGameObjectWithTag("Player");
		
		currentNode = startNode;
		
		if(nodes!=null && nodes.Count > 0) {
			node = GetNodeByID(currentNode);
			//	GetText(node.text);
		}
		
		//Set registered targets default values
		if(registeredTargets == null || registeredTargets.Length < 2) {
			registeredTargetsSize = 2;
			registeredTargets = new string[2];
		}
		
		
		
		registeredTargets[0] = player.name;
		registeredTargets[1] = gameObject.name;
		registeredTargetsGO = new Dialog[registeredTargets.Length];
		
		//Get registered targets' dialog component
		for(int i= 0; i<registeredTargets.Length; i++) {
			registeredTargetsGO[i] = GameObject.Find(registeredTargets[i]).GetComponent<Dialog>();
		}
		
		if(!locals.Contains("counter"))
			locals["counter"] = 0;
		//}
		
		
	}
	
	
	public	void  Show (){
		
		//Get the name string for the name tag
		string name= registeredTargetsGO[node.target].characterName;
		
		GUIStyle frameStyle= GUI.skin.GetStyle("dialogframe");
		GUIStyle boxStyle= GUI.skin.GetStyle("box");	
		int textWidth= areaWidth - frameStyle.padding.left - frameStyle.padding.right;
		
		int newHeight= 0;
		
		GUIContent textContent= new GUIContent(text);	
		Rect textRect= GUILayoutUtility.GetRect(textContent, boxStyle, GUILayout.Width(textWidth));
		if(node.text != null && node.text != "")
			newHeight += (int)(Mathf.Ceil(textRect.height));
		else
			textRect.height = 0;
		
		newHeight += frameStyle.padding.top + frameStyle.padding.bottom;
		
		int answersHeight= 0;
		
		if(node.type == DialogNodeType.Choice) { 
			answers = new SelectionAnswers(textWidth, choice, "choice", "choiceactive");
			answersHeight = answers.GetHeight();
			newHeight += answersHeight;
		}
		
		areaHeight = newHeight; 
		
		//Calculate position on screen
		if(areaHeight < 180) {
			areaHeight = 180;
			textRect.height = 180 - frameStyle.padding.top - frameStyle.padding.bottom - answersHeight;
		}
		
		float ScreenX= ((Screen.width * 0.5f) - (areaWidth * 0.5f));
		float ScreenY= ((Screen.height * 0.5f) - (areaHeight * 0.5f))*1.5f;
		
		//Frame of the dialog
		GUI.Box( new Rect(ScreenX,ScreenY,areaWidth,areaHeight), "", frameStyle); 
		//Text of the dialog
		if(text != null && text != "")
			GUI.Box( new Rect(ScreenX+frameStyle.padding.left, ScreenY+frameStyle.padding.top, textRect.width, textRect.height), text, boxStyle);
		//Answer options
		if(node.type == DialogNodeType.Choice) {
			answers.Draw( new Rect(
				ScreenX+frameStyle.padding.left,
				ScreenY+frameStyle.padding.top+textRect.height+boxStyle.margin.top+boxStyle.margin.bottom,
				textRect.width,
				answersHeight
				)
			             );
		}	
		
		//Nametag
		GUIContent nametagContent= new GUIContent(name);
		Rect nametagRect= GUILayoutUtility.GetRect(nametagContent, "nametag");
		
		GUI.Box( new Rect(ScreenX-nametagRect.width*0.25f, ScreenY-nametagRect.height*0.5f, nametagRect.width, nametagRect.height), name, "nametag");
	}
	
	//Return a list of all node-ID that are enabled
	public	int[] GetEnabledNodes ( int[] nodes  ){
		int[] enabledNodesOut = null;
		if(nodes != null) {
			int[] tmpEnabledNodesOut= new int[nodes.Length]; 
			int answerCount= 0;
			for(int i=0; i<nodes.Length; i++) {
				DialogNode outNode= GetNodeByID(nodes[i]);
				if(outNode.enabled) {
					tmpEnabledNodesOut[answerCount] = nodes[i]; 
					answerCount++;
				}
			} 
			enabledNodesOut = new int[answerCount];
			for(int j=0; j<answerCount; j++) {
				enabledNodesOut[j] = tmpEnabledNodesOut[j];
			}
		}
		return enabledNodesOut;
	}
	
	//Browse up in the list of possible answer if it's a choice node
	//otherwise skip to the next node
	public	void  BrowseUp (){
		if(node.type == DialogNodeType.Choice) {
			choice.BrowseUp();
		} else {
			NextNode();
		}
	}
	
	//Browse down in the list of possible answer if it's a choice node
	//otherwise skip to the next node
	public	void  BrowseDown (){
		if(node.type == DialogNodeType.Choice) {
			choice.BrowseDown();
		} else {
			NextNode();
		}
	}
	
	public	void  GetText ( string nodeText  ){
		text = "";
		if(nodeText != null) {
			text = nodeText;
			int index= 0;
			int length= 0;
			
			//Create optional text content by writing "...{ optional text} ..." to have text
			//that will be shown with a 50:50 chance or by writing "...{ option A| option B| option C} ..."
			//to have one of the text options (separated by an "|") inserted.
			//You could also, for instance, write "... {{A|B|[variable]} }..." to have a 50:50 chance of inserting either
			//A, B or a variable or skip it instead.
			Match m= reg2.Match(text);
			while(m.Success) {
				string content= "";
				int count= m.Groups["content"].Captures.Count;
				if(count > 1) {
					content = m.Groups["content"].Captures[Random.Range(0, count)].Value;
				} else if (Random.value > 0.5f) {
					content = m.Groups["content"].Captures[0].Value;
				}
				
				index = m.Groups[0].Index;
				length = m.Groups[0].Length;
				text = text.Substring(0, index) + content + text.Substring(index+length, text.Length-index-length);
				m = reg2.Match(text);
			}
			
			//Replace variables in the text (i.e. "... [money] ...") with
			//variables from the static hashtable
			m = reg.Match(text);
			while(m.Success) {
				string key= m.Groups[1].Value;
				index = m.Groups[0].Index;
				length = m.Groups[0].Length;
				text = text.Substring(0, index) + GetVariable(key).ToString() + text.Substring(index+length, text.Length-index-length);
				m = reg.Match(text, index);
			}
		}
		
	}
	
	public	void  Load ( int id  ){
		currentNode = id;
		node = GetNodeByID(currentNode);
		
		//If it's a Script node execute function via SendMessage and the node's parameters
		if (node.type == DialogNodeType.Script)
			ProcessScriptNode();
		//If it's a Variable node do the wished operation ("=", "+", "-")
		else if (node.type == DialogNodeType.Variable)
			ProcessVariableNode();
		if(node.type == DialogNodeType.Choice) {
			choice = new ChoiceController(node.answers, allowMouseSelection);
		}
		
		if(node.pass)
			NextNode();
		else if(!node.enabled)
			Exit();
		else
			GetText(node.text);
	} 
	
	public	void  NextNode (){
		//Get the id of the next node
		int nextID= GetNextNode();
		//Exit the dialog, if no next node was found...
		//...or load the next node
		if(nextID < 0) {
			Exit();
		} else {
			Load(nextID);
		}
	}
	
	public	int GetNextNode (){
		int nextNode= -1;
		
		if (node.type == DialogNodeType.Choice) {
			nextNode = GetRandomNode(choice.answers[choice.selectedAnswer].nodesOut);
			selectionGridInt = 0;
		} else if (node.type == DialogNodeType.Switch) {
			nextNode = GetSwitchedNode();
		} else if(node.nodesOut != null) {
			nextNode = GetRandomNode(node.nodesOut);
		}
		
		return nextNode;
	} 
	
	public	int GetRandomNode ( int[] nodes  ){
		int randomNode= -1;
		int[] enabledNodes= GetEnabledNodes(nodes);
		if(enabledNodes!=null && enabledNodes.Length > 0)
			randomNode = enabledNodes[Random.Range(0,enabledNodes.Length)];
		return randomNode;
	}
	
	public	void  Exit (){
		int count = (int)locals["counter"];
		locals["counter"] = ++count;
		player.GetComponent<DialogController>().activeDialog = null;
	}
	
	public	int GetSwitchedNode (){
		int nextNode= -1;
		string key= node.variable;
		
		bool exists= ExistVariable(key);
		float x=0f;
		if (exists) {
			x = GetVariable(key);
		}
		
		float val;
		Case c;
		int method;
		
		if(node.cases != null) {
			for(int j=0; j<node.cases.Length; j++) {
				c = node.cases[j];
				if(c.enabled) {
					method = c.method;
					val = c.caseValue;
					if(
						(exists && (
						method == 0 && x > val ||
						method == 1 && x < val ||
						method == 2 && x == val ||
						method == 3 && x != val
						)) || (!exists && (method == 4
						))) {
						nextNode = GetRandomNode(c.nodesOut);
						break;
					}
				}
			}
		}
		
		return nextNode;
	}
	
	public	void  ProcessScriptNode (){
		registeredTargetsGO[node.target].SendMessage(node.variable, node.text);
	}
	
	public	void  ProcessVariableNode (){
		int method= node.method;
		
		string input= node.text;
		float inputFloat=0f;
		
		float.TryParse(input,out inputFloat);
		
		bool  isLocal = true;
		
		string[] s =  node.variable.Split("."[0]);
		string name="";
		string prefix="";
		
		if(s.Length > 1) {
			name = s[s.Length-1];
			prefix = s[0];
		} else {
			name = s[0];
		}
		
		if(prefix!=null && prefix != "local")
			isLocal = false;
		
		if(!ExistVariable(name))
			SetVariable(isLocal, name, inputFloat);
		
		float variable = GetVariable(name);
		
		float y = 0.0f;		
		//	if(method == 0) //=
		//		y = 0;
		if(method == 1) //+
			y = inputFloat;
		else if(method == 2) //-
			y = -inputFloat;
		SetVariable(isLocal, name, variable+y);
		
	}
	
	public	void  SetVariable ( bool local ,    string name ,   float x  ){
		if(local)
			locals[name] = x;
		else
			Dialog.variables[name] = x;
	}
	
	// Check if exist a variable
	public	bool ExistVariable ( string key  ){
		Object val;
		float variable = 0.0f;
		
		if(locals.Contains(key))
			return true;
		else if(Dialog.variables.Contains(key))
			return true;
		
		return false;
	}
	
	//Get a variable from the static variables-hashtable and
	//(if it can't find one, create a new entry)
	public	float GetVariable ( string key  ){
		object val=null;
		float variable = 0.0f;
		
		if(locals.Contains(key))
			val = locals[key];
		else if(Dialog.variables.Contains(key))
			val = Dialog.variables[key];
		//	else {
		//		Dialog.variables.Add(key, 0.0f);
		//	}
		
		
		float.TryParse(val.ToString(),out variable);
		return variable;
	}
	
	public DialogNode GetNodeByID ( int id  ){
		DialogNode foundNode=null;
		for(int i= 0; i<nodes.Count; i++) {
			if(nodes[i].nodeID == id) {
				foundNode = nodes[i];
				break;
			}
		}
		return foundNode;
	}
	
	//Class for handeling choice nodes
	public	class ChoiceController {
		public Answer[] answers;
		Answer[] ad ;
		public int selectedAnswer;
		public bool  allowMouseSelection;
		
		public ChoiceController ( Answer[] answerList ,   bool mouseSelection  ){
			answers = GetEnabledAnswers(answerList);
			selectedAnswer = 0;
			allowMouseSelection = mouseSelection;
		}
		
		Answer[] GetEnabledAnswers ( Answer[] ans  ){
		
			List<Answer> tmp = new List<Answer>();
			for(int i=0; i<ans.Length; i++) {
				if(ans[i].enabled)
					tmp.Add(ans[i]);
			}
			 ad= tmp.ToArray();
			return ad;
		}
		
		public void  BrowseUp (){
			selectedAnswer--;
			if(selectedAnswer<0)
				selectedAnswer += answers.Length;
		}
		
		public void  BrowseDown (){
			selectedAnswer++;
			selectedAnswer=selectedAnswer%answers.Length;
		}
		
		public int GetCount (){
			return answers.Length;
		}
	}
	
	//Class to draw a choice node's answer options and allow mouse selection
	public	class SelectionAnswers {
		Rect[] rects;
		string choiceStyle;
		string choiceStyleActive;
		Rect answersRect;
		int textWidth;
		
		ChoiceController controller;
		
		
		public SelectionAnswers ( int width , ChoiceController choiceController,  string style ,   string styleActive  ){
			choiceStyle = style;
			choiceStyleActive = styleActive;
			textWidth = width;
			controller = choiceController;
			CalculateRects();
		}
		
		public void  CalculateRects (){
			rects = new Rect[controller.answers.Length];
			for(int i=0; i<controller.answers.Length; i++) {
				GUIContent answerContent= new GUIContent(controller.answers[i].text);
				rects[i] = GUILayoutUtility.GetRect(answerContent, choiceStyle, GUILayout.Width(textWidth));
			}
		}
		
		public int GetHeight (){
			int height= 0;
			for(int i=0; i<controller.answers.Length; i++) {
				height += (int)(Mathf.Ceil(rects[i].height));
			}
			return height;
		}
		
		
		public void Draw( Rect rect  ){
			int selectedAnswer= controller.selectedAnswer;
			int height= 0;
			
			for(int i=0; i<controller.answers.Length; i++) {
				Rect boxRect= new Rect(rect.x, rect.y+height, textWidth, rects[i].height);
				height += (int)(Mathf.Ceil(rects[i].height));
				
				//If this answer option is the selected one, draw a box
				//using the "active" style
				if(i==selectedAnswer)
					GUI.Box(boxRect, controller.answers[i].text, choiceStyleActive);
				else
					GUI.Box(boxRect, controller.answers[i].text, choiceStyle);
				
				//If we allow mouse selection check if the mouse is positioned
				//above this answer option and change the selected answer, if it is
				if(controller.allowMouseSelection &&
				   Event.current.type == EventType.Repaint &&
				   boxRect.Contains(Event.current.mousePosition)) {
					controller.selectedAnswer = i;
				}
			}
		}	
	}
	
	public	void  Print ( string msg  ){
		Debug.Log(msg);
	}
	
	public	void  Log ( string key  ){
		Debug.Log(key+": "+GetVariable(key));
	}
}
