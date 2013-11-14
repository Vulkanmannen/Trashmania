using UnityEngine;
using System.Collections;

public class BrokenHeart : Trash 
{
	public PersonInLove thrower = null;
	
	// collision
	protected override void myCollision(Collision collision)
	{
		if(collision.collider.name != "wall")
		{
			dir = new Vector3(0,0,0);
			bounce = true;
		}
		if(collision.collider.gameObject.CompareTag("TrashCollider"))
		{
			// caught heart
			if(thrower != null)
				thrower.heartCaught = true;
			
			destroyAndPoff("");
		}
		if(collision.collider.name == "Ground")
		{
			// droped heart
			if(thrower != null)
				thrower.heartDroped = true;
			
			destroyAndPoff("");
		}
	}
	
	// fixedUpdate
	protected override void myFixedUpdate()
	{
		
	}
	
	// spin
	protected override void spin()
	{
		//Debug.Log(transform.rotation.z);
		if(transform.rotation.z > 0.8f || transform.rotation.z < -0.8f)
		{
			rotationDir *= -1;
			Debug.Log(transform.rotation.z);
		}
		//transform.rotation = new Quaternion(0f, 0f, transform.rotation.z + 0.01f * rotationDir, 0f);
		//rigidbody.AddTorque(new Vector3(0f, 0f, 100f*rotationDir));
	}
}
