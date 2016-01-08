using UnityEngine;
using System.Collections;

public class GateJack_Dialog : MonoBehaviour {
	int scene = 0;

	//public static bool playerTalking = false;

	public float fadeSpeed = 1.5f;

	private GUISkin MetalGUISkin;
	public bool hackable = false;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void FadeToWhite ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.white, fadeSpeed * Time.deltaTime);
	}

	public void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture> ().enabled = true;
		
		// Start fading towards black.
		FadeToWhite ();
		
		// If the screen is almost black...
		if (scene == 4) {
			// ... reload the level.
			Application.LoadLevel (2);
		}
	}
	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
	}

	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = 1.22f;
			OverworldPlayer.instance.moveDestination [2] = 9.74f;
			EndScene ();
		}
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0) && Yakuza_Dialog.talkedToYakuza == true) {
			scene = 1;
		}
		if (Input.GetMouseButtonDown (0) && Yakuza_Dialog.putOnIcebreaker == true) {
			hackable = true;
		}
	}
	
	void OnMouseEnter()
	{
		if (Yakuza_Dialog.talkedToYakuza == true) {
			transform.GetComponent<Renderer> ().material.color = Color.yellow;
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
			
			GUILayout.BeginVertical ("Panel", GUI.skin.GetStyle ("window"));
			//FIRST WORD
			GUILayout.Label ("The panel flickered a bit. I have no way of using it however, " +
				"as the controls seemed to be broken. The only input avaliable was the grimy port on the side.");

			//FIRST CHOICE
			if (hackable == true){
				if (GUILayout.Button ("Jack in.")) {
					scene = 2;
				}
			}
			
			//SECOND CHOICE
			if (GUILayout.Button ("Leave.")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			
			GUILayout.BeginVertical ("Panel", GUI.skin.GetStyle ("window"));
			//From CHOICE 2
			GUILayout.Label ("I shoved the cable into one of the grimy ports on the side. As my entirety was being consumed by the lightning fast bolt of pain shot through every bone in my body the stench of the docking station faded away, until all my senses became stuck in a white limbo.");
			GUILayout.Label ("I was in.");
			//FIRST CHOICE
			if (GUILayout.Button ("-Hack Completed-")) {
				scene = 4;
			}
			
			GUILayout.EndVertical ();
			
		} else if (scene == 3) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			scene = 0;

			
		} else if (scene == 4) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
		}

		
		GUILayout.EndArea ();
	}
}

