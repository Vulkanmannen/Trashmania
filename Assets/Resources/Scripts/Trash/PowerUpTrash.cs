using UnityEngine;
using System.Collections;

public class PowerUpTrash : Trash
{
	public Player.Mode mode = Player.Mode.NORMAL;
	public float timeInPowerUp = 12;
	private bool pickedUp = false;
	private CameraMovement myCamera;
	private float alpha = 0f;

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
			//GetComponentInChildren<AnimationScript>().renderer.material.SetColor("_Color",new Color(1f, 1f, 1f, alpha));
			//if(alpha < 0.3)
			//	alpha += 0.01f;

			Vector3 head = myCamera.transform.position + new Vector3(280f, -80f, 0f);
			head.z = -50f;

			Vector3 dir = head - transform.position;

			rigidbody.velocity = dir.normalized * 500f;

			if(dir.magnitude < 60)
			{
				GameObject.FindWithTag("myPlayer").GetComponent<Player>().addPowerUp(mode);
				Destroy(gameObject);
			}
		}
	}

	// spin
	protected override void spin()
	{
		if(!pickedUp)
			if(rigidbody.angularVelocity.z < 1f && rigidbody.angularVelocity.z > -1f)
				rigidbody.AddTorque(new Vector3(0f, 0f, 500f*rotationDir));
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
		GetComponentInChildren<AnimationScript>().renderer.material.SetColor("_Color",new Color(1f, 1f, 1f, 0.6f));
		GetComponent<BoxCollider>().isTrigger = true;
		createPoffWhenDestroyed();

		Destroy(transform.Find("particle_poweruptrail").gameObject);
		Destroy(transform.Find("Glow").gameObject);
	}
}
