using UnityEngine;
using System.Collections;

public class Yakuza_Dialog : MonoBehaviour {
	public GUISkin skin;
	int scene = 0;

	public static bool talkedToYakuza;
	public static bool putOnIcebreaker;
	public int SavingSaburo = 0;
	
	public float fadeSpeed = 0.5f;

	public Sprite dead;

	public int d1 = 0;
	public int d2 = 0;
	public int d3 = 0;

	//public static bool playerTalking = false;

	private GUISkin MetalGUISkin;

	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		dead = Resources.Load<Sprite> ("Yakuza2Dead");
		talkedToYakuza = false;
		putOnIcebreaker = false;
	}

	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = 1.22f;
			OverworldPlayer.instance.moveDestination [2] = 9.74f;
		}
	}

	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
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

		GUILayout.BeginArea (new Rect (10, 10, 600, Screen.height-100));

		if (scene == 1) {
		
			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle ("window"));
			//FIRST WORD
			GUILayout.Label ("\n As I approached the man in black, he turned and glared to me with a frown." +
				"\n\n \"You don't look like the military type. Or police.\"" +
				" He reached up, and turned off his glass-gear, revealing a pair of dark eyes.\n\n \"Why did you follow me?\"\n");
			//'you have a VSR? They're old school, yo. It should work on this thing doe
			//FIRST CHOICE
			if (GUILayout.Button ("\"I saw you sneaking away from those guards.\"")) {
				scene = 2;
			}

			if (GUILayout.Button ("\"The border control. I had to get away.\"")) {
				scene = 3;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {

			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle ("window"));
			//From CHOICE 2
			GUILayout.Label ("\n\"And I saw *you* doing the exact same thing.\" The man reached into his jacket. He laughed as I took a step back.\n\n \"You really think I'm going to do something to you? You're just a kid. Come on.\" He took out some sort of cable and plugged it into his headmounted glass-gear. I didn't recognize the kind.\n");

			//FIRST CHOICE
			if (GUILayout.Button ("\"What's that?\"")) {
				scene = 4;
			}
				
			//SECOND CHOICE


			GUILayout.EndVertical ();
		
		} else if (scene == 3) {
			GUILayout.BeginVertical ("Strange man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"Yeah? Then we're here for the same reason, kid.\" The man reached into his jacket, and took out some sort of cable and plugged it into his headmounted glass-gear. I didn't recognize the kind.\n ");
				
			//ONLY CHOICE
			if (GUILayout.Button ("\"What's that?\"")) {
				scene = 4;
			}

			//Word of advice, kid: Asking questions like that could get you killed here. I'd recommend against it. But if you have to know, it was a birthday present.
			GUILayout.EndVertical ();

		} else if (scene == 4) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"This?\" he asked, pointing towards his headgear. \"I call it The ICEBREAKER. But the official name is CB30.\" He turned on his glasses once again, and plugged the other side of the cable into the panel to his left.\n\n \"I think mine's better.\"\n");
			
			//ONLY CHOICE
			if (GUILayout.Button ("Continue")) {
				scene = 5;
			}
			GUILayout.EndVertical ();

		} else if (scene == 5) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n Seconds after turning on his glasses, a bright sparkling light began flickering on the panel before the man in black. As it did, his glasses started doing the same. Soon sparks flew out of the machine, and the man's glass-gear. Yet the man remained still. I sensed something would happen to him soon, just from the noise of sparking cables and heated circuit boards that emanated from the panel." +
				"\n\n It was going to end badly, for sure.\n");
				
			
			if (GUILayout.Button ("Rush to his aid.")) {
				scene = 6;
				SavingSaburo = 1;
			}
			if (GUILayout.Button ("Stand back.")) {
				scene = 7;
				SavingSaburo = 0;
			}

			GUILayout.EndVertical ();

		} else if (scene == 6) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n I didn't know what the man was doing, but I knew it wasn't good. The cable that connected him to the rusty old panel came out easily." +
				"\"Old man,\" I said to him, as he fell to the ground. \"What was-\"\n\n I saw his eyes flicker and his body shake as he began coughing blood.\n\n\"So close,\" he whispered. \"Fuck.\" He placed two fingers on his temple and mustered the last of his strength to say, \"Kid. I've got one shot here, and I'm placing it on you.\" He leaned in. \"Take me online.\" \n\n The man then pressed the side of his head, before collapsing into a cold husk.\n");
			
			if (GUILayout.Button ("Take a step back.")) {
				scene = 8;
			}

			GUILayout.EndVertical ();

		} else if (scene == 7) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n As I stood back to watch the event unfold, his body started convuling ever more violently. Soon blood came flowing out his mouth and eyes. \n\n He got fried by the machine. The stench around his dead body was unbearable. I couldn't believe what I had witnessed, or even understand what had happened before me.\n");
				
			if (GUILayout.Button ("Take a step back.")) {
				scene = 8;
			}

			GUILayout.EndVertical ();

		} else if (scene == 8) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
			transform.GetComponent<SpriteRenderer> ().sprite = dead;
			talkedToYakuza = true;
			scene = 0;
		} else if (scene == -1 && putOnIcebreaker == false) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\nThe man's fried body was a mess of red and black, spread out over the concrete ground. To my suprise, it seemed as though his glass-gear had survived the encounter without major failures. The thought of taking it to escape the docking station popped into my mind. \n\n ...\n");
			if (GUILayout.Button ("Take the ICEBREAKER")) {
				scene = -2;
			}
			GUILayout.EndVertical ();
		} else if (scene == -2) {
			GUILayout.BeginVertical ("Strange Man", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\nThe dead man's body smelled of a terrible death. As I took the headgear off him, his cold dark eyes stared back at me with an uncaring look. It chilled me to the bones.\n\n Still, I wiped a few drops of blood from the glasses, and put them on.\n\n \"What could possibly go wrong?\" I thought to myself, as I mounted the glass-gear on my head. \n\n");
			if (GUILayout.Button ("Turn it on.")) {
				scene = 9;
			}
			GUILayout.EndVertical ();
		} else if (scene == -1 && putOnIcebreaker == true) {
			GUILayout.BeginVertical ("Saburo's Corpse", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("I had never been so close to death before. And the ghost in my head made it all the stranger.\n\n...\n\n...\n");
			if (GUILayout.Button ("Leave.")) {
				scene = 8;
			}
			GUILayout.EndVertical ();
		
		} else if (scene == 9) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\nA great flashing light blinded me. \"STEALING FROM THE DEAD IS BAD BUSINESS.\"" +
				"\n\n The voice echoed through my mind. \"BUT DESPERATE TIMES- AND SO ON.\"" +
				"\n\n\"Who are you?\" I wanted to say, but the voice answered me before I could." +
				"\n\n\"A DEAD MAN ON THE GROUND. ALSO A GHOST IN YOUR HEAD.\"");
			if (SavingSaburo == 1) {
				GUILayout.Label ("\n\"I HAVE TO SAY, THANK YOU FOR PULLING ME OUT OF THERE. EVEN IF IT WAS NOT ENOUGH TO SAVE ME, AT LEAST I DID NOT SUFFER THE FATE OF A FRIED BRAIN.\"\n");
			}
			if (SavingSaburo == 0) {
				GUILayout.Label ("\n\"GET A GOOD SHOW BY THE WAY? DO YOU GET OFF BY WATCHING PEOPLE GET THEIR BRAINS FRIED?\"" +
					"\n\n I tried telling him no, but he went on, \"BAH. IT IS FINE. THE PAIN OF GETTING MY INSIDE TURNED INTO CRISPY MEAT HAS MADE ME A BIT HARSH.\"\n\n \"I AM SURE THERE WAS NOTHING YOU COULD DO.\"\n");
			}
			if (GUILayout.Button ("Try to speak.")) {
				scene = 10;
			}
			GUILayout.EndVertical ();
			
		} else if (scene == 10) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n\"HONESTLY SPEAKING,\" the voice went on, \"I HAVE TO TRUST YOU TO SAVE ME.\"" +
				" \"But,\" I thought, \"How are you still-\"" +
				"\n\n \"I AM NOT. THIS IS MY GHOST. A DIGITAL PROJECTION OF MY BEING.\"\n");

			if (GUILayout.Button ("Ask about his ghost.")) {
				scene = 11;
			}
			if (GUILayout.Button ("Ask about his gear.")) {
				scene = 12;
			}
			if (GUILayout.Button ("Ask about him.")) {
				scene = 13;
			}

			GUILayout.EndVertical ();
		} else if (scene == 11) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"I AM A BEING OF NOTHINGNESS NOW. " +
				"ONCE I GET ONLINE I MIGHT MAKE A SOMWHAT NORMAL LIFE FOR MYSELF AGAIN, BUT NEVER AGAIN WILL I WALK IN MORTAL FORM.\"" +
				"\n\n I imagined a life with no body. Just a floating soul in space." +
				"\n\n \"MY GUESS IS I WILL GO MAD WITHIN A DECADE OR TWO. " +
				"IF I AM LUCKY I MIGHT REACH A CENTURY, BUT BY THAT TIME MY MIND WILL MOST LIKELY BE LONG GONE.\"\n\n \"THAT IS THE PRICE OF IMMORALITY.\"\n");
			d1 = 1;
			if (GUILayout.Button ("Ask about his gear.")) {
				scene = 12;
			}
			if (GUILayout.Button ("Ask about him.")) {
				scene = 13;
			}
			if (d1 == 1 && d2 == 1 && d3 == 1) {
				if (GUILayout.Button ("\"What do you want me to do?\"")) {
					scene = 14;
				}
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 12) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"IT TOOK ME YEARS TO GET AHOLD OF THE ICEBREAKER. IT IS THE GREATEST PIECE OF TECH I HAVE EVER OWNED. " +
				"I HAVE NO USE FOR IT IN DEATH, APART FROM HOLDING MY SOUL, OF COURSE. " +
				"BUT WERE YOU TO SET ME FREE, I CAN ONLY PROMISE YOU ONE THING. THE ICEBREAKER WILL BE YOURS.\"" +
				"\n\n \"IN TIME, YOU WILL REALIZE HOW GREAT A GIFT IT TRULY IS.\"\n");
			d2 = 1;
			if (GUILayout.Button ("Ask about his ghost.")) {
				scene = 11;
			}
			if (GUILayout.Button ("Ask about him.")) {
				scene = 13;
			}
			if (d1 == 1 && d2 == 1 && d3 == 1) {
				if (GUILayout.Button ("\"What do you want me to do?\"")) {
					scene = 14;
				}
			}

			GUILayout.EndVertical ();
		} else if (scene == 13) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"I AM AKIBA. I WAS ONCE KNOWN AS THE ICEBREAKER OF TOKYO. " +
				"THAT EMPTY SHELL OF A BODY OVER THERE IS CLOSE TO FIFTY YEARS OLD, BELIEVE IT OR NOT." +
				" I LEFT JAPAN AT THE END OF THE SIXTIES, DURING THE FIRST YEAR OF GREAT MIGRATION." +
			    "\" \n\n \"WHY? WELL, LET US SAY IT HAS TO DO WITH THE TRANSPACIFIC UNIFICATION, AND A FEW TOO MANY RUNS AGAINST GOVERMENT FACILITIES." +
				" THEY WANTED ME DEAD.\"\n\n \"INSTEAD I GET FRIED TRYING TO SNEAK INTO THE CAPITAL TWENTY YEARS DOWN THE LINE. HOW PATHETIC.\"\n");
			d3 = 1;
			if (GUILayout.Button ("Ask about his ghost.")) {
				scene = 11;
			}
			if (GUILayout.Button ("Ask about his gear.")) {
				scene = 12;
			}
			if (d1 == 1 && d2 == 1 && d3 == 1) {
				if (GUILayout.Button ("\"What do you want me to do?\"")) {
					scene = 14;
				}
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 14) {
			GUILayout.BeginVertical ("ICEBREAKER", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n \"I WOULD ASK OF YOU ONE THING: TO SAVE ME. YOU DO THIS BY GETTING THE ICEBREAKER ONLINE. OH, IF ONLY THE DAYS OF *Wi-Fi* WERE STILL AROUND, SUCH A TASK WOULD BE ALL TOO EASY. BUT KIDS LIKE YOU WOULD KNOW NOTHING OF THAT, WOULD YOU?\"" +
				"\n\n I shook my head, assuming that the ghost in it comprehended my action." +
				"\n\n \"NO. INSTEAD, YOU MUST SUCCEED WHERE I FAILED.\"" +
			    "\n\n A deafening crassling noise filled my head, like that of a drive getting scratched by a broken reader. \"HACK THE PANEL. BREAK THE ICE. OPEN THE GATE,\" Saburo told me." +
				"\n\n \"SIMPLE AS THAT. I WILL GUIDE YOU THROUGH IT. YOU HAVE THE GREATEST HACKER OF THE CENTURY IN YOUR MIND, SO DON'T WORRY.\"" +
				"\n\n \"LOOK AT IT THIS WAY: IT'S ALL IN YOUR HEAD...\"\n");

			if (GUILayout.Button ("Look around for a panel.")) {
				putOnIcebreaker = true;
				scene = 8;
			}
			
			GUILayout.EndVertical ();

		}
		GUILayout.EndArea ();

	}

}
