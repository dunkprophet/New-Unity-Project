using UnityEngine;
using System.Collections;

public class Nodes : MonoBehaviour {

	public int node;
	public string nodeName;
	public string nodeText;
	public bool showText = false;
	private bool tempBool = false;

	private bool clicked = false;

	public Renderer rend;
	public BoxCollider boxC;
	public SphereCollider sphereC;
	public CapsuleCollider capsC;
	public bool turnOn = false;

	private Rect nodeBox;
	private Vector3 nodePosition;



	// Use this for initialization
	void Start () {
		nodeText = nodeText.Replace("NL","\n");
		nodePosition = transform.position;
		rend = GetComponent<Renderer>();
		rend.enabled = turnOn;
		if (GetComponent<BoxCollider> () != null) {
			boxC = GetComponent<BoxCollider> ();
			boxC.enabled = turnOn;
		}
		if (GetComponent<SphereCollider> () != null) {
			sphereC = GetComponent<SphereCollider> ();
			sphereC.enabled = turnOn;
		}
		if (GetComponent<CapsuleCollider> () != null) {
			capsC = GetComponent<CapsuleCollider> ();
			capsC.enabled = turnOn;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (turnOn == false && OverworldManager.instance.netscapeNodes.Contains (node)) {
		
			turnOn = true;
			rend.enabled = turnOn;
			if (GetComponent<BoxCollider> () != null) {
				boxC.enabled = turnOn;
			}
			if (GetComponent<SphereCollider> () != null) {
				sphereC.enabled = turnOn;
			}
			if (GetComponent<CapsuleCollider> () != null) {
				capsC.enabled = turnOn;
			}
		}



		/*nodePosition = Camera.main.ScreenToWorldPoint (transform.position);*/
		if (OverworldManager.instance.grayNode == true) {
			clicked = false;
			transform.GetComponent<Renderer> ().material.color = Color.white;
		}
	}
	void OnMouseDown()
	{
		if (OverworldManager.instance.showNode == false) {
			OverworldManager.instance.nodeClicked (node, nodeName, nodeText, nodePosition);
			clicked = true;
		}
	}
	void OnMouseEnter()
	{
		if (OverworldManager.instance.showNode == false) {
			OverworldManager.instance.nodeHovered(nodeName);
			OverworldManager.instance.grayNode = false;
			transform.GetComponent<Renderer> ().material.color = Color.gray;
		}
	}
	void OnMouseExit()
	{
		if (OverworldManager.instance.showNode == false) {
			OverworldManager.instance.nodeUnHovered ();
			//OverworldManager.instance.nodeHovered ("");
			if (OverworldManager.instance.showNode == false) {
				transform.GetComponent<Renderer> ().material.color = Color.white;
			}
		}

	}
	public void OnGUI() {
		/*GUI.skin = OverworldManager.instance.MetalGUISkin;
		if (clicked == true) {
			print(nodePosition); 

			GUILayout.BeginArea (new Rect (nodePosition.x, Screen.height - nodePosition.y - 10, Screen.width/10, Screen.height/5));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));

			GUILayout.Label(nodeName);
			if (GUILayout.Button ("Close")) {
				clicked = false;
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();
		}*/
	} 
}
