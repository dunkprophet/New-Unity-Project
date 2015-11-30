using UnityEngine;
using System.Collections;

public class OverworldPlayer : MonoBehaviour {
	public static OverworldPlayer instance;
	
	public float moveSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public Vector3 moveDestination;

	void Awake () {
		instance = this;

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//Talking to things
		/*
		if (Input.GetMouseButtonDown(0)) {
			Ray hit RaycastHit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, hit)) {
				if (hit.transform.name == "MyObjectName" )Debug.Log( "My object is clicked by mouse");
			}
		}

		if (moveDestination.x == -3.99 && moveDestination.y == 0.45) {

		}

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3 (Input.mousePosition, 0, 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		Debug.Log(transform.position);
		*/
	}

	public void moveDest (float destX,float destZ) {
		moveDestination[0] = destX;
		moveDestination[2] = destZ;
	}

	public void moveZig () {
		if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination -transform.position).normalized * moveSpeed * Time.deltaTime;
			
			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {
				transform.position = moveDestination;
				
			}
		}
	}
}
