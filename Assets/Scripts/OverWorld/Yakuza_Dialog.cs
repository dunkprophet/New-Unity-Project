using UnityEngine;
using System.Collections;

public class Yakuza_Dialog : MonoBehaviour {
	public GUISkin skin;
	int scene = 0;

	public static bool talkedToYakuza;

	//public static bool playerTalking = false;

	private GUISkin MetalGUISkin;

	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		talkedToYakuza = false;
	}

	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = 1.22f;
			OverworldPlayer.instance.moveDestination [2] = 9.74f;
		}
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0) && talkedToYakuza == false) {
			scene = 1;
		}
		if (Input.GetMouseButtonDown (0) && talkedToYakuza == true) {
			scene = -1;
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

		GUILayout.BeginArea (new Rect (Screen.width/4, 10, 400, 600));

		if (scene == 1) {
		
			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle("window"));
			//FIRST WORD
			GUILayout.Label ("\"(CHANGE HIM INTO TECH GOD WHO NEEDS HELP; 'you have a VSR? They're old school, yo. It should work on this thing doe,')Look, man, I'm not wasting a single more second of my life out here.\"");
			GUILayout.Label ("The man I found standing by the warehouse entrance was talking into some kind of earpiece. An old model, the kind you would only read about on niche tech-forums at four in the morning. His hair was jet black, like oil dripping from his scalp, and just as greasy too. I glimpsed a handgun beneath his long jacket, and his eyes were hidden behind by a pair of dual-light glasses. Expensive taste.");
			GUILayout.Label ("\"Goro, you dick, get me out of here! I'm not going to-\"");
			GUILayout.Label ("The man froze when he noticed me.");
			GUILayout.Label ("After a short silence, he continued his call: \"I don't care, Goro. I'm getting out. I'll see you tonight.\"");
			GUILayout.Label ("He reached up, pressing a button on his temple. \"You don't look like a cop.\"");
			//FIRST CHOICE
			if (GUILayout.Button ("\"I'm new here. Just passing through.\"")) {
				scene = 2;
			}

			if (GUILayout.Button ("\"I don't want any trouble.\"")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {

			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle("window"));
			//From CHOICE 2
			GUILayout.Label ("\"Don't tell me you got here from one of those ships?\" I nodded in response, to which he laughed. \"A fucking freeloader, eh? Don't worry, kid, we've all been there.\" He reached out a hand. \"Name's Saburo. I'm an 'entrepreneur' here in Mihama-ku.\"");

			//FIRST CHOICE
			if (GUILayout.Button ("Shake his hand.")) {
				scene = 4;
			}
				
			//SECOND CHOICE
			if (GUILayout.Button ("\"Why are you out here?\"")) {
				scene = 5;
			}

			GUILayout.EndVertical ();
		
		} else if (scene == 3) {
			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle("window"));
			GUILayout.Label ("\"Then you're in the wrong place, kid. How did you even get in here?\" I looked to my side, towards the massive cargo ship still idly floating by the bay. \"Hah!\" he laughed, placing a palm on his forehead. \"A fucking freeloader, eh? Don't worry, kid, we've all been there.\" He reached out a hand. \"Name's Saburo. I'm an 'entrepreneur' here in Mihama-ku.\"");
				
			//ONLY CHOICE
			if (GUILayout.Button ("Shake his hand.")) {
				scene = 4;
			}
			if (GUILayout.Button ("\"Why are you out here?\"")) {
				scene = 5;
			}
			//Word of advice, kid: Asking questions like that could get you killed here. I'd recommend against it. But if you have to know, it was a birthday present.
			GUILayout.EndVertical ();

		} else if (scene == 4) {
			GUILayout.BeginVertical ("Saboru", GUI.skin.GetStyle("window"));
			GUILayout.Label ("He took my hand in a strong grip and leaned in close to my ear, whispering, \"I got something to confess to you, kid. I'm not good with tech. I got myself this clunky-ass headgear a month ago, and all I can do with it is make calls and play shitty video games.\" He let go of my hand and took off his headmounted Jock-VS2. \"I've got a deal for you. You get me out of here, and I'll let you have this... thing. Hai?\"");
			
			//ONLY CHOICE
			if (GUILayout.Button ("Take the deal.")) {
				scene = 7;
			}
			if (GUILayout.Button ("\"What's the catch?\"")) {
				scene = 6;
			}
			GUILayout.EndVertical ();

		} else if (scene == 5) {
			GUILayout.BeginVertical ("Saboru", GUI.skin.GetStyle("window"));
				GUILayout.Label ("When I refused to shake his hand, he gave me a dirty look and said something in Japanese. When he noticed I didn't understand him, he said, \"You don't even speak the national fucking language? What's wrong with kids today, my god.\" He sighed and crossed his arms. \"Look, I have to be honest with you, kid. I'm not good with tech. I got myself this clunky-ass headgear a month ago, and all I can do with it is make calls and play shitty video games.\" He let go of my hand and took off his headmounted Jock-VS2. \"I've got a deal for you. You get me out of here, and I'll let you have this... thing. Okay?\"");
				
			
			if (GUILayout.Button ("Take the deal.")) {
				scene = 7;
			}
			if (GUILayout.Button ("\"What's the catch?\"")) {
				scene = 6;
			}

			GUILayout.EndVertical ();

		} else if (scene == 6) {
			GUILayout.BeginVertical ("Saboru", GUI.skin.GetStyle("window"));
			GUILayout.Label ("\"No catch. No drawbacks. Just get me the fuck out of here, and I'll be a happy man. And, I mean, you probably don't want to spend your evening in this stinking hell hole either, right? So just get us out of here, and fast.\"");
			
			if (GUILayout.Button ("Take the deal.")) {
				scene = 7;
			}

			GUILayout.EndVertical ();

		} else if (scene == 7) {
			GUILayout.BeginVertical ("Saboru", GUI.skin.GetStyle("window"));
			GUILayout.Label ("Saboru smiled as he handed me the headgear. He then pointed towards the wall, saying, \"Just jack into that panel, and get the gate open. Good luck, kid.\"");
				
			if (GUILayout.Button ("Put on the headgear.")) {
				scene = 8;
			}

			GUILayout.EndVertical ();

		} else if (scene == 8) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			talkedToYakuza = true;
			scene = 0;
		}
		else if (scene == -1) {
			GUILayout.BeginVertical ("Saboru", GUI.skin.GetStyle("window"));
			GUILayout.Label ("\"Just jack into that panel over there, and get going. I'm sure it'll be as easy as smashing your head against the wall.\"");
				if (GUILayout.Button ("\"If you say so...\"")) {
					scene = 8;
				}
			GUILayout.EndVertical ();
		}

		GUILayout.EndArea ();

	}

}
