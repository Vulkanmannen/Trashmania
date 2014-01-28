using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	public float[] speed = {1.3f, 1.3f, 1.3f, 1.3f, 1.3f, 1.3f};
	public float[] minTime = {4f, 4f, 4f, 4f, 4f, 4f};
	public float[] randomTimeDif = {0f, 0f, 0f, 0f, 0f, 0f};
	public float[,] distanceToClosestTrashY = new float[,]{{150f, 140f, 130f, 120f, 110f, 100f}, {100f, 100f, 100f, 100f, 100f, 100f}}; 
	public float[] distanceToClosestTrashYOkToThrow = {1000f, 1000f, 700f, 600f, 500f, 400f}; 
	public float[] minDistanceToClosestTrashX = {150f, 150f, 150f, 150f, 150f, 150f}; 
	public float[] maxDistanceToClosestTrashX = {250f, 250f, 250f, 250f, 250f, 250f}; 
	public int []pasThisLineToStartThrow = {250, 350, 450, 500, 500, 550};
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
	public int walkFramerate = 20;
	public int throwFramerate = 20;
	public GlobalGameObject.GameEvent currentEvent;
	public bool ignoreMe = false;
	
	public float yDif;
	public float xDif;
	public bool canThrow;
	public bool firstTrash = true;
	public int thisLevel = 1;
	public float layer = 0;
	
	public float timer = 0f;
	protected bool throwing = false;
	public int typeOfEnemyIndex = 0;
	protected float alpha = 1f;
	private int throwSide = 1;
	
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

		if(typeOfEnemyIndex == 0 || typeOfEnemyIndex == 1)
		{
			walkFrames = 15;
			throwSide = -1;
		}

		GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2, walkFrames, true, walkFramerate);

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
			transform.position = nodes[0] + new Vector3(0f, 0f, layer);
			nodes.RemoveAt(0);
		}
		else
		{
			for(int i = 0; i < startNode + 1; ++i)
			{
				transform.position = nodes[0] + new Vector3(0f, 0f, layer);
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
			moveVec = nodes[0] + new Vector3(0f, 0f, layer) - transform.position;		
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
					if(yDif > transform.position.y + trashStartOffset.y - trash.transform.position.y || firstTrash)
					{
						firstTrash = false;
						yDif = transform.position.y + trashStartOffset.y - trash.transform.position.y;
						xDif = transform.position.x + (trashStartOffset.x  * (isLeft ? -1 : 1) * throwSide) - trash.transform.position.x;
					}
				}
			}
			
			if(	!firstTrash && 
				(	
					Mathf.Abs(xDif) < minDistanceToClosestTrashX[currentState] ||
			 		Mathf.Abs(xDif) > maxDistanceToClosestTrashX[currentState] ||
			 		yDif < distanceToClosestTrashY[thisLevel - 1, currentState] + Mathf.Abs(trashStartOffset.y)
			 	)
			   	&& yDif < distanceToClosestTrashYOkToThrow[currentState] + Mathf.Abs(trashStartOffset.y)
			  ) 
			{
				canThrow = false;
			}
			
			// Throwing
			if(timer <= Time.timeSinceLevelLoad && canThrow)
			{
				if(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1) > -pasThisLineToStartThrow[currentState] && transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1) < pasThisLineToStartThrow[currentState])
				{
					// set trowing, set animation
					throwing = true;
					//timeToThrow = true;
					GetComponentInChildren<AnimationScript>().setAnimation(1 + (typeOfEnemy[typeOfEnemyIndex] * 2), throwFrames, false, throwFramerate);
					
					timer = Time.timeSinceLevelLoad + minTime[currentState] + Random.Range(0f, randomTimeDif[currentState]);
											
					GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilityThrow()], 
															transform.position, transform.rotation);
					
					newObject.transform.position = new Vector3(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1) * throwSide, transform.position.y + trashStartOffset.y, 0f);
					newObject.transform.parent = transform.parent.transform;
					
					//float randomDir = Random.Range(-600f, 601f);
					//if(randomDir <= 0)
					//	randomDir = -1;
					//else
					//	randomDir = 1;
					//
					//Vector3 tempDir = dir;									
					//tempDir.x += Random.Range(0f, randomDirDif.x);
					//tempDir.x *= randomDir;
					//tempDir.y += Random.Range(0f, randomDirDif.y);
					//										
					//newObject.GetComponent<Trash>().dir = tempDir;
				}
			}
		}
	}
	
	protected virtual int probabilityThrow()
	{
		// probability of eatch trash
		
		float probability = Random.value;
		int objectToThrowIndex = 0;
		bool powerUpOnScreen = globalGameObject.GetComponent<GlobalGameObject>().powerUpOnScreen;

		if(thisLevel == 1)
		{
			objectToThrowIndex = 0; // 100% normal
		}
		else if(thisLevel == 2)
		{
			if(probability < 0.85f) 
				objectToThrowIndex = 0; // 75% normal
			
			else
				objectToThrowIndex = 3; // 15% bad

		}
		
		return objectToThrowIndex;
	}
}
