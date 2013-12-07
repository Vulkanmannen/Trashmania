using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	public float[] speed = {1.3f, 1.3f, 1.3f, 1.3f, 1.3f, 1.3f};
	public float[] minTime = {2.5f, 2f, 1f, 0.5f, 0.5f, 0.5f};
	public float[] randomTimeDif = {0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f};
	public float[] distanceToClosestTrashY = {100f, 100f, 100f, 100f, 100f, 100f}; 
	public float[] minDistanceToClosestTrashX = {0f, 0f, 70f, 120f, 200f, 230f}; 
	public float[] maxDistanceToClosestTrashX = {200f, 200f, 220f, 220f, 240f, 290f}; 
	public int []pasThisLineToStartThrow = {250, 300, 350, 350, 400, 400};
	public GameObject[] objectToSpawn;
	
	public Vector3 dir = new Vector3(30f, 180f, 0f);
	public Vector3 randomDirDif = new Vector3(40f, 50f, 0f);
	public Vector2 trashStartOffset = new Vector2();
	public int frameToThrow = 8;
	public int startNode = 0;
	
	public List<Vector3> nodes;
	public bool isLeft = false;
	public Vector3 moveVec;
	public Vector3 spriteOffset = new Vector3();
	
	public GameObject globalGameObject;
	public int currentState = 0;
	
	public int[] typeOfEnemy;
	public int walkFrames = 15;
	public int throwFrames = 15;
	public int walkFramerate = 10;
	public int throwFramerate = 10;
	public GlobalGameObject.GameEvent currentEvent;
	public bool ignoreMe = false;
	
	public float yDif;
	public float xDif;
	public bool canThrow;
	public bool firstTrash = true;
	public int thisLevel = 1;
	
	protected float timer = 0f;
	protected bool throwing = false;
	public int typeOfEnemyIndex = 0;
	protected float alpha = 1f;
	
	//private bool timeToThrow = false;
	
	
	void Start()
	{	

		// get path
		GameObject enemyPath = GameObject.FindWithTag("EnemyPath");
		nodes = new List<Vector3>(enemyPath.GetComponent<EnemyPath>().nodes);
		
		// get globalGameObject
		globalGameObject = GameObject.FindWithTag("GlobalGameObject");
		
		// set sprite offset
		GetComponentInChildren<AnimationScript>().transform.position = transform.position + spriteOffset;

		// set level
		thisLevel = globalGameObject.GetComponent<GlobalGameObject>().thisLevel;

		// set type
		typeOfEnemyIndex = Random.Range(0, (typeOfEnemy.Length));
		GetComponentInChildren<AnimationScript>().row = typeOfEnemy[typeOfEnemyIndex] * 2;
		
		// specific
		myStart();
	}
	
	protected virtual void myStart()
	{
		if(globalGameObject.GetComponent<GlobalGameObject>().startLeft || (isLeft && startNode != 0))
		{
			nodes.Reverse();
			isLeft = true;
		}
		
		// set isLeft in animation
		GetComponentInChildren<AnimationScript>().isLeft = isLeft;
		
		// set start pos
		if(startNode == 0)
		{
			transform.position = nodes[0];
			nodes.RemoveAt(0);
		}
		else
		{
			for(int i = 0; i < startNode + 1; ++i)
			{
				transform.position = nodes[0];
				nodes.RemoveAt(0);
			}
		}
	}
	
	void Update ()
	{	
		// get current stage
		currentState = globalGameObject.GetComponent<GlobalGameObject>().currentState;
		
		// move
		move();
		
		// animation
		animationFunc();
		
		//--------------------------------------------------------------------------
		//----------------------------currentEvent----------------------------------
		
		currentEvent = globalGameObject.GetComponent<GlobalGameObject>().currentEvent;
		
		// throw
		throwFunc();

		// fade out
		fadeOut();
		
		// myUpdate
		myUptade();
	}
	
	// update
	protected virtual void myUptade()
	{
		
	}

	protected virtual void fadeOut()
	{
		if(currentEvent != GlobalGameObject.GameEvent.NOEVENT && currentEvent != GlobalGameObject.GameEvent.GAMEOVER)
		{
			if(alpha > 0f)
				alpha -= 0.02f;
		}
		else
		{
			if(alpha < 1f)
				alpha += 0.02f;
		}

		GetComponentInChildren<AnimationScript>().renderer.material.SetColor("_Color",new Color(1f, 1f, 1f, alpha));
	}
	
	//--------------------------------------------------------------------------
	//-----------------------------Move-----------------------------------------	
	protected virtual void move()
	{
		// pop node 0
		if((transform.position.x >= nodes[0].x && !isLeft) || (transform.position.x <= nodes[0].x && isLeft))
			nodes.RemoveAt(0);
		
		// destroy this
		if(nodes.Count == 0)
		{
			Destroy(this.gameObject);		
			return;
		}
		
		// move enemy
		if(!throwing && (currentEvent == GlobalGameObject.GameEvent.NOEVENT || currentEvent == GlobalGameObject.GameEvent.GAMEOVER))
		{
			moveVec = nodes[0] - transform.position;		
			moveVec.Normalize();
			transform.position += moveVec * speed[currentState] * Time.timeScale;
		}
		
	}
	
	//--------------------------------------------------------------------------
	//----------------------------Animation-------------------------------------	
	protected virtual void animationFunc()
	{
		// set to walk
		if(throwing && GetComponentInChildren<AnimationScript>().endOfAnimation)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2, walkFrames, true, walkFramerate);
			throwing = false;		
		}
	}
	
	//--------------------------------------------------------------------------
	//----------------------------Throwing--------------------------------------
	protected virtual void throwFunc()
	{
		if(currentEvent == GlobalGameObject.GameEvent.NOEVENT)
		{

			
			// Check if its posible to throw
			
			canThrow = true;
			
			firstTrash = true;
			
			foreach(Transform trash in globalGameObject.transform)
			{
				if(trash.GetComponent<Trash>() && !trash.GetComponent<Trash>().ignoreMe)
				{
					if(yDif > transform.position.y - trash.transform.position.y || firstTrash)
					{
						firstTrash = false;
						yDif = transform.position.y - trash.transform.position.y;
						xDif = transform.position.x - trash.transform.position.x;
					}
				}
			}
			
			if(!firstTrash && 
				(	
					Mathf.Abs(xDif) < minDistanceToClosestTrashX[currentState]  
				|| 	Mathf.Abs(xDif) > maxDistanceToClosestTrashX[currentState]
				|| 	yDif < distanceToClosestTrashY[currentState] + Mathf.Abs(trashStartOffset.y))
				)
			{
				canThrow = false;
			}
			
			
			// Throwing
			if(timer <= Time.timeSinceLevelLoad && canThrow)
			{
				if(transform.position.x > -pasThisLineToStartThrow[currentState] && transform.position.x < pasThisLineToStartThrow[currentState])
				{
					// set trowing, set animation
					throwing = true;
					//timeToThrow = true;
					GetComponentInChildren<AnimationScript>().setAnimation(1 + (typeOfEnemy[typeOfEnemyIndex] * 2), throwFrames, false, throwFramerate);
					
					timer = Time.timeSinceLevelLoad + minTime[currentState] + Random.Range(0f, randomTimeDif[currentState]);
					
											
					GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilityThrow()], 
															transform.position, transform.rotation);
					
					newObject.transform.position = new Vector3(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1), transform.position.y + trashStartOffset.y, 0f);
					newObject.transform.parent = transform.parent.transform;
					
					float randomDir = Random.Range(-600f, 601f);
					if(randomDir <= 0)
						randomDir = -1;
					else
						randomDir = 1;
					
					Vector3 tempDir = dir;									
					tempDir.x += Random.Range(0f, randomDirDif.x);
					tempDir.x *= randomDir;
					tempDir.y += Random.Range(0f, randomDirDif.y);
															
					newObject.GetComponent<Trash>().dir = tempDir;
				
					//timeToThrow = false;
				}
			}
			// delay trash from appearing
			//if(timeToThrow && GetComponentInChildren<AnimationScript>().index == GetComponentInChildren<AnimationScript>().numberOfFrames - frameToThrow)
			//{
			//
			//	
			//}
		}
		
	}
	
	protected virtual int probabilityThrow()
	{
		// probability of eatch trash
		
		float probability = Random.value;
		int objectToThrowIndex = 0;
		bool powerUpOnScreen = globalGameObject.GetComponent<GlobalGameObject>().powerUpOnScreen;

		// no battery
		if(thisLevel == 1)
		{
			if(probability < 0.90f) 
				objectToThrowIndex = 0; // 90% normal
			
			else if(probability < 0.95f && !powerUpOnScreen)
				objectToThrowIndex = 1; // 5% speed
			
			else if(!powerUpOnScreen)
				objectToThrowIndex = 3; // 5% ice cream
		}
		else if(thisLevel == 2)
		{
			if(probability < 0.75f) 
				objectToThrowIndex = 0; // 75% normal
			
			else if(probability < 0.90f)
				objectToThrowIndex = 4; // 15% battery

			else if(probability < 0.95f && !powerUpOnScreen)
				objectToThrowIndex = 1; // 5% speed
			
			else if(!powerUpOnScreen)
				objectToThrowIndex = 2; // 5% truck

		}
		
		return objectToThrowIndex;
	}
}
