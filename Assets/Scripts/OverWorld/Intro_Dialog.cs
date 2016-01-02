using UnityEngine;
using System.Collections;

public class Intro_Dialog : MonoBehaviour {
	public GUISkin skin;
	int scene = 0;
	
	//public static bool playerTalking = false;
	
	private GUISkin DOS2;
	
	void Start () {
		DOS2 = Resources.Load("DOS2") as GUISkin;
		scene = 1;
	}
	
	void Update(){
		if (scene != 0) {
			OverworldPlayer.instance.moveDestination [0] = transform.position.x;
			OverworldPlayer.instance.moveDestination [2] = transform.position.y;
		}
	}
	
	void OnGUI()
	{
		
		GUI.skin = DOS2;
		
		//START
		
		GUILayout.BeginArea (new Rect (Screen.width/4, Screen.height/6, Screen.width/2, Screen.height));
		
		if (scene == 1) {
			
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));

			GUILayout.Label ("I didn't know what to do.\n\n They were all leaving. One after another, some going off to search for a purpose, others to simply make a living. Tokyo Metropolis, F. Cali, Hong Kong, and... Chiba City. These places where dreams could come true, while I stayed behind on our little home-island in the pacific. I spend the last days of summer alone, wandering through the bay area, watching rust consume the last of the buildings. Whoever founded that small port town probably did not understand the fact that metal houses white like eggshells would end up looking spoiled and rotten within only a decade or so. But I liked it. There was something melancholy, yet uplifting about the brown corruption creeping its way up the cold plate walls.");
			GUILayout.Label (" I loved my home. But during those few days I spent alone I came to realize just why. It wasn't the smell of rotten fish lingering in the air for days on end. It wasn't my parents old house, or the decaying forest on the others side of the island. \n\n It was the people.\n");
			//GUILayout.Label ("\n The first thing that struck me about the docking station was the smell- the smell of dead rats, and exploded sewer pipes. Yet I had lucked out. The entire area appeared to have been abandoned. The lights placed onto poles around the place were unlit. None of them seemed functional, by either neglect, vandalism, or both. So it was dark, and the ground beneath my feet was sticky. Like someone had spilt out a reserve of artifical sweetener around the entirety of the docking station.");
			//GUILayout.Label ("");


			/*I landed here three years ago, by the oiled-up shores of Chiba City. 
			A week had gone by since I snuck onboard the massive cargo ship on my home island in the pacific, and the pain in my limbs could confirm it. 
				Having spent most of my time hiding inside of a cramped container, watching for crewmen, and silently laying still for hours upon hours, 
				the few mucles I had back then felt as if they had withered and died. 
			As I jumped from the railings towards one of the empty docking stations, the pain made me promise myself to never again set foot upon a boat.*/

			if (GUILayout.Button ("Continue")) {
				scene = 2;
			}
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			GUILayout.BeginVertical ("", GUI.skin.GetStyle("label"));
			
			GUILayout.Label (" So I followed them. Away from comfort, away from home. As night fell on the 21 September, I packed the few belongings I could carry, a bag filled with food, some clothes, and my VS-Jockie in case I got bored during the trip. I then went down to the docks, and snuck onboard a cargo ship that had stopped for some maintenance. As I desperately ran around the ship, hiding from crewmen, and cramming myself into smaller and smaller containers and crates, I kept thinking that when I arrived, it would all be worth it. What I knew of Chiba City was that it was large, and dirty. An industrial leader, and complete shitheap at the same time. The perfect place for a lost teenager to find his place in the world.");
			GUILayout.Label ("\n At least, that was the idea. \n\n I got off the cargo ship at around four in the morning. But as I followed the stream of disembarking crewmen, I noticed some sort of guard by the docking station's exit. They wouldn't let me into the city without a pass. Obviously.");
			GUILayout.Label ("\n There were a dozen ways for me to fix my dumb mistake. I could have  talked my  would have given me a nice comfy cell, . ");
			if (GUILayout.Button ("Search the area.")) {
				scene = 3;
			}
			GUILayout.EndVertical ();
		}else if (scene == 3) {
			OverworldManager.sceneStarting = true;
			scene = 0;
		}
		
		GUILayout.EndArea ();
	}
}
