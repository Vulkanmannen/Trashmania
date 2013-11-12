using UnityEngine;
using System.Collections;

public class BigTrash : Trash 
{
	protected override void myUpdate()
	{
		if(toBeDestroyed && Time.timeSinceLevelLoad >= destroyTimer)
		{
			destroyAndPoff(pointsFromGround.ToString());
		}
	}
	
	// collision
	protected override void myCollision(Collision collision)
	{
		if(collision.collider.name == "Ground" && !canBePickedUp)
		{
			canBePickedUp = true;	
			transform.position = new Vector3(transform.position.x, transform.position.y, -50);
			
			GameObject redArrow = Instantiate(Resources.Load ("Objects/RedArrow") as GameObject, transform.position, Quaternion.Euler(new Vector3(90, 180, 0))) as GameObject;
			redArrow.transform.parent = transform;

			// increase trashOnGround
			globalGameObject.GetComponent<GlobalGameObject>().trashOnGround++;
		}
		
		//if(collision.collider.name == "BigTrash" && gameObject.name == "BigTrash")
		//{
		//	transform.position = collision.collider.transform.position + new Vector3(120f, 0f, 0f);
		//}
		
		if(collision.collider.gameObject.CompareTag("TrashCollider"))
		{
			globalGameObject.GetComponent<GlobalGameObject>().trashInARow++;	
			int totalPoints = points * globalGameObject.GetComponent<GlobalGameObject>().comboMultiplyer;
			globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
			destroyAndPoff(totalPoints.ToString());
		}
	}
	
	protected override void spin()
	{
		// dont
	}
}