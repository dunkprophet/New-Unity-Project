using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DialogIO:Dialog {
	
	
	string characterName;
	
	public static void  Save ( DialogData dialog ,   string path  ){
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = false;			//We will handle indentation manually
		settings.CloseOutput = true;	
		settings.OmitXmlDeclaration = true;
		
		XmlWriter w  = XmlWriter.Create(path, settings);//Application.dataPath+"/"+filename+".xml", settings);
		w.WriteStartElement("dialog");
		w.WriteAttributeString("characterName", dialog.characterName);
		w.WriteAttributeString("startNode", dialog.startNode.ToString());
		for(int i= 0; i < dialog.nodes.Count; i++) {
			DialogNode n= dialog.nodes[i];
			//Opening element (i.e. "<text ...>")
			w.WriteWhitespace("\n\t");
			w.WriteStartElement(n.type.ToString().ToLower());
			//ID-attribute
			w.WriteAttributeString("id", n.nodeID.ToString());											//id=n.nodeId
			//target-attribute
			if(n.type == DialogNodeType.Text
			   || n.type == DialogNodeType.Choice
			   || n.type == DialogNodeType.Script)
				w.WriteAttributeString("target", n.target.ToString());									//target=n.target
			//variable-attribute
			if(!string.IsNullOrEmpty (n.variable))
				w.WriteAttributeString("var", n.variable);												//variable="xyz"
			//method-attribute
			if(n.type==DialogNodeType.Variable)
				w.WriteAttributeString("method", n.method.ToString());									//method="0"
			//value-attribute (text contained by a variable-node)
			if(n.type == DialogNodeType.Variable || n.type == DialogNodeType.Script) {
				w.WriteAttributeString("value", n.text);	
			}
			//nodesIn-attribute
			string nodesInString = CreateNodesString(n.nodesIn);
			if (!string.IsNullOrEmpty (nodesInString))
			w.WriteAttributeString ("nodesIn", nodesInString);										//nodesIn="xx,xy,yy"
			//nodesOut-attribute (don't show for switch- and case-nodes)
			if(n.type!=DialogNodeType.Choice && n.type!=DialogNodeType.Switch) {
				string nodesOutString = CreateNodesString(n.nodesOut);
				if((nodesOutString!=null && nodesOutString!="") && (n.answers==null || n.answers.Length == 0))
					w.WriteAttributeString("nodesOut", nodesOutString);									//nodesOut="xx,xy,yy"
			}
			//enabled-attribute (don't show when enabled)
			if(!n.enabled)
				w.WriteAttributeString("enabled", n.enabled.ToString().ToLower());						//enabled="true/false"
			//position-attribute (used for the editor window)
			w.WriteAttributeString("pos", n.editorRect.x.ToString()+","+n.editorRect.y.ToString());		//pos="x,y"
			//text
			if(n.text!=null && (n.type == DialogNodeType.Text || n.type == DialogNodeType.Choice)) {
				w.WriteWhitespace("\n\t\t");
				w.WriteString(n.text);
			}
			
			if(n.answers!=null && n.answers.Length > 0) {
				for(int l=0; l < n.answers.Length; l++) {
					Answer a= n.answers[l];
					//<answer ...>
					w.WriteWhitespace("\n\t\t");
					w.WriteStartElement("answer");
					//answerID-attribute of this answer-element
					w.WriteAttributeString("id", a.answerID.ToString());
					//nodesOut-attribute of this answer-element
					string answerNodesOutString = CreateNodesString(a.nodesOut);
					
					if(!string.IsNullOrEmpty(answerNodesOutString))
						w.WriteAttributeString("nodesOut", answerNodesOutString);
					//showIf-attribute of this answer-element
					if(a.showIf!=null)
						w.WriteAttributeString("showIf", a.showIf);
					//enabled-attribute of this case-element
					if(!a.enabled)
						w.WriteAttributeString("enabled", a.enabled.ToString().ToLower());
					//position-attribute of this case-element
					w.WriteAttributeString("pos", a.editorRect.x.ToString()+","+a.editorRect.y.ToString());
					//text of this answer-element
					if(!string.IsNullOrEmpty(a.text)) {
						w.WriteWhitespace("\n\t\t\t");
						w.WriteString(a.text);
					}
					//</answer>
					w.WriteWhitespace("\n\t\t");
					w.WriteEndElement();
				}
			}
			
			if(n.cases!=null && n.cases.Length > 0) {
				for(int o=0; o < n.cases.Length; o++) {
					Case c= n.cases[o];
					//<case ... />
					w.WriteWhitespace("\n\t\t");
					w.WriteStartElement("case");
					//caseID-attribute of this answer-element
					w.WriteAttributeString("id", c.caseID.ToString());
					//method-attribute of this case-element
					w.WriteAttributeString("method", c.method.ToString());
					//value-attribute of this case-element
					w.WriteAttributeString("value", c.caseValue.ToString());
					//nodesOut-attribute of this case-element
					string caseNodesOutString= CreateNodesString(c.nodesOut);
					if(caseNodesOutString!=null || caseNodesOutString=="")
						w.WriteAttributeString("nodesOut", caseNodesOutString);
					//enabled-attribute of this case-element
					if(!c.enabled)
						w.WriteAttributeString("enabled", c.enabled.ToString().ToLower());
					//position-attribute of this case-element
					w.WriteAttributeString("pos", c.editorRect.x.ToString()+","+c.editorRect.y.ToString());
					w.WriteEndElement();
				}
			}
			
			if((!string.IsNullOrEmpty(n.text) &&
			    (n.type != DialogNodeType.Variable
			 && n.type != DialogNodeType.Script))
			   || (n.answers!=null && n.answers.Length > 0)
			   || (n.cases!=null && n.cases.Length > 0))
				w.WriteWhitespace("\n\t");
			w.WriteEndElement();
			
		}
		w.WriteWhitespace("\n");
		w.WriteEndElement();
		w.Close();
	}
	
	public static DialogData Load ( string filePath  ){
		
		string path= Application.dataPath+"/"+filePath;
		
		DialogData dialog;
		
		
		FileInfo t = new FileInfo(path);
		if(t.Exists)
			dialog = ParseDialogXml(path);
		else
		{
			// Create Default Blank dialog file
			StreamWriter str = t.CreateText();
			str.WriteLine("<dialog characterName=\"Untitled\" startNode=\"1\">\n</dialog>");
			str.Close();
			
			
			// Now load blank file
			dialog = ParseDialogXml(path);	
		}
		
		return dialog;
	}
	
	
	
	public static DialogData Load ( string filePath ,   string characterName  ){
		
		string path= Application.dataPath+Path.DirectorySeparatorChar+"Resources"+Path.DirectorySeparatorChar+filePath;
		
		DialogData dialog;
		
		if(Application.isEditor) {
			FileInfo t = new FileInfo(path);
			if(!t.Exists)
			{
				// Create Default Blank dialog file
				StreamWriter str = t.CreateText();
				str.WriteLine("<dialog characterName=\""+characterName+"\" startNode=\"1\">\n</dialog>");
				str.Close();
			}
		}
		
		return ParseDialogXml(filePath);
	}
	
	
	
	static DialogData ParseDialogXml ( string path  ){
		DialogData dialog= new DialogData();
		System.Xml.XmlTextReader rdr; 
		
		if(Application.isEditor) {
			string p= Application.dataPath+Path.DirectorySeparatorChar+"Resources"+Path.DirectorySeparatorChar+path;
			rdr = new System.Xml.XmlTextReader(p);
		} else {
			Object asset = Resources.Load(path.Split("."[0])[0]);	
			StringReader txtReader= new StringReader(asset.ToString());
			Resources.UnloadAsset(asset);
			rdr = new System.Xml.XmlTextReader(txtReader);
		}
		
		 List<DialogNode> nodes= new List<DialogNode>();

		 DialogNode loadedNode = new DialogNode();
		
		 Answer loadedAnswer=null;

		 Case loadedCase=null;
	
		 List<Answer> loadedAnswers= new List<Answer>();

		 List<Case> loadedCases= new List<Case>();

		 string lastName = "";
		
		while(rdr.Read()) {
			
			switch(rdr.NodeType) {
			case XmlNodeType.Element:
				lastName = rdr.Name;				
				if(rdr.Name=="answer") {
					loadedAnswer = new Answer();
					loadedAnswer.enabled = true;
					while(rdr.MoveToNextAttribute()) {
						switch(rdr.Name) {
						case "id":
							loadedAnswer.answerID = int.Parse(rdr.Value);
							break;
						case "nodesOut":
							loadedAnswer.nodesOut = GetNodesArray(rdr.Value);
							break;
						case "showIf":
							loadedAnswer.showIf = rdr.Value;
							break;
						case "enabled":
							loadedAnswer.enabled = (rdr.Value == "false" ? false : true);
							break;
						case "pos":
							loadedAnswer.editorRect = GetEditorRect(rdr.Value);
							break;
						}
					}
				} else if(rdr.Name=="case") {
					loadedCase = new Case();
					loadedCase.enabled = true;
					while(rdr.MoveToNextAttribute()) {
						switch(rdr.Name) {
						case "id":
							loadedCase.caseID = int.Parse(rdr.Value);
							break;
						case "nodesOut":
							loadedCase.nodesOut = GetNodesArray(rdr.Value);
							break;
						case "method":
							loadedCase.method = int.Parse(rdr.Value);
							break;
						case "value":
							loadedCase.caseValue = float.Parse(rdr.Value);
							break;
						case "enabled":
							loadedCase.enabled = (rdr.Value == "false" ? false : true);
							break;
						case "pos":
							loadedCase.editorRect = GetEditorRect(rdr.Value);
							break;
						}
					}
					
					loadedCases.Add(loadedCase);
				} else {
					if(rdr.Name!="dialog") {
						loadedNode = new DialogNode();
						loadedNode.enabled = true;
						loadedNode.type = GetNodeType(rdr.Name);
						if(rdr.Name=="switch") {
							loadedNode.pass = true;
							loadedCases.Clear();
						}
						if(rdr.IsEmptyElement) {
							loadedNode.pass = true;
							nodes.Add(loadedNode);
						}
					}
					while(rdr.MoveToNextAttribute()) {
						switch(rdr.Name) {
						case "id":
							loadedNode.nodeID = int.Parse(rdr.Value);
							break;
						case "target":
							loadedNode.target = int.Parse(rdr.Value);
							break;
						case "var":
							loadedNode.variable = rdr.Value;
							break;
						case "method":
							loadedNode.method = int.Parse(rdr.Value);
							break;
						case "value":
							loadedNode.text = rdr.Value;
							break;
						case "nodesIn":
							loadedNode.nodesIn = GetNodesArray(rdr.Value);
							break;
						case "nodesOut":
							loadedNode.nodesOut = GetNodesArray(rdr.Value);
							break;
						case "enabled":
							loadedNode.enabled = (rdr.Value == "false" ? false : true);
							break;
						case "pos":
							loadedNode.editorRect = GetEditorRect(rdr.Value);
							break;
						case "characterName":
							dialog.characterName = rdr.Value;
							break;
						case "startNode":
							dialog.startNode = int.Parse(rdr.Value);
							break;
						}
					}
				}
				break;
			case XmlNodeType.Text:
				if(lastName == "text" || lastName == "choice")
					loadedNode.text = GetCleanText(rdr.Value);
				else if(lastName == "answer")
					loadedAnswer.text = GetCleanText(rdr.Value);
				break;
			case XmlNodeType.EndElement:
				switch(rdr.Name) {
				case "answer":
					loadedAnswers.Add(loadedAnswer);
					break;
					//					case "case":
					//						loadedCases.Add(loadedCase);
					//						break;
				case "choice":
				
					if(loadedAnswers.Count> 0){
					Answer[] c = loadedAnswers.ToArray();
					
						loadedNode.answers = c;
					loadedAnswers.Clear(); 
					nodes.Add(loadedNode);
					}
					break;
				case "switch":
					if(loadedCases.Count > 0){
						Case[] d = loadedCases.ToArray();
					
						loadedNode.cases = d;
					loadedAnswers.Clear(); 
					nodes.Add(loadedNode);
					}
					break;
				case "dialog":
					dialog.nodes = nodes;
					break;
				default:
					nodes.Add(loadedNode);
					break;
				}
				break;
			}
		}
		
		rdr.Close();
		
		return dialog;
	}
	
	static string CreateNodesString ( int[] nodes  ){
		string nodesOutString= "";
		if(nodes!=null && nodes.Length > 0) {
			for(int i= 0; i < nodes.Length; i++) {
				nodesOutString += nodes[i].ToString();
				if(i<nodes.Length-1)
					nodesOutString+=",";
			}
		}
		return nodesOutString;
	}
	
	static int[] GetNodesArray ( string s  ){
		string[] stringArray = s.Split(","[0]);
		int[] nodeArray = new int[stringArray.Length];
		for(int i=0; i<nodeArray.Length; i++)
			nodeArray[i] = int.Parse(stringArray[i]);
		return nodeArray;
	}
	
	static Rect GetEditorRect ( string s  ){
		string[] pos = s.Split(","[0]);
		return new Rect(float.Parse(pos[0]), float.Parse(pos[1]), 0, 0);
	}
	
	static DialogNodeType GetNodeType ( string s  ){
		DialogNodeType nodeType = new DialogNodeType();
		switch(s) {
		case "text":
			nodeType = DialogNodeType.Text;
			break;
		case "choice":
			nodeType = DialogNodeType.Choice;
			break;
		case "switch":
			nodeType = DialogNodeType.Switch;
			break;
		case "script":
			nodeType = DialogNodeType.Script;
			break;
		case "variable":
			nodeType = DialogNodeType.Variable;
			break;
		case "pass":
			nodeType = DialogNodeType.Pass;
			break;
		}
		return nodeType;
	}
	
	//Remove whitespaces used for formatting before and after actual text
	private static Regex pattern= new Regex("\\s*([\\s\\S]*\\S)\\s*");
	static string GetCleanText ( string text  ){
		Match m= pattern.Match(text);
		return m.Groups[1].Value;
	}
}
