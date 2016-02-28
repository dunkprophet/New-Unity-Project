using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {

		OverworldManager.instance.textBoxShown = true;

		OverworldManager.instance.freeMode = false;

		OverworldManager.instance.i = 0;
		OverworldManager.instance.dialogController = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.menuStarted == false && OverworldManager.instance.freeMode == false) {
			
			
			
			if (OverworldManager.instance.dialogController < 0) {
				
				OverworldManager.instance.i = 0;
				OverworldManager.instance.dialogController = 0;
				
			}
			
			if (OverworldManager.instance.dialogController == 0) {
				
				GameManager.instance.gamePaused = true;	
				//Shown in textbox
				//scene1Background
				OverworldManager.instance.text = "...zzzbbbttzzzz...";
				OverworldManager.instance.deleteOldText = true;
				//OverworldManager.instance.lerpingColor = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), OverworldManager.instance.fadeSpeed * Time.deltaTime);
				
				
				
			}
			if (OverworldManager.instance.dialogController == 1) {
				OverworldManager.instance.text = "\"Hold on, guys, I think someone's trying to join the server.\"";
				
			}
			if (OverworldManager.instance.dialogController == 2) {
				OverworldManager.instance.text = "Calvin recognized the voice in an instant.";
				OverworldManager.instance.deleteOldText = false;
				
			}
			if (OverworldManager.instance.dialogController == 3) {
				OverworldManager.instance.text = " Aya.";
				OverworldManager.instance.deleteOldText = true;
				
			}
			if (OverworldManager.instance.dialogController == 4) {
				OverworldManager.instance.text = "\"Calvin, is that you?\" The only thing greeting him after connecting to the node was Aya's voice, " +
					"and an open, dimply lit area, guarded by the images of two stone creatures.";
				
			}
			if (OverworldManager.instance.dialogController == 5) {
				OverworldManager.instance.text = "He had read about the netscape, and the type of things that took place there, " +
					"but the shitty connection on his home island never allowed him to experience it.";
				
			}
			if (OverworldManager.instance.dialogController == 6) {
				OverworldManager.instance.text = "\"Hey,\" Calvin typed. \"It's been a while.\"";
			}
			if (OverworldManager.instance.dialogController == 7) {
				OverworldManager.instance.text = "\"It has!\" she exclaimed. \"How's everything going?\"";
			}
			if (OverworldManager.instance.dialogController == 8) {
				OverworldManager.instance.text = "\"Good, so far,\" he told her. \"Just arrived.\"";
			}
			if (OverworldManager.instance.dialogController == 9) {
				OverworldManager.instance.text = "\"Must be pretty amazing to experience the netscape for the first time, then!\"";
			}
			if (OverworldManager.instance.dialogController == 10) {
				OverworldManager.instance.text = "\"Uh... yeah.\"";
				OverworldManager.instance.deleteOldText = false;
			}
			if (OverworldManager.instance.dialogController == 11) {
				OverworldManager.instance.text = " He paused for a moment before typing out, \"It's sort of overwhelming.\"";
				OverworldManager.instance.deleteOldText = true;
			}
			if (OverworldManager.instance.dialogController == 12) {
				OverworldManager.instance.text = "\"You'll get used to it,\" she said. \"Speaking of which, do you know where we are?\"";
			}
			if (OverworldManager.instance.dialogController == 13) {
				OverworldManager.instance.text = "\"...The Training Grounds?\"";
			}
			if (OverworldManager.instance.dialogController == 14) {
				OverworldManager.instance.text = "\"Yes. I don't understand it fully yet, but some of my classmates showed me the ropes before.\"";
			}
			if (OverworldManager.instance.dialogController == 15) {
				OverworldManager.instance.text = "\"Of what?\"";
			}
			if (OverworldManager.instance.dialogController == 16) {
				OverworldManager.instance.text = "She let out a small laugh. \"Net battling.";
				OverworldManager.instance.deleteOldText = false;
			}
			if (OverworldManager.instance.dialogController == 17) {
				OverworldManager.instance.text = " It's based on the very real intrusions done by criminals.\"";
				OverworldManager.instance.deleteOldText = true;
			}
			if (OverworldManager.instance.dialogController == 18) {
				OverworldManager.instance.text = "She went on, mumbling, \"What we do here is completely lawful, of course.\"";
			}
			if (OverworldManager.instance.dialogController == 19) {
				OverworldManager.instance.text = "\"Do you want me to show you the basics?\"";
			}
			if (OverworldManager.instance.dialogController == 20) {
				OverworldManager.instance.text = "\"...\"";
				OverworldManager.instance.responseWanted = true;
			}


			if (OverworldManager.instance.dialogController == 144) {
				OverworldManager.instance.textBoxShown = false;
				OverworldManager.instance.dialogController = 0;
				OverworldManager.instance.text = "";
			}
			/*\"It was a real suprise- that you've moved to Chiba all by yourself!\"";
			}
			if (OverworldManager.instance.dialogController == 8) {
				OverworldManager.instance.text = "\"Yeah, I-\"";
			}
			if (OverworldManager.instance.dialogController == 9) {
				OverworldManager.instance.text = "\"What did your parents say? You DID tell them, right?\"";
			}
			if (OverworldManager.instance.dialogController == 10) {
				OverworldManager.instance.text = "Calvin went quiet. Aya sighed, and mumbled, \"Of course you didn't.\"";
			}
			if (OverworldManager.instance.dialogController == 11) {
				OverworldManager.instance.text = "\"I don't think they really care.\"*/
		}
	}
}
