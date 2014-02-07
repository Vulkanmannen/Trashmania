using UnityEngine;
using System.Collections;

public class TrashStartEvent : Trash
{
	public GlobalGameObject.GameEvent startEvent = GlobalGameObject.GameEvent.CATCHFIVE;
	
	protected override void start()
	{
		// set ignore
		ignoreMe = true;

		Vector3 pos = transform.localPosition;
		pos.z = -90;
		transform.localPosition = pos;
	}	
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
			hitTrashCollider();
		}
		if(collision.collider.name == "Ground")
		{
			GameObject.FindWithTag("Sister").GetComponent<Sister>().setSadFaceAnimation();
			destroyAndPoff("", 1);
		}
	}

	public override void hitTrashCollider()
	{
		GameObject.FindWithTag("Sister").GetComponent<Sister>().setHappyFaceAnimation();
		globalGameObject.GetComponent<GlobalGameObject>().startEvent(startEvent);
	}
	// fixed update
	protected override void myFixedUpdate()
	{
		dir.x *= dirMod.x;
		dir.y *= dirMod.y;
		
		
		if(dir.magnitude < 0.1f)
		{
			dir = new Vector3(0f, 0f, 0f);
		}  
		
		// get current stage
		currentState = globalGameObject.GetComponent<GlobalGameObject>().currentState;
		
		if(!canBePickedUp)
			rigidbody.velocity = new Vector3((bounce && !onGround ? rigidbody.velocity.x : 0f), -alternativeSpeed, 0f) + dir;
		else
			rigidbody.velocity = new Vector3(0f, -0.5f, rigidbody.velocity.z); // wtf!!
		
		myXVelocity = rigidbody.velocity.x;
		
	}
}
