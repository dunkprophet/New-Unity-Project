using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.freeMode == true && OverworldManager.instance.menuStarted == false) {
			
			
			
			if (OverworldManager.instance.dialogController < 0) {
				OverworldManager.instance.i = 0;
				OverworldManager.instance.dialogController = 0;
				OverworldManager.instance.textBoxShown = true;
				GameManager.instance.gamePaused = true;
				
				
			}
			
			if (OverworldManager.instance.dialogController == 0) {
				//Shown in textbox
				//scene1Background
				OverworldManager.instance.text = "...zzzbbbttzzzz...";
				//OverworldManager.instance.lerpingColor = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), OverworldManager.instance.fadeSpeed * Time.deltaTime);
				
				
				
			}
			if (OverworldManager.instance.dialogController == 1) {
				OverworldManager.instance.text = "...hold on, guys, I think someone's trying to join the server.";
				
			}
			if (OverworldManager.instance.dialogController == 2) {
				OverworldManager.instance.text = "Calvin recognized the voice isntantly.";
				
			}
			if (OverworldManager.instance.dialogController == 3) {
				OverworldManager.instance.text = "Not the best first impression.";
				
			}
			if (OverworldManager.instance.dialogController == 4) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Stepping off the ship onto the harbor, Calvin found himself midst a crowd of foreigners and ship-workers.";
				
			}
			if (OverworldManager.instance.dialogController == 5) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " In a slow pace they moved across the long steel-and-concrete walkway, until the sound of footsteps and mumbling started echoing up in the high ceiling.";
				/*"\"Next!\" the guard yelled across the docking station. The crewman first in line took a step forward. " +
					"He held up his passport with a shaking hand.";
					The guard glanced at it and placed his hand on the crewman's shoulder. \"Step aside.\"
					Panic spread across the man's face. \"Please,\" he mumbled, looking back to the crowd. \"I don't-\"
					The guard took out a stun gun. \"Stay where you are.\" He did not. The man lept into the crowd, pushing 
					No one in the crowd seemed to care. When the guard caalled out, " +
				"\"Next!\" after the uncouncious man had been dragged off, another took his place at the front of the line.
				Calvin however, found himself caring immensely. " +
				"The guy with the fake passport got knocked out with a couple of thousand volts. What would they do to the guy with none at all?
				 */
				
			}
			if (OverworldManager.instance.dialogController == 6) {
				
				OverworldManager.instance.text = "Soon they were all stopped by a line of checkpoint, entryways into the city guarded by people in black and blue riot gear.";
				
			}
			if (OverworldManager.instance.dialogController == 144) {
				OverworldManager.instance.textBoxShown = false;
				OverworldManager.instance.dialogController = 0;
				OverworldManager.instance.text = "";
			}
		}
	}
}
