// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MyLookAt : MonoBehaviour {
	public bool followHorizontal =  false;
	public bool  followVertical = false;
	
	public Transform target;
	
	public float speed = 5.0f;
	
	
	void  Update (){
		Vector3 relative = transform.InverseTransformPoint(target.position);	
		Vector2 angle;
		
		if(followHorizontal)
			angle.y = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
		else
			angle.y = 0;
		
		if(followVertical)
			angle.x = Mathf.Atan2(-relative.y, Mathf.Abs(relative.z)) * Mathf.Rad2Deg;
		else
			angle.x = 0;
		
		transform.Rotate(angle.x * speed * Time.deltaTime, angle.y * speed * Time.deltaTime, 0);
	}
}