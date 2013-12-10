using UnityEngine;
using System.Collections;

public class Battery : Trash
{
	// start
	protected override void start()
	{
		base.start();
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
			globalGameObject.GetComponent<GlobalGameObject>().points -= lostPoints;
			string textToShow = "-" + lostPoints.ToString();
			destroyAndPoff(textToShow);
		}
		
		if(collision.collider.name == "Ground")
		{
			destroyAndPoff("", 1);
		}
	}
	// change Behavior when taped on
	public override void hitTrashCollider()
	{
		destroyAndPoff("");
	}
}
