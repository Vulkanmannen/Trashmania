using UnityEngine;
using System.Collections;

public class BonusTrashCatchFive : Trash
{
	public int orderOfObjects = 0;
	
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
			globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.NOEVENT);
			destroyAndPoff("");
		}
	}
	public override void hitTrashCollider()
	{
		int totalPoints = points;
		globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
		globalGameObject.GetComponent<GlobalGameObject>().numberOfCatchedBonusTrash++;
		destroyAndPoff(totalPoints.ToString());
	}
}
