using UnityEngine;
using System.Collections;

public class Gate_Dialogue : MonoBehaviour {
	public GUISkin skin;
	string scene = "stop"; 
	
	/*void OnMouseDown(){
		string scene = "start";
	}*/
	
	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			scene = "start";
		}
	}
	
	
	/*public void StartCondition(){
		if(-4.5 < OverworldPlayer.instance.moveDestination[0] < -3.5) {
			
			string scene = "start";
			
		}
	}*/
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		if (scene == "start") {
			GUILayout.BeginArea (new Rect (50, 50, 250, 250));
			
			
			GUILayout.BeginVertical ();
			GUILayout.Label ("Howdy.");
			
			if (GUILayout.Button ("O hai.")) {
				scene = "hello";
			}
			
			if (GUILayout.Button ("Where the party is?")) {
				scene = "party";
			}
			
			GUILayout.EndVertical ();
		} else if (scene == "hello") {
			GUILayout.BeginVertical ();
			GUILayout.Label ("Cool talk to you later.");
			GUILayout.EndVertical ();
		} else if (scene == "party") {
			GUILayout.BeginVertical ();
			GUILayout.Label ("I would also like to know.");
			GUILayout.EndVertical ();
		}
		
		GUILayout.EndArea ();
	}
}