using UnityEngine;
using System.Collections;

public class Intro_Dialog : MonoBehaviour {
	public GUISkin skin;
	public bool written = true;
	public bool started = false;
	public static bool SplashDone = false;
	int scene = 0;
	
	//public static bool playerTalking = false;
	
	private GUISkin DOS2;
	
	void Start () {
		DOS2 = Resources.Load("DOS2") as GUISkin;
		scene = 1;
		SplashDone = true;
	}
	
	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = transform.position.x;
			OverworldPlayer.instance.moveDestination [2] = transform.position.y;
		}


	}
	
	void OnGUI()
	{
		if (started == false && SplashDone == true){
		if (UnityEngine.Input.GetMouseButtonDown(0)) {
			if (scene < 8 && written == true) {
				scene++;
				written = false;
			}
		}
		if (UnityEngine.Input.GetMouseButtonUp(0)){
			written = true;
		}
		GUI.skin = DOS2;

		GUILayout.BeginArea (new Rect (0, 0, Screen.width/2, Screen.height));
		
		if (scene == 1) {
			
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			
			GUILayout.Label("Welcome to DOS65_");
			GUILayout.EndVertical ();

		} else if (scene == 2) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\>_");
			
			GUILayout.EndVertical ();
		} else if (scene == 3) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\> Run Memoirs.txt_");
			
			GUILayout.EndVertical ();
		} else if (scene == 4) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\> Run Memoirs.txt\n\n\n\n...\n\n\n...\n\n_");
			
			GUILayout.EndVertical ();
		} else if (scene == 5) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\> Run Memoirs.txt\n\n\n\n...\n\n\n...\n\n\nFILE ENCRYPTED\n\nBREAKING ENCRYPTION_");
			
			GUILayout.EndVertical ();
		} else if (scene == 6) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\> Run Memoirs.txt\n\n\n\n...\n\n\n...\n\n\nFILE ENCRYPTED\n\nBREAKING ENCRYPTION" +
				             "\n\n\n...\n\n\n...\n\n\n\n_");
			
			GUILayout.EndVertical ();
		}else if (scene == 7) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			GUILayout.Label ("Welcome to DOS65 \n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
				             "AKIHABARA コンピュータストア CB80 version 1.0\n" +
				             "Copyright 2063, 64, 65 愚かなアメリカ版 Corp.\n\n" +
				             "Volume in drive D is 3F1F2U05P\nDirectory of D:\\\n" +
				             "\nFDOS            <DIR>    09-21-82  6:47p" +
			                 "\nAUTOEXEC BAT      435    09-21-82  6:47p" +
			                 "\nBOOTSECT BIN      512    09-21-82  6:47p" +
			                 "\nCOMMAND  COM   93,963    09-21-82  6:47p" +
			                 "\nCONFIG   SYS      435    09-21-82  6:47p" +
			                 "\nFDOSBOOT BIN      435    09-21-82  6:47p" +
			                 "\nSECTIONL SYS      435    02-12-65  12:22a" +
			                 "\n         9 file(s)       142,038,154 bytes" +
				             "\n         1 dir(s)  1,064,517,632,863 bytes 無料で" +
				             "\n\n\nD:\\> Run Memoirs.txt\n\n\n\n...\n\n\n...\n\n\nFILE ENCRYPTED\n\nBREAKING ENCRYPTION" +
				             "\n\n\n...\n\n\n...\n\n\n\nDecryption successful_");
			
			GUILayout.EndVertical ();
		}

		
		GUILayout.EndArea ();
		
		//START
		
		GUILayout.BeginArea (new Rect (10, 0, Screen.width/2, Screen.height));
		
		if (scene == 8) {
			
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));

			GUILayout.Label ("Memoirs.txt" +
				  "\n\n I'm not writing this as an excuse. I only think its fair for people to hear the other side of the story. " +
				  "\n\n I grew up on an fishing island in the pacific. Beside spending most of my childhood in a dark and secluded house in the back of my tiny hometown, I would often hang around with my small group of friends." +
				  " But as school came to an end, those friends soon disappeared. Some started working, others got into high-schools on the mainland, or across the sea. A few joined the war in China." +
				  " \n\n But I didn't get a job. I didn't get into any of the schools I applied to. And I couldn't go to war." +
				  "\n\n I was stuck.\n");

			if (GUILayout.Button ("Continue")) {
				scene = 9;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 9) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			
				GUILayout.Label ("Memoirs.txt" +
				                 "\n\n I'm not writing this as an excuse. I only think its fair for people to hear the other side of the story. " +
				                 "\n\n I grew up on an fishing island in the pacific. Beside spending most of my childhood in a dark and secluded house in the back of my tiny hometown, I would often hang around with my small group of friends." +
				                 " But as school came to an end, those friends soon disappeared. Some started working, others got into high-schools on the mainland, or across the sea. A few joined the war in China." +
				                 " \n\n But I didn't get a job. I didn't get into any of the schools I applied to. And I couldn't go to war." +
				                 "\n\n I was stuck.");
				GUILayout.Label ("\n The last one to leave the island was Aya. I still remember the sound of her voice when she told me she got into TT, " +
					"the prestigious school of both Nippon and international fame. The perfect place for my genius friend." +
				                 " And I felt jealous. Not that she got into the school, but that she had a dream to work for. A purpose to strive towards." +
				                 " While I had nothing. Not that I told her, obviously. Instead, I congratulated her, and waved her goodbye as she drifted away" +
				                 " across the horizon on a fancy boat from the capital." +
				                 "\n\n I didn't sleep that night.\n");

			if (GUILayout.Button ("Continue.")) {
				scene = 10;
			}
			GUILayout.EndVertical ();
			} else if (scene == 10) {
				GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));

				GUILayout.Label ("Memoirs.txt" +
					" \n\n A week later I sat on the hill outside town, looking out over the rusted stinking cargo ships rolling by on their way to Tokyo, when I got an idea." +
					" I knew it would be the dumbest thing I ever did in my life, but at that point I felt as though I had nothing of value to lose. " +
					"I had no one on the island. My parents were gone- not dead, just not there. And the people on the island... never really cared about me. Mostly because I never talked to any of them." +
					" \n\n Being half-Japanese, half-States made for a weird situation concerning my nationality." +
					" Due to the laws of former military islands between the west and the east, I couldn't get access to either country without a school or company behind me." +
					" \n\n My only option was to sneak in.");
				
				if (GUILayout.Button ("Continue.")) {
					scene = 11;
				}
				GUILayout.EndVertical ();
			} 
			else if (scene == 11) {
				GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));

				GUILayout.Label ("Memoirs.txt" +
					" \n\n A week later I sat on the hill outside town, looking out over the rusted stinking cargo ships rolling by on their way to Tokyo, when I got an idea." +
					" I knew it would be the dumbest thing I ever did in my life, but at that point I felt as though I had nothing of value to lose. " +
					"I had no one on the island. My parents were gone- not dead, just not there. And the people on the island... never really cared about me. Mostly because I never talked to any of them." +
					" \n\n Being half-Japanese, half-States made for a weird situation concerning my nationality." +
					" Due to the laws of former military islands between the west and the east, I couldn't get access to either country without a school or company behind me." +
					" \n\n My only option was to sneak in.");
				GUILayout.Label ("\n On the 21 September I arrived at the stinking metal harbor of Chiba City, having hitched a ride on one of the larger ships as a part of the crew. But as I pushed my way through the crowd, two heavily armed guardsmen caught my attention at the docking station's exit." +
					" I had hoped the security would be looser. Oh, how panicked I became, looking around the cramped space in desperate search for some kind of escape." +
				    " Without a passport, I would most likely be detained, and held in a filthy cell in the city. Considering I had no proof of my origin, " +
					"they would probably assume me to be some sort of foreigner looking to sneak past the border. Which I was, but that's beside the point." +
					"\n\n The stream of fishermen and shipcrew pushed me towards the guards more and more so. While I attempted fighting back, I noticed another person walking in the opposite direction, away from the exit." +
					" A man in a black robe, and glowing glasses. Going beyond the crowd and around a corner, he placed his palm against some sort of panel and opened a ventilation duct. The man climbed inside. Me, having followed him, did the same.\n");
				if (GUILayout.Button ("Continue.")) {
					scene = 12;
				}
				GUILayout.EndVertical ();
			}
			else if (scene == 12) {
			OverworldManager.sceneStarting = true;
			started = true;
			scene = 0;
		}
		
		GUILayout.EndArea ();
	}
	}
}
