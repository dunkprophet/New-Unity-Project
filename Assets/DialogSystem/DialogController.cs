
using UnityEngine;
using System.Collections;

public class DialogController : MonoBehaviour {
	
	
	[System.NonSerialized]
	public Dialog activeDialog;
	private GameObject target;
	public GUISkin dialogSkin;
	public bool  allowMouseSelection = true;
	//Texture2D backgroundTexture;
	
	private CharacterMotor characterMotor;
	private MouseLook mouseLookX;
	private MouseLook mouseLookY;
	private MouseLook mouseLook;
	
	void  Start (){
		characterMotor = GetComponent<CharacterMotor>();
		mouseLookX = GetComponent<MouseLook>();
		mouseLookY = GameObject.Find("Main Camera").GetComponent<MouseLook>();
	}
	
	private bool blockV= false;
	private bool blockH= false;
	
	RaycastHit hit;
	
	void  Update (){
		if(activeDialog) {
			//Disable ability to move while in dialog
			characterMotor.canControl = false;
			mouseLookX.enabled = false;
			mouseLookY.enabled = false;
			
			//Turn into the direction of the dialog's source
			
			//		FIXME_VAR_TYPE turnTo= activeDialog.gameObject.transform.position - transform.position;
			//		turnTo.y = 0;
			//		transform.rotation = Quaternion.LookRotation(Vector3.Slerp(transform.forward, turnTo, 2 * Time.deltaTime));
			//		Camera.main.transform.rotation = Quaternion.LookRotation(Vector3.Slerp(Camera.main.transform.forward, turnTo, 2 * Time.deltaTime));
			
			Vector3 relative1 = transform.InverseTransformPoint(activeDialog.gameObject.transform.position);	
			Vector3 relative2 = Camera.main.transform.InverseTransformPoint(activeDialog.gameObject.transform.position);	
			
			float angleY= Mathf.Atan2(relative1.x, relative1.z) * Mathf.Rad2Deg;
			float angleX= Mathf.Atan2(-relative2.y, relative2.z) * Mathf.Rad2Deg;
			
			transform.Rotate(0 , angleY * 2 * Time.deltaTime, 0);
			Camera.main.transform.Rotate(angleX * 2 * Time.deltaTime, 0, 0);
			
			//Input to switch to the next dialog node
			if(Input.GetButtonUp("Fire1")) {
				activeDialog.NextNode();
			}
			
			float v= Input.GetAxisRaw("Vertical");
			float h= Input.GetAxisRaw("Horizontal");	
			
			//Allow the vertical axis to be used to scroll through answer options
			if(!blockV && Mathf.Abs(v) > 0) {
				blockV = true;
				if(v > 0) {
					activeDialog.BrowseUp();
				} else {
					activeDialog.BrowseDown();
				}
			} else if (Mathf.Abs(v) <= 0)
				blockV = false;
			//Horizontal axis also activates the next node
			if(!blockH && Mathf.Abs(h) > 0) {
				blockH = true;
				activeDialog.NextNode();
			} else if (Mathf.Abs(h) == 0)
				blockH = false;
		} else if(Input.GetButtonUp("Fire1") && target && target.GetComponent<Dialog>()) {
			//If there is no active dialog, activate the current target's dialog, when
			//the left mouse button is clicked
			activeDialog = target.GetComponent<Dialog>();
			activeDialog.allowMouseSelection = allowMouseSelection;
			activeDialog.Load(activeDialog.startNode);
		} else if (!characterMotor.canControl){
			//If there is no active dialog, but the player's crontrols are still
			//disabled, reenable them
			characterMotor.canControl = true;
			mouseLookX.enabled = true;
			mouseLookY.enabled = true;
		} else {
			//Use a raycast to determin the player's current target object
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, 3)) {
				if(hit.collider.gameObject.tag != "Player")
					target = hit.collider.gameObject;
			} else {
				target = null;
			}
		}
	}
	
	void  OnGUI (){
		GUI.skin = dialogSkin;
		
		if(activeDialog) {
			activeDialog.Show();
		} else if(target && target.GetComponent<Dialog>()) {
			//Draw the target's nametag
			Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position+target.GetComponent<Dialog>().nametagOffset);
			GUIContent name= new GUIContent(target.GetComponent<Dialog>().characterName);
			Rect rect= GUILayoutUtility.GetRect(name, "nametag");
			GUI.Label( new Rect(screenPos.x-rect.width*0.5f, Screen.height-screenPos.y-rect.height*0.5f, rect.width, rect.height), name, "nametag");
		}
	}
}