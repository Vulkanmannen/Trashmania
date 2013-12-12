using UnityEngine;
using System.Collections;

public class BrokenHeart : Trash 
{
	public PersonInLove thrower = null;
	
	public float turnSpeed = 2f;
	
	public float maxSpeedDown = -280;
	public float maxSpeedUp = 100;
	public float changeSpeed = 2;
	public float fallSpeed = -280;
	public float fallSideSpeed = 150;
	
	public float randomSideSpeed = 50;
	public float randomFallSpeed = 50;
	public float randomShangeSpeed = 2;
	
	// myStart	
	protected override void start()
	{	
		changeSpeed += Random.Range(-randomShangeSpeed, randomShangeSpeed);
		
		fallSideSpeed += Random.Range(-randomSideSpeed, randomSideSpeed);
		
		maxSpeedDown += Random.Range(-randomFallSpeed, randomFallSpeed);
		fallSpeed = maxSpeedDown;
		
		if(thrower != null)
		{
			if(thrower.isLeft)
			{
				fallSideSpeed *= -1;
				fallSpeed = fallSpeed + 50;
			}
		}
	}
	// collision
	protected override void myCollision(Collision collision)
	{
		if(collision.collider.name != "wall" && !collision.collider.GetComponent<BrokenHeart>())
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
			// droped heart
			if(thrower != null)
				thrower.heartDroped = true;
			
			destroyAndPoff("",1);
		}
	}

	public override void hitTrashCollider()
	{
		// caught heart
		if(thrower != null)
			thrower.heartCaught = true;
		
		destroyAndPoff("");
	}
	
	// fixedUpdate
	protected override void myFixedUpdate()
	{
		// set speed
		if(!bounce)
		{
			fallSpeed += changeSpeed;
			
			if(fallSpeed >= maxSpeedUp || fallSpeed <= maxSpeedDown)
			{
				fallSpeed = maxSpeedDown;
				fallSideSpeed *= -1;
				rigidbody.velocity = new Vector3();
			}
		}
		else
			fallSpeed = maxSpeedDown;
		
		// set velocity
		rigidbody.velocity = new Vector3((bounce ? rigidbody.velocity.x : fallSideSpeed), fallSpeed, 0f);
	}
	
	// spin
	protected override void spin()
	{
		float z = 30f;
		Quaternion target = Quaternion.Euler(0, 0, z * rotationDir);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSpeed);	
		
		if(fallSideSpeed < 0)
			rotationDir = -1;
		else
			rotationDir = 1;
	}
}
