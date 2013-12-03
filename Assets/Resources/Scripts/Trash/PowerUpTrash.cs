using UnityEngine;
using System.Collections;

public class PowerUpTrash : Trash
{
	public Player.Mode mode = Player.Mode.NORMAL;
	public float timeInPowerUp = 12;
	private bool pickedUp = false;
	private CameraMovement myCamera;

	// start
	protected override void start()
	{
		base.start();
		
		// set ignore
		ignoreMe = true;

		// set camera
		myCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
	}

	// fixed update
	protected override void myFixedUpdate()
	{
		if(!pickedUp)
			base.myFixedUpdate();
		else
		{
			Vector3 head = myCamera.transform.position + new Vector3(280f, -80f, 0f);
			head.z = -50f;

			Vector3 dir = head - transform.position;

			rigidbody.velocity = dir.normalized * 500f;

			if(dir.magnitude < 140)
			{
				destroyAndPoff("");
				GameObject.FindWithTag("myPlayer").GetComponent<Player>().addPowerUp(mode);
			}
		}
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
			pickedUp = true;
			GetComponent<BoxCollider>().isTrigger = true;
		}
		if(collision.collider.name == "Ground")
		{
			destroyAndPoff("");
		}
	}
}
