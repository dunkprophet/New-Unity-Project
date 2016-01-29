using UnityEngine;
using System.Collections;

public class DockingStation : MonoBehaviour {

	public bool ready = true;
	public bool tempBool = true;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.scene == 1) {


			
			if (OverworldManager.instance.dialogController < 0) {
				OverworldManager.instance.i = 0;
				OverworldManager.instance.dialogController = 0;



			}
			
			if (OverworldManager.instance.dialogController == 0) {
				//Shown in textbox
				//scene1Background
				OverworldManager.instance.text = "初日\nFirst Day";
				//OverworldManager.instance.lerpingColor = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), OverworldManager.instance.fadeSpeed * Time.deltaTime);



			}
			if (OverworldManager.instance.dialogController == 1) {
				OverworldManager.instance.text = "Every breath he took felt like mold invading his lungs.";

			}
			if (OverworldManager.instance.dialogController == 2) {
				OverworldManager.instance.text = "The damp air, most likely made from some foul combination of fumes and factory sludge, melted with the stench of sweat and shit.";

			}
			if (OverworldManager.instance.dialogController == 3) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Stepping off the ship onto the harbor, Calvin found himself midst a crowd of foreigners and ship-workers.";

			}
			if (OverworldManager.instance.dialogController == 4) {
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
			if (OverworldManager.instance.dialogController == 5) {

				OverworldManager.instance.text = "Soon they were all stopped by a line of checkpoint, entryways into the city guarded by people in black and blue riot gear.";

			}
			if (OverworldManager.instance.dialogController == 6) {
				OverworldManager.instance.text = "The ship's crew held up their passports, after which they were waved along by the security.";

			}
			if (OverworldManager.instance.dialogController == 7) {
				OverworldManager.instance.text = "While those like Calvin, those without passports, were examined in great detail.";

			}
			if (OverworldManager.instance.dialogController == 8) {
				OverworldManager.instance.text = " \"Your name and occupation.\"";

			}
			if (OverworldManager.instance.dialogController == 9) {
				OverworldManager.instance.text = "\"Purpose for coming to Chiba.\"";

			}
			if (OverworldManager.instance.dialogController == 10) {
				OverworldManager.instance.text = "Questions asked so many times they no longer sounded like they needed answering.";

			}
			/*if (OverworldManager.instance.dialogController == 10) {
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "1111111";
				OverworldManager.instance.response2 = "2222222";
				OverworldManager.instance.response3 = "3333333";

			}
			if (OverworldManager.instance.dialogController == 11 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.scene = -2;
				Application.LoadLevel(2);
			}
			if (OverworldManager.instance.dialogController == 11 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "2 is picked";
				if (tempBool == true){
					OverworldManager.instance.fadeToWhite = true;
					OverworldManager.instance.playMusic(1);
					tempBool = false;
				}
			}
			if (OverworldManager.instance.dialogController == 11 && OverworldManager.instance.response3picked == true) {
				OverworldManager.instance.text = "3 is picked";
			}*/
			if (OverworldManager.instance.dialogController == 11) {
				OverworldManager.instance.text = "It didn't matter either way. They let everyone in.";
			}
			if (OverworldManager.instance.dialogController == 12) {
				OverworldManager.instance.text = "From the ship Calvin had travelled on came a drove of Chinese civilians fleeing the war, people from the States searching for better lives, and even a few Indonesian families trying to escape the waste.";
			}
			if (OverworldManager.instance.dialogController == 13) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "In Chiba, everyone had a place.";
			}
			if (OverworldManager.instance.dialogController == 14) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = ".. or so he had heard.";
			}
			if (OverworldManager.instance.dialogController == 15) {

				OverworldManager.instance.text = "For an hour he stood in line looking on as they crowd slowly melted through the arching entrances to the city... long enough a time to get used to both the smell of sewers and the monotony of queuing.";
			}
			if (OverworldManager.instance.dialogController == 16) {

				OverworldManager.instance.text = "When he finally reached the checkpoint his body felt numb to the core.";
			}
			if (OverworldManager.instance.dialogController == 17) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"Kid,\" the guardsman said, ready to write down whatever he said onto a clipboard. \"Name and origin.\"";
			}
			if (OverworldManager.instance.dialogController == 18) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = "\n\n\"...\"";
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "[Truth] Calvin, Ogasawara.";
				OverworldManager.instance.response2 = "[Lie] Jiro, Hong Kong.";
				//OverworldManager.instance.response3 = "[Enter own name.]";
			}
			if (OverworldManager.instance.dialogController == 19 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Calvin, Ogasawara.\"";
			}
			if (OverworldManager.instance.dialogController == 20 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "The guard laughed. \"An islander? Then you're technically a metropolis citizen. No passport?\"";
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Calvin shook his head. \"Never got one.\"";
			}
			if (OverworldManager.instance.dialogController == 19 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"Jiro, Hong Kong.\"";
			}
			if (OverworldManager.instance.dialogController == 20 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "The guard stared at him. \"Lone kid from the mainland... \"";
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "";
			}
			if (OverworldManager.instance.dialogController == 22) {
				OverworldManager.instance.text = "";
			}
			if (OverworldManager.instance.dialogController == 23) {
				OverworldManager.instance.text = "";
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "Search the back of the station.";
				OverworldManager.instance.response2 = "Look around the damp walls.";
			} 
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "There was no way the docking station could have been fully secure, Calvin thought. There had to a way out, somewhere.";
			}
			if (OverworldManager.instance.dialogController == 25 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Returning to the very back of the room, where him and the sailors had been dropped off, he began feeling around the edge of the docking station.";
			}
			if (OverworldManager.instance.dialogController == 26 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Somewhere in the darkness, there had to be a way out.";
			}
			if (OverworldManager.instance.dialogController == 27 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "If only he got out of the room, he could then find some other way into the city.";
			}
			if (OverworldManager.instance.dialogController == 28 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Right below the edge Calvin felt a cabel hanging out of a crack in the platform.";
			}
			if (OverworldManager.instance.dialogController == 29 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Pulling on it made tiny sparks fly out of the crack, trailing down far below towards the water. A long drop, he thought, a few dozen meters at best.";
			}
			if (OverworldManager.instance.dialogController == 30 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "But the light revealed something else to him.";
			}
			if (OverworldManager.instance.dialogController == 31 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " Another ledge.";
			}
			if (OverworldManager.instance.dialogController == 32 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "A platform just below, shrouded in darkness.";
			}
			if (OverworldManager.instance.dialogController == 33 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "He looked behind him to find that the crowd had shrunk. Only a handful sailors remained in the station.";
			}
			if (OverworldManager.instance.dialogController == 34 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Either Calvin lept down, or he'd get caught by the guards.";
			}
			if (OverworldManager.instance.dialogController == 35 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "With a tight grip he took a hold of the cable, and lowered himself into nothingness, ignoring the flickering sparks and hoping that his weak arms would hold him just long enough.";
			}
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Not straying too far from the crowd, Calvin felt around the faintly lit up walls in search of something, anything that could help him.";
			}
			if (OverworldManager.instance.dialogController == 25 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "A box.";
			}
			if (OverworldManager.instance.dialogController == 26 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " Some kind of old control panel, covered in a layer of wet fungus and what he could only describe as sticky seaweed.";
			}
			if (OverworldManager.instance.dialogController == 27 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "The tiny screen showed a few number, and had a couple of buttons placed on its outer rim.";
			}
			if (OverworldManager.instance.dialogController == 28 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Calvin found one more interesting than the others. \"Ventilation.\" " +
					"Seeing it made him look around the room, instantly noticing a number of large pipes.";
			}
			if (OverworldManager.instance.dialogController == 29 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Worth a shot.";
			}
			if (OverworldManager.instance.dialogController == 30 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Pressing the button opened up a menu. The only button then that seemed to do anything was the one entitled, \"INCREASE AIR FLOW\"";
			}
			if (OverworldManager.instance.dialogController == 31 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "A creeking noise emminated from the wall, before a crevice blew open, along with heaps of dust and mold.";
			}
			if (OverworldManager.instance.dialogController == 32 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "A crevice just big enough to fit Calvin.";
			}
			if (OverworldManager.instance.dialogController == 33 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Wasting no time, and not wanting to get caught by the guard who definitely must have heard the high-pitched noise, Calvin slithered his way into the pipe, and continued into the darkness.";
			}
			if (OverworldManager.instance.dialogController == 34 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.dialogController = 36;
			}
			if (OverworldManager.instance.dialogController == 36) {
				OverworldManager.instance.text = "DARKNESS, THEN MEETING";
				if (tempBool == true){
					OverworldManager.instance.fadeToWhite = true;
					OverworldManager.instance.playMusic(1);
					tempBool = false;
				}
			}

			/*if (OverworldManager.instance.dialogController == 3) {
				OverworldManager.instance.eraseLog();
				OverworldManager.instance.response1picked = false;
				OverworldManager.instance.text = "He did not have a passport.";
				if (ready == true){
					StartCoroutine(pause(5.0f));
				}
			}*/
			/*if (OverworldManager.instance.dialogController == 3) {
				OverworldManager.instance.response1 = ">Continue";
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.dialogController = 3;
			}
			if (OverworldManager.instance.dialogController == 3 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.response1picked = false;
				OverworldManager.instance.text = "";
				OverworldManager.instance.updateLog();
			
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.dialogController = 4;
			}
			if (OverworldManager.instance.response1picked == true && OverworldManager.instance.dialogController == 4) {
				OverworldManager.instance.eraseLog();
				OverworldManager.instance.coding++;
				OverworldManager.instance.responseWanted = false;
				OverworldManager.instance.text = " " +
					" ";
				OverworldManager.instance.updateLog();
				OverworldManager.instance.dialogController++;
				OverworldManager.instance.response1picked = false;
			}*/
		}
	}
	public IEnumerator pause(float delay) {
		ready = false;
		yield return new WaitForSeconds(delay);
		OverworldManager.instance.i = 0;
		OverworldManager.instance.dialogController = 0;
		
		ready = true;
	}
}
