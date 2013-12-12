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

		// set camera
		myCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
	}

	// fixed update
	protected override void myFixedUpdate()
	{
		if(!pickedUp)
		{
			currentState = globalGameObject.GetComponent<GlobalGameObject>().currentState;

			rigidbody.velocity = new Vector3((bounce ? rigidbody.velocity.x : 0f), +-velocityY[thisLevel - 1, currentState], 0f) + dir;
			
			myXVelocity = rigidbody.velocity.x;
		}
		else
		{
			Vector3 head = myCamera.transform.position + new Vector3(280f, -80f, 0f);
			head.z = -50f;

			Vector3 dir = head - transform.position;

			rigidbody.velocity = dir.normalized * 500f;

			if(dir.magnitude < 100)
			{
				GameObject.FindWithTag("myPlayer").GetComponent<Player>().addPowerUp(mode);
				Destroy(gameObject);
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
			hitTrashCollider();
		}
		if(collision.collider.name == "Ground")
		{
			destroyAndPoff("", 1);
		}
	}

	public override void hitTrashCollider()
	{
		pickedUp = true;
		GetComponent<BoxCollider>().isTrigger = true;
		createPoffWhenDestroyed();
	}
}
