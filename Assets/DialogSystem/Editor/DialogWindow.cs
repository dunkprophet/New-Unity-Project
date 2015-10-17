using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class DialogWindow : EditorWindow {
			
		static DialogWindow instance;
		
		private int highestID= 0;
		
		//The strings for the popup menu to create nodes
		private string[] nodeTypes= {"Text", "Choice", "Script", "Switch", "Variable", "Pass"};
		
		private Dialog selectedDialog;
		private DialogEditor selectedEditor;
		
		private Dialog.DialogData dialogData;
		
		private List<NodeWindow>  windows = new List<NodeWindow>();  
		private List<Connection> connections = new List<Connection>();
		
		private int selectedOut;
		private int selectedIn;
		
		private int startNode;
		
		private string characterName;
		
		private string[] registeredTargets;
		
		public DialogWindow (){
			instance = this;
		}
		
		public class NodeWindow : Dialog.DialogNode {
		
		public string text;
				
		public string variable; 
		public	 int methodInt; 
				
		public	 string windowName= "";
		public	 int windowID;
		public	 int nodeID	;
		public	 int target= 0;
				
		public	 	string[] param;
				
		public	 bool enabled;
		public	 	bool  pass;
			
					
		public	 Dialog.DialogNodeType type;
		public	 int state;
				
		public	 int width= 150;
		public	 int height= 150;
				
		public	 int o= 15;		//offset
		public	 int m= 5;		//margin
		public	 int order=0	;
		public	 Rect position;
				
		public	 Rect rect;
			
		public virtual Dialog.DialogNode Save (){
				
				var node = new Dialog.DialogNode();
				node.nodeID = windowID;
				node.type = type;
				node.target = target;
				node.text = text;
				//			node.param = param;
				node.enabled = enabled;
				node.pass = pass;
				node.variable = variable; 
				node.method = methodInt;
				
				node.editorRect = position; 
				
				
				//Create references to in- and out-nodes from
				//the connections saved in 'connections'
				List<int> tmp1= new List<int>();
				List<int> tmp2= new List<int>();
				var connections = DialogWindow.instance.connections;
				for(int j= 0; j < connections.Count; j++) {
					if(connections[j].nodeOut == windowID)
						tmp1.Add(connections[j].nodeIn);
					else if(connections[j].nodeIn == windowID)
						tmp2.Add(connections[j].nodeOut);
				}
				node.nodesOut = tmp1.ToArray();
				node.nodesIn = tmp2.ToArray();
					
				return node;
			}
			
			public int[] GetNodesOut (){
				List<int>  tmp= new List<int>();
			Connection[] tmpConn = new Connection[ DialogWindow.instance.connections.Count];
			DialogWindow.instance.connections.CopyTo(tmpConn);
			Connection[] connections =tmpConn;

				for(int j= 0; j < connections.Length; j++) {
					if(connections[j].nodeOut == windowID)
						tmp.Add(connections[j].nodeIn);
				}
				
			return tmp.ToArray();
			
			}
			
			public virtual void CreateNodeWindow (int _windowID){
				
			}
			
			
			public Color  CreateColor (  int r ,   int g ,   int b  ){
				return new Color(r* 0.00392156862745f, g* 0.00392156862745f,b* 0.00392156862745f,1);
			}
			
			
			public  Rect Show (){
			if (type == Dialog.DialogNodeType.Script)
					GUI.color = Color.green;
					
			else if (type == Dialog.DialogNodeType.Variable)
					GUI.color = CreateColor( 161, 134, 190);
			else if (type == Dialog.DialogNodeType.Switch)
					GUI.color = CreateColor( 102, 184, 255);
			else if (type == Dialog.DialogNodeType.Case)
					GUI.color = CreateColor( 40, 155, 255);			
			else if (type == Dialog.DialogNodeType.Text)			
					GUI.color = Color.grey;
			else if (type == Dialog.DialogNodeType.Choice)			
					GUI.color = CreateColor( 243, 131, 76);
				else if (type == Dialog.DialogNodeType.Answer)			
					GUI.color = CreateColor( 255, 83, 0);				
				else
					GUI.color = CreateColor( 255, 255, 255);	
		
			
			return GUI.Window (windowID,new Rect(position.x, position.y, width, height), CreateNodeWindow, windowName);
			}
			
			public bool StandardNodeHead ( bool en  ){
				enabled= en;
				if(GUI.Button(new Rect(m, m+o, 45, 20),"In"))
					DialogWindow.instance.SetIn(windowID);				
				enabled = GUI.Toggle( new Rect(45+m*2,m+o, 15, 20),enabled, "", "toggle");
				if(GUI.Button( new Rect(60+m*3, m+o, 20, 20),"X"))
					DialogWindow.instance.DeleteNode(windowID);
				if(GUI.Button( new Rect(80+m*4, m+o, 45, 20),"Out"))
					DialogWindow.instance.SetOut(windowID);
				return enabled;
			}
			
			public bool ParentNodeHead ( bool en , Dialog.DialogNodeType childType  ){
			
				float w= width-2*m+o;
				float h= height -2*m-o;
				enabled= en;
				if(GUI.Button( new Rect(m, m+o, 35, 20),"In"))
					DialogWindow.instance.SetIn(windowID);				
				enabled = GUI.Toggle( new Rect(35+m*2, m+o, 15, 20),enabled, "", "toggle");
				if(GUI.Button( new Rect(50+m*3, m+o, 20, 20),"X"))
					DialogWindow.instance.DeleteNode(windowID);
				if(GUI.Button( new Rect(70+m*4, m+o, w-70-m*3-o, 20),"Add"))
					DialogWindow.instance.AddChildNode(windowID, childType);
				return enabled;
			}
			
		}
		
		//Text Node
	public class TextNodeWindow: NodeWindow {
			
			public  TextNodeWindow (){
				type = Dialog.DialogNodeType.Text;
			
			}
			
			public override void  CreateNodeWindow ( int _windowID  ){
				enabled = StandardNodeHead(enabled);
				target = EditorGUI.Popup( new Rect(m, 20+m*2+o, width-2*m, 20), target, DialogWindow.instance.registeredTargets, "popup");
				text = EditorGUI.TextArea( new Rect(m, 40+m*3+o, width-2*m, height-40-m*4-o),text, "textarea");
				GUI.DragWindow ();
			}
		}
		
		//Pass Node
	public	class PassNodeWindow : NodeWindow {
			
		public PassNodeWindow (){
				type = Dialog.DialogNodeType.Pass;
				pass = true;
				height = 45;
			}
			
		public override void  CreateNodeWindow ( int _windowID  ){
			enabled = StandardNodeHead(enabled);
			GUI.DragWindow ();   
			}
		}
		
		
		//Choice Node
	public class ChoiceNodeWindow: NodeWindow {
			
		public  ChoiceNodeWindow (){
				type = Dialog.DialogNodeType.Choice;
				height = 100;
			}
			
		public override Dialog.DialogNode Save (){
				Dialog.DialogNode node= base.Save();
				//FIXME_VAR_TYPE tmp= new Array(node.nodesOut);
//				
				//Sort answer nodes
				List<int> _nodesOutList = new List<int>();
				
				for(int i=0;i<node.nodesOut.Length;i++){
				_nodesOutList.Add(node.nodesOut[i]);
					}
				List<int> sortArray= DialogWindow.SortNodesByOrder(_nodesOutList);	
				node.nodesOut =	sortArray.ToArray();
				
				Dialog.Answer[] tmpAnswers= new Dialog.Answer[sortArray.Count];
				for(int l= 0; l < sortArray.Count; l++) {
					NodeWindow answerNode= GetNodeWindowByID(sortArray[l]);
					Dialog.Answer newAnswer= new Dialog.Answer();
					newAnswer.answerID = answerNode.windowID;
					newAnswer.text = answerNode.text;
					newAnswer.enabled = answerNode.enabled;
					newAnswer.editorRect = answerNode.position;
					newAnswer.nodesOut = answerNode.GetNodesOut();
					tmpAnswers[l] = newAnswer;
				}
				node.answers = tmpAnswers;
				
			
				return node;
			}
			
		public override void  CreateNodeWindow ( int _windowID  ){	
			float w = width-2*m;
			float h = height-3*m-o;
			enabled = ParentNodeHead(enabled, Dialog.DialogNodeType.Answer);
			text = EditorGUI.TextArea( new Rect(m, 20+2*m+o, w, h-20),text, "textarea");
			GUI.DragWindow();
			}
		}
		
		//Answer Node
	public class AnswerNodeWindow:NodeWindow {
			
		public int order= 0;
		public int answerID=0;
		public AnswerNodeWindow (){
				text = "Answer Title";
				type = Dialog.DialogNodeType.Answer;
				pass = true;
				height = 70;
			}
			
		public override  Dialog.DialogNode Save (){
////		
//			 Dialog.DialogNode node= base.Save();
//				//	    	//Answer nodes will always be skipped, as they don't
//				//			//show any text of their own
//							node.pass = true;
//							return node;
				return null;
			}
			
		 public override void  CreateNodeWindow ( int _windowID  ){	
			float w= width-2*m;
			
			order = EditorGUI.IntField( new Rect(m,m+o, 40, 20), order, "textfield");
			enabled = GUI.Toggle( new Rect(40+m*2,m+o, 15, 20),enabled, "", "toggle");		
			if(GUI.Button( new Rect(55+m*3, m+o, 20, 20),"X"))
				DialogWindow.instance.DeleteNode(_windowID);
			if(GUI.Button( new Rect(75+m*4, m+o, w-75-3*m, 20),"Out"))
				DialogWindow.instance.SetOut(_windowID);
			
			text = EditorGUI.TextField( new Rect(m, o+20+m*2, w, 20), "", text, "textfield");
			GUI.DragWindow();   
			}
		}
		
		//Script Node
		public	class ScriptNodeWindow: NodeWindow {
			
		public ScriptNodeWindow (){
			type = Dialog.DialogNodeType.Script;
				pass = true;
				height = 95;
			}
			
		public override Dialog.DialogNode Save (){
			Dialog.DialogNode node= base.Save();
				node.pass = true;
				return node;
			}
			
		 public override void  CreateNodeWindow ( int _windowID  ){	
			float w= width-2*m;
			float h= height -2*m-o;
			
			enabled = StandardNodeHead(enabled);
			target = EditorGUI.Popup( new Rect(m, 20+m*2+o, w, 20), target, DialogWindow.instance.registeredTargets, "popup");
			variable = EditorGUI.TextField( new Rect(m, 40+m*3+o, w-60, 20), "", variable, "textarea");
			text = EditorGUI.TextField( new Rect(w-60+m*2, 40+m*3+o, 55, 20), "", text, "textarea");
			GUI.DragWindow();        
			}
		}
		
		//Switch Node
	public class SwitchNodeWindow: NodeWindow {
			
		public  SwitchNodeWindow (){
				type = Dialog.DialogNodeType.Switch;
				pass = true;
				height = 70;
			}
			
		public  override Dialog.DialogNode Save (){
			Dialog.DialogNode node= base.Save();
			node.pass = true;
				//Sort case nodes
				
			List<int> _nodesOutList = new List<int>();
			for(int z=0;z<node.nodesOut.Length;z++){
				_nodesOutList.Add(node.nodesOut[z]);
			
			}
			
			List<int> sortArray = DialogWindow.SortNodesByOrder(_nodesOutList);
				
				node.nodesOut = sortArray.ToArray();
				
			Dialog.Case[] tmpCases = new Dialog.Case[sortArray.Count];
				for(int l= 0; l < sortArray.Count; l++) {
				NodeWindow caseNode = GetNodeWindowByID(sortArray[l]);
				Dialog.Case newCase= new Dialog.Case();
					newCase.caseID = caseNode.windowID;
//					newCase.caseValue = caseNode.caseValue;
					newCase.method = caseNode.methodInt;
					newCase.enabled = caseNode.enabled;
					newCase.editorRect = caseNode.position;
					
					newCase.nodesOut = caseNode.GetNodesOut();
					
					tmpCases[l] = newCase;
				}
				node.cases = tmpCases;
				return node;
			}
			
		public override void CreateNodeWindow (int _windowID){	
			float w= width-2*m;
			float h= height -2*m-o;
			
			enabled = ParentNodeHead(enabled, Dialog.DialogNodeType.Case);
			variable = EditorGUI.TextField( new Rect(m, 20+m*2+o, w, 20), "", variable, "textarea");
			GUI.DragWindow();    
			}
		}
		
		//Case Node
	public  class CaseNodeWindow: AnswerNodeWindow {
			
		public string[] methods= {">","<","==", "!=", "undefined"};
		public float caseValue= 0.0f;
			
		public  CaseNodeWindow (){
				type = Dialog.DialogNodeType.Case;
				pass = true;
				height = 70; 
				methodInt = 0;
				param = new string[2];
			}
			
		public  override Dialog.DialogNode Save (){
//				Dialog.DialogNode node= base.Save();
//				node.pass = true;
//				//			return node;
				return null;
			}
			
		public override void  CreateNodeWindow ( int _windowID  ){	
				float w = width-2*m;
			
			order = EditorGUI.IntField(new Rect(m,m+o, 40, 20), order, "textfield");
			enabled = GUI.Toggle( new Rect(40+m*2,m+o, 15, 20),enabled, "", "toggle");		
			if(GUI.Button( new Rect(55+m*3, m+o, 20, 20),"X"))
				DialogWindow.instance.DeleteNode(_windowID);
			if(GUI.Button( new Rect(75+m*4, m+o, w-75-3*m, 20),"Out"))
				DialogWindow.instance.SetOut(_windowID);
			methodInt = EditorGUI.Popup( new Rect(m, 20+m*2+o, 45, 20), methodInt, methods, "popup");
			
			caseValue = EditorGUI.FloatField( new Rect(45+m*2, 20+m*2+o, w-60+m*2, 20),caseValue, "textarea");
			
			GUI.DragWindow();   
			}
		}
		
		//Variable Node
	public class VariableNodeWindow : NodeWindow {
			
			//		FIXME_VAR_TYPE visibility= 0;
			string[] methods = {"=","+","-"};
			
		public	VariableNodeWindow (){
				type = Dialog.DialogNodeType.Variable;
				pass = true;
				height = 95; 
				variable = "";
				text = ""; 
				methodInt = 0;
			}
			
		public  override Dialog.DialogNode Save (){
			Dialog.DialogNode node= base.Save();
				node.pass = true;
				return node;
			}
			
			public override void  CreateNodeWindow ( int _windowID  ){	
			enabled = StandardNodeHead(enabled);
			//				visibility = EditorGUI.Popup( new Rect(m, 20+m*2+o, width-m*2, 20), visibility, ["local", "global"], "popup");
			variable = EditorGUI.TextField( new Rect(m, 20+m*2+o, width-m*2, 20), "",variable, "textarea");
			methodInt = EditorGUI.Popup( new Rect(m, 40+m*3+o, 30, 20), methodInt, methods, "popup");
			text = EditorGUI.TextField( new Rect(30+m*2, 40+m*3+o, 105, 20), "", text, "textarea");
			//            variable = EditorGUI.TextField( new Rect(m, 40+m*3+o, 65, 20), "", variable, "textarea");
			//			methodInt = EditorGUI.Popup( new Rect(65+m*2, 40+m*3+o, 30, 20), methodInt, methods, "popup");
			//			text = EditorGUI.TextField( new Rect(95+m*3, 40+m*3+o, 35, 20), "", text, "textarea");
			GUI.DragWindow ();    
			}
		} 
		
	public class Connection{
		public	int nodeIn;
		public	int nodeOut;
			
			public Connection (int o, int i){
				nodeIn = i;
				nodeOut = o;
			}
	}
		
	public class Graph{
		public	float x;
		public	float y;
		public	float width;
		public	float height;
		public	Vector2 closestPoint;
		public	Vector2 farestPoint;
	}
		
		private Graph graph= new Graph();
		
	public class Preview{
		public float x;
		public 	float y;
		public	float width;
		public	float height;
		public	float scale;
		public	float offset;
			
			public Rect GetRect (){
				return new Rect(x,y,width,height);
			}
			
	}
		
		private Preview preview= new Preview();
		
	public class ScrollView{
		public float width;
		public float height;
		public float offset;
	}
		
		public ScrollView scrollView = new ScrollView();
		
		public static void  Open ( Dialog dialog ,DialogEditor editor  ){
		
		DialogWindow window = (DialogWindow) EditorWindow.GetWindow(typeof(DialogWindow));
			window.selectedDialog = dialog;
			window.selectedEditor = editor;
			window.title = "Dialog Editor";
			window.NewLoad();
		}
		
		public Vector2 scrollPos= Vector2.zero;
		
		public bool dragView= false;
		int chosenType;
		
		public static Material lineMaterial;
		
		public static void  CreateLineMaterial (){
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		} 
		
		public static List<int> SortNodesByOrder ( List <int> nodes  ){ 
			List<int> tmp = nodes;
			List<int> sorted= new List<int>();
			while(tmp.Count > 0) {
				int foundAt= 0;
			int lowestID= GetNodeWindowByID(tmp[0]).order;
				for(int k= 1; k < tmp.Count; k++) {
				int x= GetNodeWindowByID(tmp[k]).order;
					if(x < lowestID) {
						lowestID = x;
						foundAt = k;
					}
				}
				sorted.Add(tmp[foundAt]);
				tmp.RemoveAt(foundAt);
				
				
			}
			return sorted;
		}
		
		 public void  OnGUI (){
			//The minimum padding to each side of the working area
			scrollView.offset = 100;
			
			if(windows != null && windows.Count > 0) {	 
			graph.farestPoint = Vector2.zero;
			graph.closestPoint = new Vector2(windows[0].position.x, windows[0].position.y);
			
				for(int i= 0; i<windows.Count; i++) {
					float posX= windows[i].position.x;
					float posY= windows[i].position.y;
					float width= windows[i].position.width;
					float height= windows[i].position.height;
					//Find the farest point
					if(posX+width > graph.farestPoint.x)
						graph.farestPoint.x = posX + width;
					if(posY+height > graph.farestPoint.y)
						graph.farestPoint.y = posY + height ;
					//Find the closest point
					if(posX < graph.closestPoint.x)
						graph.closestPoint.x = posX;
					if(posY < graph.closestPoint.y)
						graph.closestPoint.y = posY;
				}
				
				//Calculate graph info
				graph.width = Mathf.Max(graph.farestPoint.x, 1024) - Mathf.Min(graph.closestPoint.x, 0);
				graph.height = Mathf.Max(graph.farestPoint.y, 768) - Mathf.Min(graph.closestPoint.y, 0);
				graph.x = Mathf.Min(graph.closestPoint.x, 0);
				graph.y = Mathf.Min(graph.closestPoint.y, 0);
				
				//Draw connections between NodeWindows
				if(Event.current.type == EventType.repaint) {
					GL.PushMatrix();
					GL.LoadPixelMatrix();
					DrawConnection();
					GL.End();
					GL.PopMatrix();
				}
				
			} else {
				graph.width = 1024;
				graph.height = 768;
				graph.x = 0;
				graph.y = 0;
			}
			
			//Calculate the size of the scrollView from the size of the graph
			scrollView.width = graph.width + scrollView.offset*2;
			scrollView.height = graph.height + scrollView.offset*2;
			
			//Drag and drop functionality for the preview
			if(
				Event.current.type == EventType.MouseDown && (
				Event.current.button == 2 ||
				Event.current.button == 0 && PointIsWithinRect(Event.current.mousePosition, preview.GetRect())
				)) 
			{
				dragView = true;
			}
			else if(dragView && Event.current.type == EventType.MouseDrag) {
				float speed= -1.0f;
				if(Event.current.button == 0)
					speed = preview.scale;
				float scrollToX= scrollPos.x + Event.current.delta.x/speed;
				float scrollToY= scrollPos.y + Event.current.delta.y/speed;
				scrollPos.x = Mathf.Clamp(scrollToX, 0, scrollView.width-position.width+15);
				scrollPos.y = Mathf.Clamp(scrollToY, 0, scrollView.height-position.height+15);
				Event.current.Use();
				Repaint();
			}
			else if(Event.current.type == EventType.MouseUp) {
				dragView = false;
			}
			
			
			Rect scrollViewRect= new Rect (Mathf.Min(graph.closestPoint.x-scrollView.offset, -scrollView.offset), Mathf.Min(graph.closestPoint.y-scrollView.offset, -scrollView.offset), scrollView.width, scrollView.height);
			//Limit window size
			instance.maxSize = new Vector2(scrollViewRect.width+15, scrollViewRect.height+15);
			
			scrollPos = GUI.BeginScrollView (
				new Rect (0, 0, position.width, position.height),
				scrollPos, 
				scrollViewRect,
				true,
				true
				);
			
			if(windows != null && windows.Count > 0) {	    	
				BeginWindows();
				ShowWindows();
				EndWindows();
				
			}
			
			// Close the scroll view
			GUI.EndScrollView ();
			
			//Draw preview
			if(windows != null && windows.Count > 0 && Event.current.type == EventType.repaint) {
				//Maximum sizes
				preview.width = 150;
				preview.height = 150;
				//Keep aspect ratio
				if(graph.height < graph.width) {
					preview.height = (preview.width/graph.width)*graph.height;
				}  else {
					preview.width = (preview.height/graph.height)*graph.width;
				}
				
				preview.scale =preview.width/graph.width;
				preview.offset = Mathf.Round(scrollView.offset * preview.scale);
				preview.x = position.width - preview.offset*2 - 25 - preview.width;
				preview.y = 10;
				preview.width = preview.width + preview.offset*2;
				preview.height = preview.height + preview.offset*2;
				
				//GL.PushMatrix();
				GL.LoadPixelMatrix();
				if(!DialogWindow.lineMaterial)
					DialogWindow.CreateLineMaterial();
				DialogWindow.lineMaterial.SetPass(0);
				
				//Draw the transparent background of the preview
				GL.Begin(GL.QUADS);
				GL.Color(new Color(0.5f, 0.5f, 0.5f, 0.25f));
				GL.Vertex(new Vector2(preview.x, preview.y));
				GL.Vertex(new Vector2(preview.x+preview.width, preview.y));
				GL.Vertex(new Vector2(preview.x+preview.width, preview.y+preview.height));
				GL.Vertex(new Vector2(preview.x, preview.y+preview.height));
				GL.End();
				
				//Draw the preview's nodes, connections and the viewfield
				DrawPreview();
				
				//Draw a black border around the preview
				GL.Begin(GL.LINES);
				GL.Color(Color.black);
			GL.Vertex(new Vector2(preview.x, preview.y));
			GL.Vertex(new Vector2(preview.x+preview.width, preview.y));
				
			GL.Vertex(new Vector2(preview.x+preview.width, preview.y));
			GL.Vertex(new Vector2(preview.x+preview.width, preview.y+preview.height));
				
			GL.Vertex(new Vector2(preview.x+preview.width, preview.y+preview.height));
			GL.Vertex(new Vector2(preview.x, preview.y+preview.height));
				
			GL.Vertex(new Vector2(preview.x, preview.y+preview.height));
			GL.Vertex(new Vector2(preview.x, preview.y));
				GL.End();
				
				//GL.PopMatrix();
			}
			
			
			//Toolbar	
			GUI.color = Color.white;
			GUILayout.BeginHorizontal("box", GUILayout.Width(400));
			
			chosenType = EditorGUILayout.Popup(chosenType, nodeTypes, "popup", GUILayout.Width(100));
			
			if(GUILayout.Button("Create Node")) {
			Dialog.DialogNodeType typeChoice;
				if(chosenType == 1)
				typeChoice = Dialog.DialogNodeType.Choice;
				else if(chosenType == 2)
				typeChoice = Dialog.DialogNodeType.Script;
				else if(chosenType == 3)
				typeChoice = Dialog.DialogNodeType.Switch;
				else if(chosenType == 4)
				typeChoice =Dialog.DialogNodeType.Variable;
				else if(chosenType == 5)
				typeChoice = Dialog.DialogNodeType.Pass;
				else
				typeChoice = Dialog.DialogNodeType.Text;
				CreateNode(typeChoice);
			
			}
			if(GUILayout.Button("Clear")) {
				highestID = 0;
				windows = null;	
				connections = null;
			}
			if(GUILayout.Button("Save")) {
				Save();
			}
			
			characterName = EditorGUILayout.TextField(characterName, GUILayout.Width(120));        
			startNode = EditorGUILayout.IntField(startNode, "textfield");        
			
			EditorGUILayout.LabelField("Out:", GUILayout.Width(30));
			GUILayout.Box(selectedOut.ToString(), GUILayout.Width(50));
			EditorGUILayout.LabelField("In:", GUILayout.Width(30));
			GUILayout.Box(selectedIn.ToString(), GUILayout.Width(50));
			
			
			
			//        GUILayout.Box(selectedIn.ToString(), GUILayout.Width(50));
			
			
			GUILayout.EndHorizontal();        
		}
		
		public bool PointIsWithinRect (Vector2 point ,Rect rect){   	
			return ((point.x > rect.x && point.x < rect.x+rect.width)&& (point.y > rect.y && point.y < rect.y+rect.height));
		}
		
		public  void Save (){
			//FIXME_VAR_TYPE newNodes= new DialogNode[windows.Length];
			
			List<Dialog.DialogNode> newNodes = new List<Dialog.DialogNode>();
			for(int i= 0; i < windows.Count; i++) {
			Dialog.DialogNode newNode=windows[i].Save();
				//Save the window's current position on the work area
				//newNode.editorRect = windows[i].position;  	
				if(newNode != null)	
					newNodes.Add(newNode);
			}
			
			selectedEditor.targetObject.characterName = characterName;		
			selectedDialog.characterName = characterName;
			
			
			selectedEditor.targetObject.nodes = newNodes;
			selectedEditor.targetObject.startNode = startNode;
			
			
			//selectedEditor.targetObject.currentNode = startNode;
			
			dialogData.nodes = newNodes;
			dialogData.startNode = startNode;
			dialogData.characterName = characterName;
			
			string defaultPath= "";
			string defaultName= "newDialog.xml";
			
			if(selectedDialog.filePath!=null) {
				string test= selectedDialog.filePath;
				int lastIndex= test.LastIndexOf("/");
				Debug.Log(test.Substring(0, lastIndex));
				defaultPath = Application.dataPath + "/" + test.Substring(0, lastIndex);
				defaultName = test.Substring(lastIndex + 1, test.Length - lastIndex - 1);
			}
			
			
			
			 string path= EditorUtility.SaveFilePanel(
				"Save dialog as XML",
				defaultPath,
				defaultName,
				"xml");
			if(path.Length !=0) {
				
				
				
				DialogIO.Save(dialogData, path);
			}
			EditorUtility.SetDirty(selectedEditor.targetObject);
			EditorUtility.SetDirty(selectedEditor.target);
			AssetDatabase.Refresh();
		}
		
		public void  SetIn ( int newIn  ){
			if(selectedIn == newIn)
				selectedIn = 0;
			else {
				selectedIn = newIn;
				if(selectedOut==newIn)
					selectedOut = 0;
				else if(selectedOut != 0) {
					AddNewConnection(selectedOut, selectedIn);
					selectedIn = 0;
					selectedOut = 0;
				}
			}
		}
		
		public void  SetOut (int newOut){
			if(selectedOut == newOut)
				selectedOut = 0;
			else {
				selectedOut = newOut;
				if(selectedIn==newOut)
					selectedIn = 0;
				else if(selectedIn != 0) {
					AddNewConnection(selectedOut, selectedIn);
					selectedIn = 0;
					selectedOut = 0;
				}
			}
		}
		
		public void  AddNewConnection ( int newOut ,   int newIn  ){
			bool delete= false;
			for(int i= 0; i<connections.Count; i++) {
				if(connections[i].nodeOut == newOut && connections[i].nodeIn == newIn) {
					connections.Remove(connections[i]);
					delete = true;
					break;
				}
			}
			if(!delete) {
				connections.Add(new Connection(newOut, newIn));
			}
		}
		
		public void  DeleteNode ( int id  ){
			
			
			int index= DialogWindow.instance.GetNodeWindowIndexByID(id);
			NodeWindow window= windows[index];
			windows.RemoveAt(index);
			//If the deleted node was a choice-node, delete all dependent answer-nodes
			if(window.type == Dialog.DialogNodeType.Choice) {
				for(int i= 0; i<connections.Count; i++) {
					if(connections[i].nodeOut == id) {
						DeleteNode(connections[i].nodeIn);
						i--;
					}
				}
			}
			
			//Remove connections that included the deleted node
			for(int j= 0; j<connections.Count; j++) {
				if(connections[j].nodeOut == id || connections[j].nodeIn == id) {
					connections.RemoveAt(j);
					j--;
				}
			}
			
			if(id==startNode && windows.Count > 0)
				startNode = windows[0].windowID;
			
		}
		
		public 	void  AddChildNode ( int id ,   Dialog.DialogNodeType dt  ){
			NodeWindow childNode= CreateNode(dt);
			AddNewConnection(id, childNode.windowID);
			
		NodeWindow childWindow = GetNodeWindowByID(childNode.windowID);
		NodeWindow parentWindow = GetNodeWindowByID(id);
			
			childWindow.position.x = parentWindow.position.x + parentWindow.position.width + 100;
			childWindow.position.y = parentWindow.position.y;
			
		}
		
	public NodeWindow CreateWindowNode ( Dialog.DialogNodeType dt  ){
		List<NodeWindow> tmpWindows = new List<NodeWindow>();
		NodeWindow newWindow=null;
			//Text
		if(dt == Dialog.DialogNodeType.Text)
			newWindow = new TextNodeWindow();
			//Choice
		else if(dt == Dialog.DialogNodeType.Choice)
			newWindow = new ChoiceNodeWindow();
			//Answer
		else if(dt == Dialog.DialogNodeType.Answer)
			newWindow = new AnswerNodeWindow();
			//Script
		else if(dt == Dialog.DialogNodeType.Script)
			newWindow = new ScriptNodeWindow();
			//Switch
		else if(dt == Dialog.DialogNodeType.Switch)
			newWindow = new SwitchNodeWindow();
			//Case
		else if(dt == Dialog.DialogNodeType.Case)
				newWindow = new CaseNodeWindow();
			//Pass
		else if(dt == Dialog.DialogNodeType.Pass)
			newWindow = new PassNodeWindow();
			//Variable
		else if(dt == Dialog.DialogNodeType.Variable)
			newWindow = new VariableNodeWindow();
		
			return newWindow;
		}
		
	public	NodeWindow CreateNode ( Dialog.DialogNodeType dt  ){
			NodeWindow newWindow = CreateWindowNode(dt);
			newWindow.enabled = true;
			newWindow.windowID = ++highestID;
			GUI.FocusWindow(newWindow.windowID);
			//GUI.BringWindowToFront(newWindow.windowID);
			newWindow.windowName = dt+": "+newWindow.windowID;
			newWindow.text = "";
		Rect a = new Rect(graph.x+scrollPos.x+Screen.width*0.5f-scrollView.offset, graph.y+scrollPos.y+Screen.height*0.5f-scrollView.offset, 0, 0);
			AddNewWindow(newWindow,a);
			return newWindow;
		}
		
	public void  AddNewWindow (NodeWindow newNode ,Rect newRect){
//		            List<NodeWindow> tmpWindows = new List<NodeWindow>();
//		                  tmpWindows = windows;
//		            this.windows = new List<NodeWindow>();
//		            for(int i= 0; i<tmpWindows.Count-1; i++) {
//		                windows[i] = tmpWindows[i];
//		            }
		newNode.position = newRect;
		if(windows==null)	//null exceptions
		windows = new List<NodeWindow>();
		windows.Add(newNode);
		
		if(windows.Count == 1)
			startNode = newNode.windowID;
		
		            newNode.position = newRect;
		            windows[windows.Count-1] = newNode;
		
		
	}
		
		public void  ShowWindows (){
			for(int i= 0; i<windows.Count; i++) {
				windows[i].position = windows[i].Show();
				
			}
		}
		
		public void  DrawConnection (){
			Vector2 scroll= new Vector2(-scrollPos.x-Mathf.Min(graph.closestPoint.x-scrollView.offset, -scrollView.offset), -scrollPos.y-Mathf.Min(graph.closestPoint.y-scrollView.offset, -scrollView.offset));
			
			//Set up material			
			if(!DialogWindow.lineMaterial)
				DialogWindow.CreateLineMaterial();
			DialogWindow.lineMaterial.SetPass(0);
			
			if(connections==null) //null exceptions
			connections = new List<Connection>();
			for(int i= 0; i < connections.Count; i++) {
			NodeWindow start = GetNodeWindowByID(connections[i].nodeOut);
			NodeWindow end = GetNodeWindowByID(connections[i].nodeIn);
				
				
				//Main line	
				Vector2 startPos= scroll+new Vector2(start.position.x+start.position.width, start.position.y+30);
			Vector2 endPos= scroll+new Vector2(end.position.x, end.position.y+30);
				
				GL.Begin(GL.LINES);
				GL.Color(Color.gray);
				GL.Vertex(startPos);
				GL.Vertex(endPos);
				
				//Arrow
				GL.Color(Color.gray);
				Vector2 x= (endPos-startPos)*0.5f;
			GL.Vertex(startPos+x-x.normalized*10+(new Vector2(-x.y, x.x).normalized*5));
				GL.Vertex(startPos+x);
				GL.Vertex(startPos+x);
			GL.Vertex(startPos+x-x.normalized*10+(new Vector2(x.y, -x.x).normalized*5));
				GL.End();
				
			}		
		}
		
		public void  DrawPreview (){
			Vector2 previewPos= new Vector2(preview.x-graph.x*preview.scale+preview.offset, preview.y-graph.y*preview.scale+preview.offset);
			
			//Background	    
			GL.Begin(GL.QUADS);
			GL.Color(new Color(1.0f, 1.0f, 1.0f, 0.5f));
			
			Rect sr= new Rect(scrollPos.x*preview.scale, scrollPos.y*preview.scale, position.width*preview.scale, position.height*preview.scale);
			
			Vector2 pp= new Vector2(preview.x, preview.y);
			
			GL.Vertex(pp+ new Vector2(
				Mathf.Max(sr.x, 0),
				Mathf.Max(sr.y, 0)
				));
			
		GL.Vertex(pp+new Vector2(
				Mathf.Min(sr.x+sr.width, preview.width),
				Mathf.Max(sr.y, 0)
				));
			
		GL.Vertex(pp+new Vector2(
				Mathf.Min(sr.x+sr.width, preview.width),
				Mathf.Min(sr.y+sr.height, preview.height)
				));
			
		GL.Vertex(pp+new Vector2(
				Mathf.Max(sr.x, 0),
				Mathf.Min(sr.y+sr.height, preview.height)
				));
			
			GL.End();
			
			if(!DialogWindow.lineMaterial)
				DialogWindow.CreateLineMaterial();
			DialogWindow.lineMaterial.SetPass(0);
			
			//Connecting lines
			for(int i= 0; i < connections.Count; i++) {
			NodeWindow start=GetNodeWindowByID(connections[i].nodeOut);
			NodeWindow end= GetNodeWindowByID(connections[i].nodeIn);
				
				Vector2 startPos= start.position.center;
			Vector2 endPos= end.position.center;
				
				GL.Begin(GL.LINES);
				GL.Color(Color.gray);
				GL.Vertex(previewPos+(startPos*preview.scale));
				GL.Vertex(previewPos+(endPos*preview.scale));
				GL.End();
			}		
			
			//Quads representing the node windows
			for(int j=0; j<windows.Count; j++) {
				Rect wr= windows[j].position;
				GL.Begin(GL.QUADS);
				GL.Color(Color.gray);
				GL.Vertex(previewPos+new Vector2(wr.x*preview.scale, wr.y*preview.scale));
				GL.Vertex(previewPos+new Vector2((wr.x+wr.width)*preview.scale, wr.y*preview.scale));
				GL.Vertex(previewPos+new Vector2((wr.x+wr.width)*preview.scale, (wr.y+wr.height)*preview.scale));
				GL.Vertex(previewPos+new Vector2(wr.x*preview.scale, (wr.y+wr.height)*preview.scale));
				GL.End();
			
			}
			
		}
		
		
	public void  NewLoad (){
		registeredTargets = selectedDialog.registeredTargets;
		AssetDatabase.Refresh();
		dialogData = DialogIO.Load(selectedDialog.filePath, selectedDialog.characterName);
		//FIXME_VAR_TYPE nodes= selectedDialog.nodes;
		
		startNode = dialogData.startNode;
		characterName = dialogData.characterName;  
	
		
		List<Dialog.DialogNode> nodes= dialogData.nodes;
		if(nodes == null)
		{
			nodes[0] = dialogData.nodes[0];
			
		}
		
		windows= new List<NodeWindow>();
		
		//Get all nodes and turn them into windows
		for(int i= 0; i<nodes.Count; i++) {
			Dialog.DialogNode n =  nodes[i];
			
			if(n.nodeID> highestID)
				highestID = nodes[i].nodeID;
			
			NodeWindow newWindow= CreateWindowNode(n.type);
	
			newWindow.windowID = n.nodeID;
			newWindow.windowName = n.type+": "+n.nodeID;
			newWindow.target = n.target;  
			newWindow.text = n.text;			
			newWindow.variable = n.variable;
			newWindow.methodInt = n.method;
			newWindow.enabled = n.enabled;  
			newWindow.pass = n.pass;  
			//newWindow.param = n.param;
			newWindow.position = n.editorRect;
			
			windows.Add(newWindow);
			
			if(n.nodesOut!=null && n.type != Dialog.DialogNodeType.Choice && n.type != Dialog.DialogNodeType.Switch) {
				for(int k= 0; k<n.nodesOut.Length; k++) {
					connections.Add(new Connection(n.nodeID, n.nodesOut[k]));
				}
			}
			
			if(n.type == Dialog.DialogNodeType.Choice) {
				for(int j= 0; j<n.answers.Length; j++) {
					Dialog.Answer a= n.answers[j];
					AnswerNodeWindow newAnswer= new AnswerNodeWindow();
					newAnswer.order = j;
					newAnswer.windowID = a.answerID;
					newAnswer.windowName = "Answer: "+a.answerID;
					newAnswer.text = a.text;
					newAnswer.enabled = a.enabled;  
					newAnswer.position = a.editorRect;
					windows.Add(newAnswer);
					
					if(a.answerID> highestID)
						highestID = a.answerID;
					
					connections.Add(new Connection(n.nodeID, a.answerID));
					if(a.nodesOut!=null) {
						for(int l= 0; l<a.nodesOut.Length; l++) {
							connections.Add(new Connection(a.answerID, a.nodesOut[l]));
						}
					}
				}
			}
			
			if(n.type == Dialog.DialogNodeType.Switch) {
				for(int h= 0; h<n.cases.Length; h++) {
					Dialog.Case c= n.cases[h];
					CaseNodeWindow newCase= new CaseNodeWindow();
					newCase.order = h;
					newCase.windowID = c.caseID;
					newCase.windowName = "Case: "+c.caseID;
					newCase.methodInt = c.method;
					newCase.caseValue = c.caseValue;
					newCase.enabled = c.enabled;  
					newCase.position = c.editorRect;
					windows.Add(newCase);
					
					if(c.caseID> highestID)
						highestID = c.caseID;
					
					connections.Add(new Connection(n.nodeID, c.caseID));
					if(c.nodesOut!=null) {
						for(int m= 0; m<c.nodesOut.Length; m++) {
							connections.Add(new Connection(c.caseID, c.nodesOut[m]));
						}
					}
				}
			}
		}
		
	}
	
		public void  Load (){
			
			registeredTargets = selectedDialog.registeredTargets;
			
			startNode = selectedDialog.startNode;
			
		NodeWindow[] nodes = new NodeWindow[selectedDialog.nodes.Count];
//		selectedDialog.nodes;
		
			
			if(nodes == null){
			NodeWindow[] temp = new NodeWindow[selectedDialog.nodes.Count];
			
				selectedDialog.nodes.CopyTo(temp);
				windows[0]=temp[0];
			}
			windows= new List<NodeWindow>();
			
			//Get all nodes and turn them into windows
			for(int i= 0; i<nodes.Length; i++) {
				if(nodes[i].nodeID> highestID)
					highestID = nodes[i].nodeID;
				
				windows[i] = CreateWindowNode(nodes[i].type);
				
				//			if(nodes[i].type == DialogNodeType.Case)  {
				//				windows[i].methodInt = int.Parse(nodes[i].param[0]);
				//			}
				
			if(nodes[i].type == Dialog.DialogNodeType.Variable)  {
					if(nodes[i].param.Length<4) {
						Debug.Log("Hi!");
						string[] tmp= new string[4];
						for(int l= 0; l < nodes[i].param.Length; l++)
							tmp[l] = nodes[i].param[l];
						nodes[i].param = tmp;
					}
					if(nodes[i].param[3] == "" || nodes[i].param[3] == null)
						nodes[i].param[3] = "0";
				windows[i].methodInt = int.Parse((nodes[i].param[0]));
//					windows[i].visibility = int.Parse(nodes[i].param[3]);
				}
				
				windows[i].windowID = nodes[i].nodeID;
				windows[i].windowName = nodes[i].type+": "+nodes[i].nodeID;
				windows[i].target = nodes[i].target;	
				windows[i].text = nodes[i].text; 
				
				windows[i].variable = nodes[i].variable;
				windows[i].enabled = nodes[i].enabled;	
				windows[i].pass = nodes[i].pass;	
				windows[i].param = nodes[i].param;
				windows[i].position = nodes[i].editorRect;
			}
		
			//Create the connection between the nodes
			for(int j= 0; j< windows.Count; j++) {
			Dialog.DialogNode n = nodes[j];
			
					if(n.nodesOut != null) {
							for(int k= 0; k <n.nodesOut.Length; k++) {
						
					int a=windows[j].windowID;
					int b =  n.nodesOut[k];
							connections.Add(new Connection(a,b));
							if(n.type == Dialog.DialogNodeType.Choice || n.type == Dialog.DialogNodeType.Switch) 
								{
						GetNodeWindowByID(n.nodesOut[k]).order = k ;
									
	
								}
							}
				}
			}
			
		}   
		
		//Finds first instance of a NodeWindow with this ID...
	public static NodeWindow GetNodeWindowByID ( int id  ){
			NodeWindow foundWindow = new NodeWindow();
		for(int i= 0; i<DialogWindow.instance.windows.Count; i++) {
			if(DialogWindow.instance.windows[i].windowID == id) {
				foundWindow = DialogWindow.instance.windows[i];
					break;
				}
			}
			return foundWindow;
			
		}
		
		//Returns the index of the first instance of a NodeWindow with this ID...
	public int GetNodeWindowIndexByID ( int id  ){
			int index=0;
			for(int i= 0; i<windows.Count; i++) {
				if(windows[i].windowID == id) {
					index = i;
					break;
				}
			}
			return index;
		}
}
