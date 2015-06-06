using UnityEngine;
using System.Collections;

public class OverworldPlayer : MonoBehaviour {
	
	//float speed = 3.0f;
	RaycastHit hit;
	Vector3 newPos;

	bool canWalk = false;
	
	public float speed = 3.0f;
	public float rotateSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {


		/*Input.GetButton ("w");
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis(""), 0);
		transform.position += move * speed * Time.deltaTime;
		*/
		if (canWalk)
		{
			makeCharacterWalk();
		}

		Debug.Log(transform.position);

	}
	void OnGUI()
	{
		Event e = Event.current;
		
		if (e.button == 0 && e.isMouse)
		{
			Debug.Log("mouse click");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f))
			{
				newPos = new Vector3(hit.point.x, 0.5f, hit.point.z);    
			}
			
			canWalk = true;
			
			//point = Input.mousePosition;
			//Debug.Log("point is " + point);
		}
	}
	void makeCharacterWalk()
	{
		CharacterController controller = GetComponent<CharacterController>();
		
		Vector3 diff = transform.TransformDirection(newPos - transform.position);
		controller.SimpleMove(diff * speed);    
	}
}
