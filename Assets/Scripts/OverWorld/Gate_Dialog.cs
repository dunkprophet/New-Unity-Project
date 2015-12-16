using UnityEngine;
using System.Collections;

public class Gate_Dialog : MonoBehaviour {
	int scene = 0;

	//public static bool playerTalking = false;
	
	private GUISkin MetalGUISkin;

	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
	}

	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = 2.60f;
			OverworldPlayer.instance.moveDestination [2] = -3.54f;
		}
	}

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
		
		GUI.skin = MetalGUISkin;

		//START
		
		GUILayout.BeginArea (new Rect (Screen.width/4, Screen.height/4, 400, 400));
		
		if (scene == 1) {
			
			GUILayout.BeginVertical ("Fence Gate", GUI.skin.GetStyle("window"));
			//FIRST WORD
			GUILayout.Label ("ENTER CODEENTER CODEENTER CODEENTER CODE");
			
			//FIRST CHOICE
			if (GUILayout.Button ("*Hit the cube*")) {
				scene = 2;
			}
			
			//SECOND CHOICE
			if (GUILayout.Button ("*HUG THE CUBE*")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			
			GUILayout.BeginVertical ("Fence Gate", GUI.skin.GetStyle("window"));
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
			GUILayout.BeginVertical ("Fence Gate", GUI.skin.GetStyle("window"));
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

