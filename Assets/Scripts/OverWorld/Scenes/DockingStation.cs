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
		if (OverworldManager.instance.scene == 1 && OverworldManager.instance.freeMode == false && OverworldManager.instance.tutorialDone == false) {

			
			if (OverworldManager.instance.dialogController < 0) {
				OverworldManager.instance.i = 0;
				OverworldManager.instance.dialogController = 0;



			}
			
			if (OverworldManager.instance.dialogController == 0) {
				//Shown in textbox
				//scene1Background
				OverworldManager.instance.text = "初日\nDay 1";
				//OverworldManager.instance.lerpingColor = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), OverworldManager.instance.fadeSpeed * Time.deltaTime);



			}
			if (OverworldManager.instance.dialogController == 1) {
				OverworldManager.instance.text = "Every breath he took felt like mold invading his lungs.";

			}
			if (OverworldManager.instance.dialogController == 2) {
				OverworldManager.instance.text = "The damp air, most likely made from some foul combination of fumes and factory sludge, melted with the stench of sweat and shit.";

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
			if (OverworldManager.instance.dialogController == 7) {
				OverworldManager.instance.text = "The ship's crew held up their passports, after which they were waved along by the security.";

			}
			if (OverworldManager.instance.dialogController == 8) {
				OverworldManager.instance.text = "While those like Calvin, those without passports, were examined in great detail.";

			}
			if (OverworldManager.instance.dialogController == 9) {
				OverworldManager.instance.text = " \"Your name and occupation.\"";

			}
			if (OverworldManager.instance.dialogController == 10) {
				OverworldManager.instance.text = "\"Purpose for coming to Chiba.\"";

			}
			if (OverworldManager.instance.dialogController == 11) {
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
			if (OverworldManager.instance.dialogController == 12) {
				OverworldManager.instance.text = "It didn't matter either way. They let everyone in.";
			}
			if (OverworldManager.instance.dialogController == 13) {
				OverworldManager.instance.text = "From the ship Calvin had travelled on came a drove of Chinese civilians fleeing the war, people from the States searching for better lives, and even a few Indonesian families trying to escape the waste.";
			}
			if (OverworldManager.instance.dialogController == 14) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "In Chiba, everyone had a place.";
			}
			if (OverworldManager.instance.dialogController == 15) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = ".. or so he had heard.";
			}
			if (OverworldManager.instance.dialogController == 16) {

				OverworldManager.instance.text = "For an hour he stood in line looking on as they crowd slowly melted through the arching entrances to the city... long enough a time to get used to both the smell of sewers and the monotony of queuing.";
			}
			if (OverworldManager.instance.dialogController == 17) {

				OverworldManager.instance.text = "When he finally reached the checkpoint his body felt numb to the core.";
			}
			if (OverworldManager.instance.dialogController == 18) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "\"Kid,\" the guardsman said, ready to write down whatever he said onto a clipboard. \"Name and origin.\"";
			}
			if (OverworldManager.instance.dialogController == 19) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = "\n\n\"...\"";
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "[Truth] Calvin, Ogasawara.";
				OverworldManager.instance.response2 = "[Lie] Jiro, Hong Kong.";
				//OverworldManager.instance.response3 = "[Enter own name.]";
			}
			if (OverworldManager.instance.dialogController == 20 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Calvin, Ogasawara.\"";
				OverworldManager.instance.govermentKnowsName = true;
				OverworldManager.instance.choices.Add (OverworldManager.instance.govermentKnowsName);
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "The guard laughed. \"An islander? Then you're practically a metropolis citizen. No passport?\"";
			}
			if (OverworldManager.instance.dialogController == 22 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Calvin shook his head. \"Never got one.\"";
			}
			if (OverworldManager.instance.dialogController == 23 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Daijoubu. I'll just register you as a transfer.\" He scribbled a few lines on a form before handing it over.";
			}
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Welcome to Chiba City.\"";
			}
			if (OverworldManager.instance.dialogController == 25 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.dialogController = 26;
			}
			if (OverworldManager.instance.dialogController == 20 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"Jiro, Hong Kong.\"";
				OverworldManager.instance.govermentKnowsName = false;
				OverworldManager.instance.choices.Add (OverworldManager.instance.govermentKnowsName);
			}
			if (OverworldManager.instance.dialogController == 21 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "The guard shook his head. \"Lone kid from the mainland...\"";
			}
			if (OverworldManager.instance.dialogController == 22 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " He pulled out a note from his pocket, and handed it over.";
			}
			if (OverworldManager.instance.dialogController == 23 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"Take this and go directly to the GRC embassy. Unless you want to roam around without an ID.\"";
			}
			if (OverworldManager.instance.dialogController == 24 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "That was the idea.";
			} 
			if (OverworldManager.instance.dialogController == 25 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "The guard grabbed Calvin by the shoulder, saying, \"Don't do anything stupid,\" before letting him into the city.";
			}
			if (OverworldManager.instance.dialogController == 26) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 27) {
				OverworldManager.instance.text = "美浜区\nMihama-ku";
				if (tempBool == true){
					OverworldManager.instance.audio.PlayOneShot(OverworldManager.instance.train);
					//OverworldManager.instance.audio.PlayOneShot(OverworldManager.instance.song1);
					tempBool = false;
				}
			}
			if (OverworldManager.instance.dialogController == 28) {
				OverworldManager.instance.text = "He listened to the sounds of the city.";
				tempBool = true;
			}
			if (OverworldManager.instance.dialogController == 29) {
				OverworldManager.instance.text = "Trains rolling by, clonking noise of machinery and tools in the factories around him. All cold, and lifeless.";
			}
			if (OverworldManager.instance.dialogController == 30) {
				OverworldManager.instance.text = "And not a human around.";
				OverworldManager.instance.background = OverworldManager.instance.scene1Background2;
			}
			if (OverworldManager.instance.dialogController == 31) {
				if (tempBool == true){
					OverworldManager.instance.fadeToWhite = true;
					tempBool = false;
				}
				OverworldManager.instance.text = "Calvin walked the narrow streets alone, sliding by booming ventilation pipes and giant closed gates. He only got a tiny glipse at the darkly green sky above, through a web of train-rails and steel beams.";
			}
			if (OverworldManager.instance.dialogController == 32) {
				tempBool = true;
				OverworldManager.instance.text = "He moved onwards, with a single question stuck in his head.";
			}
			if (OverworldManager.instance.dialogController == 33) {
				OverworldManager.instance.text = " Where would he go?";
			}
			if (OverworldManager.instance.dialogController == 34) {
				OverworldManager.instance.text = "He needed someplace to sleep. And some food.";
			}
			if (OverworldManager.instance.dialogController == 35) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Both of those required money.";
			}
			if (OverworldManager.instance.dialogController == 36) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = ".. which he didn't have.";
			}
			if (OverworldManager.instance.dialogController == 37) {
				OverworldManager.instance.text = "As he wandered through a dimly lit alleyway, thinking of ways to get himself some credits, two voices began yelling from around a corner. ";
			}
			if (OverworldManager.instance.dialogController == 38) {
				OverworldManager.instance.text = "\"Have you gone senile, ossan?\"";
			}
			if (OverworldManager.instance.dialogController == 39) {
				OverworldManager.instance.text = "\"For wanting my due payment? No.\"";
			}
			if (OverworldManager.instance.dialogController == 40) {
				OverworldManager.instance.text = "Calvin saw two groups of men glaring at each other at the end of the alley.";
				OverworldManager.instance.guiAlpha = 1;
			}
			if (OverworldManager.instance.dialogController == 41) {
				OverworldManager.instance.text = "\"Go fuck youself,\" said the guy to the right, a gangly fellow wearing a black jacket and a cloth around his face.";
			}
			if (OverworldManager.instance.dialogController == 42) {
				OverworldManager.instance.text = "\"Look. If you won't pay me, then at least point me towards someone who will.\" To the left stood an older man with unkempt hair and an outfit best suited a homeless person. Both of them seemed to speak for their respective groups.";
			}
			if (OverworldManager.instance.dialogController == 43) {
				OverworldManager.instance.text = "The man spat on the ground. \"Dogs like you should be paid in lead, you piece of-\"";
			}
			if (OverworldManager.instance.dialogController == 44) {
				OverworldManager.instance.text = "One of his companions interupted him.";
			}
			if (OverworldManager.instance.dialogController == 45) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Then, after a short pause, the masked man pointed towards the other end of the alley.";
			}
			if (OverworldManager.instance.dialogController == 46) {
				OverworldManager.instance.text = " \"You brought back-up.";
			}
			if (OverworldManager.instance.dialogController == 47) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " You fucker.\"";
			}
			if (OverworldManager.instance.dialogController == 48) {
				OverworldManager.instance.text = "One of the thugs started sprinting towards Calvin.";
			}
			if (OverworldManager.instance.dialogController == 49) {
				OverworldManager.instance.text = "The scraggly man took a step back, bearing a shocked expression. \"Oh, for the love of,\" he said, and pulled out a gun. \"We've been compromised, move in!\"";
			}
			if (OverworldManager.instance.dialogController == 50) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Before Calvin could react, the Yakuza member took a hold of him by the neck, pressing a muzzle against his forehead.";
			}
			if (OverworldManager.instance.dialogController == 51) {
				OverworldManager.instance.text = "\"What the-\" he said.";
			}
			if (OverworldManager.instance.dialogController == 52) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " \"A fucking kid...?\"";
			}
			if (OverworldManager.instance.dialogController == 53) {
				OverworldManager.instance.text = "Several gunshots rang in Calvin's ears, and echoed between the concrete walls.";
			}
			if (OverworldManager.instance.dialogController == 54) {
				OverworldManager.instance.text = "Followed by silence.";
			}
			if (OverworldManager.instance.dialogController == 55) {
				OverworldManager.instance.text = "The Yakuza let go of Calvin, and turned to look at the carnage.";
			}
			if (OverworldManager.instance.dialogController == 56) {
				OverworldManager.instance.text = "\"...\"";
			}
			if (OverworldManager.instance.dialogController == 57) {
				OverworldManager.instance.text = "Five men, filled with bullets. A team of policemen clad in full armor and heavy rifles stood over them, checking for signs of life.";
			}
			if (OverworldManager.instance.dialogController == 58) {
				OverworldManager.instance.text = "Every step they took drenched their boots in blood.";
			}
			if (OverworldManager.instance.dialogController == 59) {
				OverworldManager.instance.text = "The Yakuza looked on, pale-faced. \"Kuso.\" Calvin saw him clenching his gun, aiming it towards the cops, his hands shaking.";
			}
			if (OverworldManager.instance.dialogController == 60) {
				OverworldManager.instance.text = "\"They'll pay for this,\" he said, lowering his weapon. \"Follow me.\"";
			}
			if (OverworldManager.instance.dialogController == 61) {
				OverworldManager.instance.text = "\"W-What?\" Calvin stuttered as the Yakuza grabbed him by the arm and started running.";
			}
			if (OverworldManager.instance.dialogController == 62) {
				OverworldManager.instance.text = "\"Unless you want to get shot, you'll do as I say, kid.\"";
			}
			if (OverworldManager.instance.dialogController == 63) {
				OverworldManager.instance.text = "Assuming it to be his best choice, Calvin went along with his demands. \"Kuso!\" the Yakuza rambled on as they ran through the narrow alleyways. \"I told Ryota those mercs were bad news, but he just had to go and...\"";
			}
			if (OverworldManager.instance.dialogController == 64) {
				OverworldManager.instance.text = "\"...\" He gritted his teeth.";
				if (tempBool == true){
					tempBool = false;
					OverworldManager.instance.fadeToBlack = true;
				}
			}
			if (OverworldManager.instance.dialogController == 65) {
				tempBool = true;
				OverworldManager.instance.text = "\"KUSO!\"";
			}
			if (OverworldManager.instance.dialogController == 66) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 67) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 68) {
				OverworldManager.instance.text = "As they reached a long chain-link fence below the tracks of the Keiyo Line, the man stopped.";
			}
			if (OverworldManager.instance.dialogController == 69) {
				OverworldManager.instance.text = "\"My day wasn't supposed to go like this,\" he mumbled, taking out a cigarette.";
			}
			if (OverworldManager.instance.dialogController == 70) {
				OverworldManager.instance.text = "He leaned against a pole as he lit it. \"I had a date with Lina. Real nice girl.\"";
			}
			if (OverworldManager.instance.dialogController == 71) {
				OverworldManager.instance.text = "He blew out some smoke. \"Then you came and got my boss killed.\"";
			}
			if (OverworldManager.instance.dialogController == 72) {
				OverworldManager.instance.text = "Calvin's heart skipped a beat.";
				OverworldManager.instance.guiAlpha = 0;
			}
			if (OverworldManager.instance.dialogController == 73) {
				OverworldManager.instance.text = "\"Don't worry,\" he said, giving a quick laugh at Calvin's reaction. \"I won't hold a grudge. Those cops were going to fuck us over one of these days anyhow. Except now I survived.\"";
			}
			if (OverworldManager.instance.dialogController == 74) {
				OverworldManager.instance.text = "\"...for some stupid fucking reason.\"";
			}
			if (OverworldManager.instance.dialogController == 75) {
				OverworldManager.instance.text = "Calvin didn't say a thing. He only stared at the gangster, hoping not to be shot.";
			}
			if (OverworldManager.instance.dialogController == 76) {
				OverworldManager.instance.text = "\"The reason why you're here is because I need you.\"";
			}
			if (OverworldManager.instance.dialogController == 77) {
				OverworldManager.instance.text = "Calvin flinched when the man reached out a hand.";
			}
			if (OverworldManager.instance.dialogController == 78) {
				OverworldManager.instance.text = "\"Name's Saburo. You?\"";
			}
			if (OverworldManager.instance.dialogController == 79) {
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "\"I'm Calvin.\"";
				OverworldManager.instance.response2 = "\"...\"";
				OverworldManager.instance.text = "\"...\"";
			}
			if (OverworldManager.instance.dialogController == 80 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"I'm-\"";
			}
			if (OverworldManager.instance.dialogController == 80 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"...\"";
			}
			if (OverworldManager.instance.dialogController == 81 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Actually,\" Saburo interuppted him. \"I don't care. Just give me your adress.\"";
			}
			if (OverworldManager.instance.dialogController == 81 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"Fine, whatever,\" Saburo said. \"I don't care. Just give me your adress.\"";
			}
			if (OverworldManager.instance.dialogController == 82) {
				OverworldManager.instance.text = "Calvin paused for a second, before mumbling the old nickname he'd used when he registered for his first e-mail at age nine.";
			}
			if (OverworldManager.instance.dialogController == 83) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 84) {
				OverworldManager.instance.text = "\"Alright,\" he said, recording it on a hidden wrist-gear. \"I'll keep in touch. Until then, just stay alive.\"";
			}
			if (OverworldManager.instance.dialogController == 85) {
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "\"Wait.\"";
				OverworldManager.instance.response2 = "\"...\"";
				OverworldManager.instance.text = "\"...\"";
			}
			if (OverworldManager.instance.dialogController == 86 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.responseWanted = true;
				OverworldManager.instance.response1 = "\"What do you want from me?\"";
				OverworldManager.instance.response2 = "\"Thank you.\"";
				OverworldManager.instance.text = "\"Yeah?\"\n\n\"...\"";
			}
			if (OverworldManager.instance.dialogController == 86 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.dialogController = 93;
			}
			if (OverworldManager.instance.dialogController == 87 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"What do you want from me?\"";
			}
			if (OverworldManager.instance.dialogController == 88 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Saburo shook his head. \"I don't want anything. I just need you to be my witness.\"";
			}
			if (OverworldManager.instance.dialogController == 89 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "He took a deep drag of his cigarette. \"I'll contact you when the it's time. Until then, as I said, I just need you to stay alive.\"";
			}
			if (OverworldManager.instance.dialogController == 90 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "\"Sounds good?\"";
			}
			if (OverworldManager.instance.dialogController == 91 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "Calvin nodded.";
			}
			if (OverworldManager.instance.dialogController == 92 && OverworldManager.instance.response1picked == true) {
				OverworldManager.instance.text = "He smirked in return. \"Alright. See ya.\"";
			}
			if (OverworldManager.instance.dialogController == 86 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "\"Thank you.\"";
			}
			if (OverworldManager.instance.dialogController == 87 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.text = "Saburo smirked. \"No problem. I kind of need you to stay alive, after all. See ya.\"";
			}
			if (OverworldManager.instance.dialogController == 88 && OverworldManager.instance.response2picked == true) {
				OverworldManager.instance.dialogController = 93;
			}
			if (OverworldManager.instance.dialogController == 93) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Calvin stayed by the Keiyo Line, watching the pale Yakuza disappear into the factory smog.";
			}
			if (OverworldManager.instance.dialogController == 94) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " \"...what the hell have I fallen into?\"";
			}
			if (OverworldManager.instance.dialogController == 95) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 96) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 97) {
				OverworldManager.instance.text = "稲毛区\nInage-ku";
				if (tempBool == true){
					OverworldManager.instance.fadeToWhite = true;
					tempBool = false;
				}
			}
			if (OverworldManager.instance.dialogController == 98) {
				OverworldManager.instance.text = "To his suprise, even the streets of Chiba's more urban areas lay barren in the darkness.";
				tempBool = true;

			}
			if (OverworldManager.instance.dialogController == 99) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Calvin always imagined the city to be filled at all times, like a machine that never sleeps.";
			}
			if (OverworldManager.instance.dialogController == 100) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " But at four in the morning it seemed like even Inage rested, despite it being the city's most densely populated ward.";
			}
			if (OverworldManager.instance.dialogController == 101) {
				OverworldManager.instance.text = "Calvin needed someplace to rest too.";
			}
			if (OverworldManager.instance.dialogController == 102) {
				OverworldManager.instance.text = "He found a decent site handling Chiba apartment contracts, the cheap and off-the-grid ones, by looking through some old forum threads. A place hidden in the shadow of some skyscrapers had caught his attention.";
			}
			if (OverworldManager.instance.dialogController == 103) {
				OverworldManager.instance.text = "It took less than an hour to get a hold of the owner, and sign the contract.";
			}
			if (OverworldManager.instance.dialogController == 104) {
				OverworldManager.instance.text = "That was the easy part.";
			}
			if (OverworldManager.instance.dialogController == 105) {
				OverworldManager.instance.text = "Hard part was finding the correct street.";
			}
			if (OverworldManager.instance.dialogController == 106) {
				OverworldManager.instance.text = "All signs were in Japanese, of which Calvin only had a basic understanding.";
			}
			if (OverworldManager.instance.dialogController == 107) {
				OverworldManager.instance.text = "He never did too well in school- the main reason why he's wandering the streets of Chiba in the middle of night instead of sleeping at some fancy student-complex.";
			}
			if (OverworldManager.instance.dialogController == 108) {
				OverworldManager.instance.text = "...the dark tunnel-like passageways of Inaga were starting to get to him.";
			}
			if (OverworldManager.instance.dialogController == 109) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "He needed sleep.";
			}
			if (OverworldManager.instance.dialogController == 110) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = " Badly.";
			}
			if (OverworldManager.instance.dialogController == 111) {
				OverworldManager.instance.text = "On the site it had said, \"ENTRANCE HIDDEN BEHIND ELECTRONICS STORE,\" which only made it harder to find. Every street in Inaga had a store like that.";
			}
			if (OverworldManager.instance.dialogController == 112) {
				OverworldManager.instance.text = "Having walked around for almost two hours, Calvin finally crossed paths with someone. A man in a cape.";
			}
			if (OverworldManager.instance.dialogController == 113) {
				OverworldManager.instance.text = "He spotted him walking in circles by a street corner, mumbling to himself.";
			}
			if (OverworldManager.instance.dialogController == 114) {
				OverworldManager.instance.text = "\"Excuse me,\" Calvin said. \"Do you know where 'Sadu-suto-rito' is?\"";
			}
			if (OverworldManager.instance.dialogController == 115) {
				OverworldManager.instance.text = "The man did not respond.";
				OverworldManager.instance.guiAlpha = 1;
			}
			if (OverworldManager.instance.dialogController == 116) {
				OverworldManager.instance.text = "\"He won't answer you,\" a raspy voice said behind Calvin. It came from an old man leaning out of a nearby archway. \"Mind's all scrambled.\"";
			}
			if (OverworldManager.instance.dialogController == 117) {
				OverworldManager.instance.text = "Calvin turned to the mumbling man, then to the old guy by the building. \"Should we call someone?\"";
			}
			if (OverworldManager.instance.dialogController == 118) {
				OverworldManager.instance.text = "\"Nah,\" he said, \"they'll swing by in the morning to pick him up. Until then I suggest leaving him alone. You can never know how a Tripper will react to interference.\"";
			}
			if (OverworldManager.instance.dialogController == 119) {
				OverworldManager.instance.text = "Calvin took a step back.";
			}
			if (OverworldManager.instance.dialogController == 120) {
				OverworldManager.instance.text = "The old man chuckled. \"You're that kid, right? The one who signed up for box 106?\"";
			}
			if (OverworldManager.instance.dialogController == 121) {
				OverworldManager.instance.text = "Calvin nodded and walked up to him. \"Yes! You're Hachi?\"";
			}
			if (OverworldManager.instance.dialogController == 122) {
				OverworldManager.instance.text = "\"Yeah. Figured you'd be wandering about here, so I decided to take a look.\" The old man waved him in. \"Come on, I'll show you to your room.\"";
			}
			if (OverworldManager.instance.dialogController == 123) {
				OverworldManager.instance.text = "Calvin followed him inside the building, and up a long staircase. Hachi went on about rules and what-not about the apartment. \"No pets\", \"No drugs\", and so on. Calvin only barely listened. He was just happy to finally be close to a bed.";
				if (tempBool == true){
					tempBool = false;
					OverworldManager.instance.fadeToBlack = true;
				}
			}
			if (OverworldManager.instance.dialogController == 124) {
				OverworldManager.instance.text = "When they ultimately arrived at a tiny door enscribed with the numbers \"106\", the old man gave him a pat on the back.";
				tempBool = true;
			}
			if (OverworldManager.instance.dialogController == 125) {
				OverworldManager.instance.text = "\"35000¥ each month, delivered to my office. Break the rules, you're out. Got it?\"";
			}
			if (OverworldManager.instance.dialogController == 126) {
				OverworldManager.instance.text = "Calvin nodded, and Hachi gave him the keys.";
				OverworldManager.instance.guiAlpha = 0;
			}
			if (OverworldManager.instance.dialogController == 127) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 128) {
				OverworldManager.instance.text = "...";
			}
			if (OverworldManager.instance.dialogController == 128) {
				OverworldManager.instance.text = "2日目\nDay 2";
			}
			if (OverworldManager.instance.dialogController == 129) {
				OverworldManager.instance.text = "Still tired.";
			}
			if (OverworldManager.instance.dialogController == 130) {
				OverworldManager.instance.text = "Still dead inside.";
			}
			if (OverworldManager.instance.dialogController == 131) {
				OverworldManager.instance.text = "Calvin groaned as he forced himself to sit up and look out into the everstaying darkness of Chiba City.";
			}
			if (OverworldManager.instance.dialogController == 132) {
				OverworldManager.instance.text = "His new home was pretty nice.";
			}
			if (OverworldManager.instance.dialogController == 133) {
				OverworldManager.instance.text = "A tiny box with a tiny window, holding nothing but a desk and a bed. ";
			}
			if (OverworldManager.instance.dialogController == 134) {
				OverworldManager.instance.text = "...though the bed didn't seem like a real bed, but rather a repurposed solarium of some kind.";
			}
			if (OverworldManager.instance.dialogController == 135) {
				OverworldManager.instance.text = "At least it had a good mattress.";
			}
			if (OverworldManager.instance.dialogController == 136) {
				OverworldManager.instance.text = "Calvin sighed, and stood up, beginning to immedietly unpack his few belongings.";
			}
			if (OverworldManager.instance.dialogController == 137) {
				OverworldManager.instance.deleteOldText = false;
				OverworldManager.instance.text = "Some clothes.";
			}
			if (OverworldManager.instance.dialogController == 138) {
				OverworldManager.instance.text = " A toothbrush.";
			}
			if (OverworldManager.instance.dialogController == 139) {
				OverworldManager.instance.deleteOldText = true;
				OverworldManager.instance.text = ".. and his old laptop.";
			}
			if (OverworldManager.instance.dialogController == 140) {
				OverworldManager.instance.text = "It was a pre-crash model. Couldn't get it anywhere else.";
			}
			if (OverworldManager.instance.dialogController == 141) {
				OverworldManager.instance.text = "He even coded it himself, following a guide he found online.";
			}
			if (OverworldManager.instance.dialogController == 142) {
				OverworldManager.instance.text = "But it worked as good as he wanted it to.";
			}
			if (OverworldManager.instance.dialogController == 143) {
				OverworldManager.instance.text = "Not really having anything in mind, Calvin sat down at the desk ready to waste his day in the same way he'd wasted most of his childhood.";
			}
			if (OverworldManager.instance.dialogController == 144) {
				OverworldManager.instance.textBoxShown = false;
				OverworldManager.instance.dialogController = -1;
				OverworldManager.instance.text = "";
				OverworldManager.instance.day = 2;
				OverworldManager.instance.date = "22 September";
				OverworldManager.instance.scene = 2;
				OverworldManager.instance.freeMode = true;

				OverworldManager.instance.programList.Add(OverworldManager.instance.Bug);
				OverworldManager.instance.programList.Add(OverworldManager.instance.GOD);
				OverworldManager.instance.mailListBool.Add(true);
				OverworldManager.instance.mailListBool.Add(true);
				OverworldManager.instance.mailListBool.Add(true);

			}


			/*if (OverworldManager.instance.dialogController == 36) {
				OverworldManager.instance.text = "DARKNESS, THEN MEETING";
				if (tempBool == true){
					OverworldManager.instance.fadeToWhite = true;
					OverworldManager.instance.playMusic(1);
					tempBool = false;
				}
			}*/

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
