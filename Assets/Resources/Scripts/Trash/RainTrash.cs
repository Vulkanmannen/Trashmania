using UnityEngine;
using System.Collections;

public class RainTrash : Trash 
{
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
			destroyAndPoff("", 1);
		}
	}
	public override void hitTrashCollider()
	{
		int totalPoints = points;
		globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
		destroyAndPoff(totalPoints.ToString());
	}
	// fixed update
	protected override void myFixedUpdate()
	{
		// get current stage
		currentState = globalGameObject.GetComponent<GlobalGameObject>().currentState;
		

		rigidbody.velocity = new Vector3((bounce ? rigidbody.velocity.x : 0f), -alternativeSpeed, 0f) + dir;

		
		myXVelocity = rigidbody.velocity.x;
	}
}
