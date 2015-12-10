using UnityEngine;
using System.Collections;

public class Gate_Dialog : MonoBehaviour {
	public GUISkin skin;
	int scene = 0;
	
	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			scene = 1;
		}
	}
	
	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.gray;
	}
	
	void OnMouseExit()
	{
		if (scene == 0) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
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
		//START
		
		GUILayout.BeginArea (new Rect (50, 50, 250, 250));
		
		if (scene == 1) {
			
			GUILayout.BeginVertical ();
			//FIRST WORD
			GUILayout.Label ("ENTER CODE");
			
			//FIRST CHOICE
			if (GUILayout.Button ("*HIT THE CUBE*")) {
				scene = 2;
			}
			
			//SECOND CHOICE
			if (GUILayout.Button ("*HUG THE CUBE*")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			
			GUILayout.BeginVertical ();
			//From CHOICE 2
			GUILayout.Label ("*CUBE IS SAD*");
			
			//FIRST CHOICE
			if (GUILayout.Button ("Sorry, Cube... :(")) {
				scene = 4;
			}
			
			//SECOND CHOICE
			if (GUILayout.Button ("Fuck cubes, man! THE GOVERMENT'S CORRUPT!")) {
				scene = 4;
			}
			
			GUILayout.EndVertical ();
			
		} else if (scene == 3) {
			GUILayout.BeginVertical ();
			GUILayout.Label ("*CUBE LOVES YOU*");
			
			//ONLY CHOICE
			if (GUILayout.Button ("Thanks, Chris!")) {
				scene = 4;
			}
			
			GUILayout.EndVertical ();
			
		} else if (scene == 4) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			scene = 0;
		}
		
		GUILayout.EndArea ();
	}
	
}
