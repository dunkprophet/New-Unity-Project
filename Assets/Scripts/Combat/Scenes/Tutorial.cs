using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public bool oneTime = false;
	public bool twoTime = false;
	// Use this for initialization
	void Start () {

		OverworldManager.instance.textBoxShown = true;

		OverworldManager.instance.freeMode = false;

		OverworldManager.instance.i = 0;
		OverworldManager.instance.dialogController = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.menuStarted == false) {
			
			
			
			if (OverworldManager.instance.dialogController < 0) {
				
				OverworldManager.instance.i = 0;
				OverworldManager.instance.dialogController = 0;
				
			}
			
			if (OverworldManager.instance.dialogController == 0) {
				
				GameManager.instance.textRunning = true;	
				//Shown in textbox
				//scene1Background
				OverworldManager.instance.text = "*Attempting connection*";
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
				OverworldManager.instance.text = " It's based on very real intrusions done by criminals.\"";
				OverworldManager.instance.deleteOldText = true;
			}
			if (OverworldManager.instance.dialogController == 18) {
				OverworldManager.instance.text = "She went on, mumbling, \"However, what we do here is completely lawful. Obviously.\"";
			}
			if (OverworldManager.instance.dialogController == 19) {
				OverworldManager.instance.text = "\"Do you want me to show you the basics?\"";
				if (oneTime == false){
					oneTime = true;
					OverworldManager.instance.programList.Add (OverworldManager.instance.Breaker);
				}
			}
			if (OverworldManager.instance.dialogController == 20) {
				OverworldManager.instance.text = "\"...\"";
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "\"Sure.\"";
				OverworldManager.instance.response2 = "\"I can probably figure it out by myself.\"";
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"I can probably figure it out by myself.\"";
			}
			if (OverworldManager.instance.dialogController == 22 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "She chuckled. \"Ah, okay. Well, good luck to you then! " +
					" If you want to talk, you can always come by the school, or message me. So... I'll see you around!\"";
			}
			if (OverworldManager.instance.dialogController == 23 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Her voice disappeared, leaving in its stead a thick silence." +
					" The dark void of the netscape seemed to lack any real ambience, so Calvin put on a song from his hard drive," +
					" and started the bot-match.";

			}
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "";
				GameManager.instance.textRunning = false;
				OverworldManager.instance.dialogController = 144;
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Sure.\"";
			}
			if (OverworldManager.instance.dialogController == 22 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Great! But before we get into the actual fighting, I want to show you how to set everything up.\"";
			}
			if (OverworldManager.instance.dialogController == 23 && OverworldManager.instance.response1picked == true) {
				GameManager.instance.textRunning = false;
				OverworldManager.instance.text = "\"To your left is the setup box. This will show commands you can issue to your programs" +
					", as well as allow you to place your programs at the beginning of a match.\"";
			}
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"You see those white boxes on the battlefield? Those are Input Points. " +
					"You can place one program on every Input Point.\"";
			}
			if (OverworldManager.instance.dialogController == 25 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"On this map there is only a single Input Point.\"";
			}
			if (OverworldManager.instance.dialogController == 26 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"I just sent you one of my cheap programs. It's called 'Breaker.'\"";
			}
			if (OverworldManager.instance.dialogController == 27 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Try placing it on the Input Point.\"";
			}
			if (OverworldManager.instance.dialogController == 28 && GameManager.instance.players.Count == 0) {
				OverworldManager.instance.textPrint = "\"Try placing it on the Input Point.\"";
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.freeMode = true;
				OverworldManager.instance.text = "";
				OverworldManager.instance.i = 0;
			}
			if (OverworldManager.instance.dialogController == 28 && OverworldManager.instance.freeMode == true && GameManager.instance.players.Count == 1) {
				if (twoTime == false){
					twoTime = true;
					OverworldManager.instance.textPrint = "";
				}
				OverworldManager.instance.text = "\"Awesome! Now you can start the match against those bots.\"";
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.freeMode = false;
			}
			if (OverworldManager.instance.dialogController == 29) {
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.freeMode = true;
				OverworldManager.instance.textPrint = "\"Awesome! Now you can start the match against those bots.\"";
				OverworldManager.instance.text = "";
				OverworldManager.instance.i = 0;
			}
			if (OverworldManager.instance.dialogController == 29 && GameManager.instance.matchStarted == true) {
				if (twoTime == true){
					twoTime = false;
					OverworldManager.instance.textPrint = "";
				}
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				GameManager.instance.textRunning = true;
				OverworldManager.instance.text = "\"Alright, you started the match. " +
					"Now, as you can see, these battles are kind of similar to those old turn-based games we used to play back home.\"";
				OverworldManager.instance.dialogController = 31;
			}
			if (OverworldManager.instance.dialogController == 31 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
			}
			if (OverworldManager.instance.dialogController == 32 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.text = "\"But the major difference is this: Server Space.\"";
			}
			if (OverworldManager.instance.dialogController == 33 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"Your programs can each take one step at a time on the Server. " +
					"Each step increases that programs Server Space by one. " +
					"";
			}
			if (OverworldManager.instance.dialogController == 34 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = "But a program can only have a certain amount of Server Space before it reaches max-capacity.\"";
				GameManager.instance.textRunning = false;
			}
			if (OverworldManager.instance.dialogController == 35 && GameManager.instance.round <= 1) {

				OverworldManager.instance.textBoxShown = true;
				// Server Space tydligt är hp
				// Nämn att plattorna är liv
				OverworldManager.instance.text = "\"Try moving around a bit, and see for yourself.\"";
				if (OverworldManager.instance.textPrint == "\"Try moving around a bit, and see for yourself.\""){

					OverworldManager.instance.freeMode = true;
				}
			} //\"This will cause it to lose old Server Space, in favor of new.\"
			//\"The trick is then to always be on the move, since moving essentially replenishes the program's health.\"
			// \"And then attacking is all about-\"
			//Calvin, I have to go. I'll mail you later. Good luck!\"
			if (OverworldManager.instance.dialogController == 35 && GameManager.instance.round >= 2) {
				if (twoTime == false){
					twoTime = true;
					OverworldManager.instance.textPrint = "";
					OverworldManager.instance.i = 0;
				}
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				GameManager.instance.textRunning = true;
				OverworldManager.instance.text = "\"You see those colored tiles following the Breaker? Those are its Server Space.\"";
			}
			if (OverworldManager.instance.dialogController == 36 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"The Breaker has a max capacity of 4 tiles. Each represents one point of damage the program can recieve.";
			}
			if (OverworldManager.instance.dialogController == 37 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " If the program loses all of its tiles, it loses connection to the server, and disappears.\"";
			}
			if (OverworldManager.instance.dialogController == 38 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"So the trick is to always be on the move, since moving essentially replenishes your health!";
			}
			if (OverworldManager.instance.dialogController == 39 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " Or if the program's at max health, moving keeps it at max health!\"";
			}
			if (OverworldManager.instance.dialogController == 40 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "\"And as you can see, the box on your left has changed. " +
					"Now, the stats of your selected program are shown, " +
					"along with some buttons.\"";
			}
			if (OverworldManager.instance.dialogController == 41 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "\"These buttons have three different functions.\"";
			}
			if (OverworldManager.instance.dialogController == 42 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"Attacking, ";
			}
			if (OverworldManager.instance.dialogController == 43 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "skipping your turn without moving, ";
			}
			if (OverworldManager.instance.dialogController == 44 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = "and undoing a program's movement\".";
			}
			if (OverworldManager.instance.dialogController == 45 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "\"But only use these if you really want to pull off a complicated move.\"";
			}
			if (OverworldManager.instance.dialogController == 46 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "\"The most efficient and fun way to control your programs is to simply click on the battlefield to move, " +
					"and then attack a tile around the program to end its turn.\"";
			}
			if (OverworldManager.instance.dialogController == 47 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.text = "\"And make sure to position your program correctly at the end of a turn! " +
					"If there's one thing I understand, it's that attacking is all about-\"";
			}
			if (OverworldManager.instance.dialogController == 48 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"Oh shit. ";
			}
			if (OverworldManager.instance.dialogController == 49 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = "Calvin, I really have to go, sorry. I'll mail you later. Good luck!";
			}
			if (OverworldManager.instance.dialogController == 50 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.text = "\"Hold on,\" Calvin said. \"What did you mean with-\"";
			}
			if (OverworldManager.instance.dialogController == 51 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.text = "*Aralia_3 left the server*";
			}
			if (OverworldManager.instance.dialogController == 52 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = false;
				OverworldManager.instance.deleteOldText = true;
				GameManager.instance.textRunning = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.text = "\"Well, shit.\"";
			}
			if (OverworldManager.instance.dialogController == 53 && GameManager.instance.matchStarted == true) {
				OverworldManager.instance.freeMode = true;
				OverworldManager.instance.textBoxShown = false;
				GameManager.instance.textRunning = false;
				OverworldManager.instance.dialogController = 144;
			}

			if (OverworldManager.instance.dialogController == 144) {
				
				GameManager.instance.textRunning = false;
				OverworldManager.instance.textBoxShown = true;
				OverworldManager.instance.dialogController = 145;
				OverworldManager.instance.text = "";
				OverworldManager.instance.freeMode = true;
			}
			if (OverworldManager.instance.dialogController == 145 && GameManager.instance.deadAIplayers == 2){
				OverworldManager.instance.netscapeNodes.Add (10);
				OverworldManager.instance.dialogController = 146;
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
