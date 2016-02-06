using UnityEngine;
using System.Collections;

public class Nodes : MonoBehaviour {

	public int node;
	public string nodeName;
	public string nodeText;

	// Use this for initialization
	void Start () {
		nodeText = nodeText.Replace("NL","\n");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnMouseDown()
	{
		OverworldManager.instance.nodeClicked (node, nodeName, nodeText);
	}
	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.gray;
	}
	void OnMouseExit()
	{
		transform.GetComponent<Renderer> ().material.color = Color.white;
	}
}
