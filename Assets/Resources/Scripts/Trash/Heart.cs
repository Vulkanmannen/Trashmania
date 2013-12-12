using UnityEngine;
using System.Collections;

public class Heart : Trash 
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
			globalGameObject.GetComponent<GlobalGameObject>().points -= lostPoints;
			globalGameObject.GetComponent<GlobalGameObject>().resetCombo();
			string textToShow = "-" + lostPoints.ToString();
			destroyAndPoff(textToShow, 1);
		}
	}
	public override void hitTrashCollider()
	{
		int totalPoints = points;
		globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
		destroyAndPoff(totalPoints.ToString());
	}
}
