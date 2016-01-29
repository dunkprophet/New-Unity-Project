using UnityEngine;
using System.Collections;

public class Fence_Dialog : MonoBehaviour {
	int scene = 0;

	//public static bool playerTalking = false;
	
	private GUISkin MetalGUISkin;

	public static bool talkedToYakuza;

	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
	}

	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = 3.91f;
			OverworldPlayer.instance.moveDestination [2] = -1.8f;
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

			GUILayout.BeginVertical ("Chain-link fence", GUI.skin.GetStyle("window"));
			//FIRST WORD
			if (talkedToYakuza == true){GUILayout.Label ("22222");}
			GUILayout.Label ("Leaked-out green goop flooded the area beyond the chain-link fence. The stench soared at me, hitting me in the gut. Almost puked out what little content my stomach held.");

			//FIRST CHOICE
			if (GUILayout.Button ("Take a closer look.")) {
				scene = 2;
			}
			
			//SECOND CHOICE
			if (GUILayout.Button ("Walk away.")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			
			GUILayout.BeginVertical ("Chain-link fence", GUI.skin.GetStyle("window"));
			//From CHOICE 2
			GUILayout.Label ("But looking past it all- there was a light. A colorful, glaring light of completely dead coldness.");
			GUILayout.Label (" It came from the streets of the city. Chiba City. Gazing upon that complex mess of steel and flesh for the very first time placed a seed in my mind, a thought of what I wanted to do with my life.");
			GUILayout.Label (" ");

			//FIRST CHOICE
			if (GUILayout.Button ("Leave.")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
			
		} else if (scene == 3) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			scene = 0;
		}
		
		GUILayout.EndArea ();
	}
}

