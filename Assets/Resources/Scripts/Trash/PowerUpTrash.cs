using UnityEngine;
using System.Collections;

public class PowerUpTrash : Trash
{
	public bool isAdditional = false;
	public Player.Mode mode = Player.Mode.NORMAL;
	public Player.AdditionalMode additionalMode = Player.AdditionalMode.NORMAL;
	
	
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
			if(isAdditional)
				GameObject.FindWithTag("myPlayer").GetComponent<Player>().setMode(additionalMode);
			else
				GameObject.FindWithTag("myPlayer").GetComponent<Player>().setMode(mode);
			
			destroyAndPoff("");
		}
		if(collision.collider.name == "Ground")
		{
			destroyAndPoff("");
		}
	}
}
