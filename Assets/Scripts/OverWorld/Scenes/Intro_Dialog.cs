using UnityEngine;
using System.Collections;

public class Intro_Dialog : MonoBehaviour {
	public GUISkin skin;
	public bool written = true;
	public bool started = false;
	public bool ready = true;
	public static bool SplashDone = false;
	public int p = 0;
	public int k = 0;
	public int c = 0;
	public static string note;
	public bool tempBool = false;
	
	//public static bool playerTalking = false;
	
	private GUISkin DOS2;
	
	void Start () {
		//ALL MAIL HERE!
		GameObject mailObject; 

		mailObject = ((GameObject)Instantiate (
			Resources.Load("Prefabs/mail") as GameObject,
			new Vector3(0,0,0),
			Quaternion.Euler (new Vector3 ()))
		              ) as GameObject;
		
		mailObject.GetComponent<Mail>().sender = "Zzaam_x";
		mailObject.GetComponent<Mail>().time = "[06:28:15]";
		mailObject.GetComponent<Mail>().date = "09/22";
		mailObject.GetComponent<Mail>().subject = "Hey Kid";
		mailObject.GetComponent<Mail>().content = "I'm sure you feel as shitty as I do about what happened yesterday. " +
			"Actually, no, I should feel worse considering they were my friends. " +
				"Then again, you probably have a lot less experience with death, and killings, whilst I have tons." +
				"\n\n...whatever, look. I want this to be over with too, so just do as I say: LAY LOW. Don't make anyone look at you funny, " +
				"and don't fucking talk about what you saw yesterday. Okay? Good. I'll write to you in three days, THREE DAYS, " +
				"to tell you where I want you, and when.\n\nDon't fuck up till then.\n\n/Saburo";
		
		OverworldManager.instance.mailList.Add(mailObject);
		
		
		mailObject = ((GameObject)Instantiate (
			Resources.Load("Prefabs/mail") as GameObject,
			new Vector3(0,0,0),
			Quaternion.Euler (new Vector3 ()))
		              ) as GameObject;
		
		mailObject.GetComponent<Mail>().sender = "Hachi";
		mailObject.GetComponent<Mail>().time = "[18:02:59]";
		mailObject.GetComponent<Mail>().date = "09/22";
		mailObject.GetComponent<Mail>().subject = "Concerning payment";
		mailObject.GetComponent<Mail>().content = "You didn't listen to me yesterday, so here's the info again:" +
			"\n\n-¥35000 a month, delvered to me PERSONALLY" +
				"\n-No drugs" +
				"\n-No pets" +
				"\n-No loud noises after midnight" +
				"\n-NO HACKING!" +
				"\n\nI know your type. Hoping to relive the lives of the \"netscapers\" by living on their old street. Bah." +
				" If you want to be a dank hacker, then you can find some other place to live. Or use a proxy.";
		
		OverworldManager.instance.mailList.Add(mailObject);
		
		mailObject = ((GameObject)Instantiate (
			Resources.Load("Prefabs/mail") as GameObject,
			new Vector3(0,0,0),
			Quaternion.Euler (new Vector3 ()))
		              ) as GameObject;
		
		mailObject.GetComponent<Mail>().sender = "Aralia_3";
		mailObject.GetComponent<Mail>().time = "[20:09:30]";
		mailObject.GetComponent<Mail>().date = "09/22";
		mailObject.GetComponent<Mail>().subject = "Calvin!";
		mailObject.GetComponent<Mail>().content = "I just got your message, sorry for not looking in my inbox sooner (;﹏;) I've been REALLY busy at school\n\n" +
			"But cool! Moving to Chiba all by yourself, that's awesome! " +
				"Would have been even better if you would have gotten in somewhere..." +
				" but you can always try for a spot next year! Don't forget to get a job too, " +
				"just a temporary one, of course, but still! You need to be able to pay for your apartment.\n\n" +
				"You DO have an apartment, don't you? Chiba is the easiest place in the world for that right now," +
				" so it shouln't be a problem :) Just write if you need any help with anything, I promise to answer you this time!!!" +
				"\n\n /Aya\n\nPS. If you wanna talk, just head on over to the Training Ground on the net. I can show you some of the" +
				" stuff we do at TT in our spare time! (Hint: It involves programming v(￣∇￣))"; 
		mailObject.GetComponent<Mail>().unlockingNode = 9;
		
		
		OverworldManager.instance.mailList.Add(mailObject);

	}
	
	void Update(){
		if (OverworldManager.instance.scene  == 0) {
			//OverworldManager.instance.scrollPosition.y = Mathf.Infinity;
			if (OverworldManager.instance.dialogControllerMenu == 0) {
				OverworldManager.instance.textForLog = "Welcome to DOS65";
				if (tempBool == false){
					OverworldManager.instance.GetComponent<AudioSource>().clip = OverworldManager.instance.song1;
					OverworldManager.instance.GetComponent<AudioSource>().Play();
					tempBool = true;
				}
				if (ready == true){
					StartCoroutine(pause(0.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 1) {
				tempBool = false;
				OverworldManager.instance.textForLog = "\n\n\nVRS-Jock v1.9.2 alpha 1 [DOS65] \nInstalled at EYE/2 port \n\n" +
					"AKIHABARA コンピュータストア CB80 1.0\n" +
					"Copyright 2063, 64, 65 愚かなアメリカ版\n\n" +
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
					"\n\n\nD:\\>";
				if (ready == true){
					StartCoroutine(pause(2.0f));
				}

			}
			if (OverworldManager.instance.dialogControllerMenu == 2){
				OverworldManager.instance.textForLog = "User\\CAL\\Files\\>";
				if (ready == true){
					StartCoroutine(pause(2.5f));
				}

			}
			if (OverworldManager.instance.dialogControllerMenu == 3){
				OverworldManager.instance.textForLog = "run Icebreaker.exe";
				if (ready == true){
					StartCoroutine(pause(1.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 4){
				OverworldManager.instance.textForLog = "\n\n...";
				if (ready == true){
					StartCoroutine(pause(3.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 5){

				OverworldManager.instance.textForLog = "\n\n...\n\nStartup Successful";
				if (ready == true){
					StartCoroutine(pause(2.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 6){

				OverworldManager.instance.textForLog = "\n\nStartup Successful" + note;

				if (ready == true){
					StartCoroutine(pause(1.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 7){
				OverworldManager.instance.textForLog = "\n\nReading Data...\n" + note;
				OverworldManager.instance.updateLog();
				k++;
				if (k == 27 && tempBool == false) {
					k = 0;
					tempBool = true;
				}
				if (k == 27 && tempBool == true){
					if (ready == true){
						StartCoroutine(pause(2.5f));
					}
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 8){

				OverworldManager.instance.textForLog = "\n\nStartup Successful";

				OverworldManager.instance.textForLog = "\n\n人はどのような女の子を殺すためにマシンを構築しますか？" +
					"\nいいえ、彼は彼の手を使用していませんでした。" +
					"\nスマート男のように、彼はツールを使用しました。" +
					"\nしかし、単に同じ、" +
					"\nどのようにあなたは誰のせいだ質問ができますか？" +
					"\n彼女の名前は何でしたか？" +
					"\nそれは問題ではありません。" +
					"\n今聞きます..." +
					"\n「名医」は支払わなければなりません！";
				if (ready == true){
					StartCoroutine(pause(3.5f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 9){
				OverworldManager.instance.textForLog = "";
				OverworldManager.instance.eraseLog();
				if (ready == true){
					StartCoroutine(pause(1.0f));
				}
			}

			if (OverworldManager.instance.dialogControllerMenu == 10){
				OverworldManager.instance.textForLog = "";
				if (ready == true){
					StartCoroutine(pause(0.0f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu == 11 && p == 5){
				OverworldManager.instance.textForLog = "\n\n  1 2 3 4 5 6 7";
				if (ready == true){
					StartCoroutine(pause(3.0f));
				}
			}
			else if (OverworldManager.instance.dialogControllerMenu == 11){
				OverworldManager.instance.textForLog = "\n\nEntering Menu";
				if (ready == true){
					c++;
					StartCoroutine(pause(3.0f));
				}
			}
			if (OverworldManager.instance.dialogControllerMenu >= 12){
				OverworldManager.instance.dialogControllerMenu = 10;
				p++;
			}
			if (p == 6){
				p = 0;
			}
			if (c == 30) {
				OverworldManager.instance.eraseLog();
				c = 0;
			}

		}
		if (k == 0) {
			note = SystemInfo.deviceModel;
		}
		if (k == 10) {
			note = SystemInfo.deviceName;
		}
		if (k == 20) {
			note = SystemInfo.deviceType.ToString();
		}
		/*if (k == 3) {
			note = SystemInfo.deviceUniqueIdentifier.ToString();
		}
		if (k == 4) {
			note = SystemInfo.graphicsDeviceID.ToString();
		}
		if (k == 5) {
			note = SystemInfo.graphicsDeviceName.ToString();
		}
		if (k == 6) {
			note = SystemInfo.graphicsDeviceType.ToString();
		}
		if (k == 7) {
			note = SystemInfo.graphicsDeviceVendor.ToString();
		}
		if (k == 8) {
			note = SystemInfo.graphicsDeviceVendorID.ToString();
		}
		if (k == 9) {
			note = SystemInfo.graphicsDeviceVersion.ToString();
		}
		if (k == 10) {
			note = SystemInfo.graphicsMemorySize.ToString();
		}
		if (k == 11) {
			note = SystemInfo.graphicsMultiThreaded.ToString();
		}
		if (k == 12) {
			note = SystemInfo.graphicsShaderLevel.ToString();
		}
		if (k == 13) {
			note = SystemInfo.maxTextureSize.ToString();
		}
		if (k == 14) {
			note = SystemInfo.npotSupport.ToString();
		}
		if (k == 15) {
			note = SystemInfo.operatingSystem.ToString();
		}
		if (k == 16) {
			note = SystemInfo.processorCount.ToString();
		}
		if (k == 17) {
			note = SystemInfo.supportedRenderTargetCount.ToString();
		}
		if (k == 18) {
			note = SystemInfo.processorType.ToString();
		}
		if (k == 19) {
			note = SystemInfo.supports3DTextures.ToString();
		}
		if (k == 20) {
			note = SystemInfo.supportsAccelerometer.ToString();
		}
		if (k == 21) {
			note = SystemInfo.supportsComputeShaders.ToString();
		}
		if (k == 22) {
			note = SystemInfo.supportsGyroscope.ToString();
		}
		if (k == 23) {
			note = SystemInfo.supportsImageEffects.ToString();
		}
		if (k == 24) {
			note = SystemInfo.supportsLocationService.ToString();
		}
		if (k == 25) {
			note = SystemInfo.systemMemorySize.ToString();
		}*/
		if (k == 26) {
			note = "";
		}
	}
	public IEnumerator pause(float delay) {
		ready = false;
		yield return new WaitForSeconds(delay);
		print ("DialogController++");
		if (OverworldManager.instance.dialogReady == true) {
			OverworldManager.instance.updateLog();
			OverworldManager.instance.dialogControllerMenu++;
			//OverworldManager.instance.i = 0;
		}

		ready = true;
	}

		/*
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
		
		GUILayout.EndArea ();*/


}
