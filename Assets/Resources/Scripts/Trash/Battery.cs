using UnityEngine;
using System.Collections;

public class Battery : Trash
{
	// start
	protected override void start()
	{
		base.start();
		
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
			globalGameObject.GetComponent<GlobalGameObject>().gameOverTexture = "gameOverBattery";
			globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.GAMEOVER);
		}
		
		if(collision.collider.name == "Ground")
		{
			destroyAndPoff("");
		}
	}
}
