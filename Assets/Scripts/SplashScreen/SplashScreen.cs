using UnityEngine; 
using System.Collections; 
using System.Collections.Generic;

public class SplashScreen : MonoBehaviour {
	public Texture SplashScreenImage;
	public float timeToDisplayImage;
	public int nextLevelToLoad;

	public float fadeSpeed = 1.5f;        

	//private bool sceneStarting = true;
	
	private float timeForNextLevel;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	public void Start() {
		timeForNextLevel = Time.time + timeToDisplayImage;
	}
	
	public void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), SplashScreenImage);
		if (Time.time >= timeForNextLevel) {
			EndScene();
		}
	}
	void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if (GetComponent<GUITexture> ().color.a >= 0.95f)
			// ... reload the level.
			Intro_Dialog.SplashDone = true;
			Application.LoadLevel(1);
			//Application.LoadLevel(0);
	}
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
}
