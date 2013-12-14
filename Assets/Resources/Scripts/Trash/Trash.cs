using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour
{
	public int points;
	public int pointsFromGround;
	public int lostPoints;
	public Vector3 dir;
	public int hitGround = 0;
	public bool canBePickedUp = false;
	public float[,] velocityY = new float[,] {{200f, 220f, 240f, 260f, 300f, 400f}, {260f, 300f, 330f, 360f, 400f, 450f}};
	public float alternativeSpeed = 200;
	public int currentState = 0;
	public int kindsOfSprites = 4;
	public GameObject[] poffWhenDestroyd;
	
	public GameObject globalGameObject;
	public bool ignoreMe = false;
	public bool dontWarnAboutMe = false;
	public bool isPowerUp = false;
	public int thisLevel;

	protected Vector3 dirMod = new Vector3(0.99f, 0.97f, 0f);
	public bool onGround = false;
	protected bool toBeDestroyed = false;
	protected float destroyTimer;
	public bool bounce = false;
	protected bool destroyed = false;
	protected int rotationDir = 0;
	
	public float myXVelocity = 0;
	
	void Start ()
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject");
		
		rotationDir = (Random.Range(-100, 101) > 0 ? 1 : -1);
		
		thisLevel = globalGameObject.GetComponent<GlobalGameObject>().thisLevel;
		
		start();
	}
	
	protected virtual void start()
	{
		if(kindsOfSprites > 1)
			gameObject.GetComponentInChildren<AnimationScript>().index = Random.Range(0, kindsOfSprites);
	}

	void Update ()
	{
		thisLevel = globalGameObject.GetComponent<GlobalGameObject>().thisLevel;

		if(transform.position.y < -700 || transform.position.x > 700 || transform.position.x < -700)
			Destroy(this);

		myUpdate();
	}
	
	// myUpdate
	protected virtual void myUpdate()
	{
		
	}
	
	// fixedUpdate
	void FixedUpdate()
	{
		myFixedUpdate();
		
		spin ();
	}
	
	// fixed update
	protected virtual void myFixedUpdate()
	{
		dir.x *= dirMod.x;
		dir.y *= dirMod.y;
		
		
		if(dir.magnitude < 0.1f)
		{
			dir = new Vector3(0f, 0f, 0f);
		}  
		
		// get current stage
		currentState = globalGameObject.GetComponent<GlobalGameObject>().currentState;

		if(!canBePickedUp)
			rigidbody.velocity = new Vector3((bounce && !onGround ? rigidbody.velocity.x : 0f), -velocityY[thisLevel - 1, currentState], 0f) + dir;
		else
			rigidbody.velocity = new Vector3(0f, -0.5f, rigidbody.velocity.z); // wtf!!
		
		myXVelocity = rigidbody.velocity.x;
		
	}
	
	// spin
	protected virtual void spin()
	{
		if(rigidbody.angularVelocity.z < 1f && rigidbody.angularVelocity.z > -1f)
			rigidbody.AddTorque(new Vector3(0f, 0f, 500f*rotationDir));
	}
	
	
	//----------------------------------------------------------Collision---------------------------------------------------------
	//----------------------------------------------------------------------------------------------------------------------------
	void OnCollisionEnter(Collision collision)
	{
		myCollision(collision);
	}
	
	// collision
	protected virtual void myCollision(Collision collision)
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

	public virtual void hitTrashCollider()
	{
		globalGameObject.GetComponent<GlobalGameObject>().trashInARow++;	
		globalGameObject.GetComponent<GlobalGameObject>().numberOfCaughtTrash++;
		globalGameObject.GetComponent<GlobalGameObject>().numberOfNormalTrash++;
		int totalPoints = points * globalGameObject.GetComponent<GlobalGameObject>().comboMultiplyer;
		globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
		destroyAndPoff(totalPoints.ToString());
	}
	
	// this is run when a trash is picked up, it waits and destroy the trash
	public void destroy(float seconds = 0.5f)
	{
		toBeDestroyed = true;
		destroyTimer = Time.timeSinceLevelLoad + seconds;
	}
	
	// destroy the trash and create puff
	public void destroyAndPoff(string textToShow, int particleIndex = 0)
	{
		if(!destroyed)
		{
			createPoffWhenDestroyed(textToShow, particleIndex);
			Destroy(this.gameObject);
			destroyed = true;
		}
	}
	
	// this is run when a trash is destroyed, it creates a poff/popup
	public void createPoffWhenDestroyed(string textToShow = "", int particleIndex = 0)
	{
		if(textToShow == "0" || textToShow == "-0")
			textToShow = "";
		
		GameObject newObject = (GameObject)Instantiate(poffWhenDestroyd[particleIndex], transform.position - new Vector3(0f, 0f, 100f), Quaternion.Euler(new Vector3(0, 0, 0)));
		newObject.GetComponent<DestroyedTrashPopup>().setText(textToShow);
		newObject.GetComponent<DestroyedTrashPopup>().timeOnScreen = 0.6f;
		newObject.transform.parent = transform.parent.transform;
	}
}


