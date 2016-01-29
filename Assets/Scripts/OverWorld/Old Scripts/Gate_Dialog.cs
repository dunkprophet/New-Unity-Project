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
			OverworldPlayer.instance.moveDestination [0] = 1.22f;
			OverworldPlayer.instance.moveDestination [2] = 9.74f;
		}
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0) && Yakuza_Dialog.talkedToYakuza == true) {
			scene = 1;
		}
	}
	
	void OnMouseEnter()
	{
		if (Yakuza_Dialog.talkedToYakuza == true) {
			transform.GetComponent<Renderer> ().material.color = Color.gray;
		}
	}
	
	void OnMouseExit()
	{
		if (scene == 0 && Yakuza_Dialog.talkedToYakuza == true) {
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
			
			GUILayout.BeginVertical ("Gate", GUI.skin.GetStyle("window"));
			//FIRST WORD
			GUILayout.Label ("The gate had marks strewn across its surface, like someone had punched it repeatidly for quite a while. But that would have been impossible. Who could be strong enough to punch solid steel?");
			
			//FIRST CHOICE
			if (GUILayout.Button ("Leave.")) {
				scene = 2;
			}
			
			GUILayout.EndVertical ();

		} else if (scene == 2) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			scene = 0;
		}
		
		GUILayout.EndArea ();
	}
}

