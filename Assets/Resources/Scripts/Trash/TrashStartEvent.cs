using UnityEngine;
using System.Collections;

public class TrashStartEvent : Trash
{
	public GlobalGameObject.GameEvent startEvent = GlobalGameObject.GameEvent.CATCHFIVE;
	
	protected override void start()
	{
		// set ignore
		ignoreMe = true;
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
			destroyAndPoff("");
		}
	}

	public override void hitTrashCollider()
	{
		GameObject.FindWithTag("Sister").GetComponent<Sister>().setHappyFaceAnimation();
		globalGameObject.GetComponent<GlobalGameObject>().startEvent(startEvent);
	}
}
