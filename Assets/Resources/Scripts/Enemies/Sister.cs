using UnityEngine;
using System.Collections;

public class Sister : Enemy 
{
	public enum AnimationMode {WALK, JUMP, THROW, WAITIDLE, HAPPYFACE, SADFACE};
	public AnimationMode animationMode = AnimationMode.WALK;
	public Vector3[] objectStartPos = new Vector3[5]; 
	
	public GameObject bonusObjectToThrow;
	
	private int nextNode = 0;
	private bool thrownPowerup = false;
	private bool thrownObjects = false;
	
	protected override void myStart()
	{
		// path dir
		//int dir = Random.Range(-60, 61);
		if(globalGameObject.GetComponent<GlobalGameObject>().startLeft)
		{
			nodes.Reverse();
			isLeft = true;
		}

		// set isLeft in animation
		GetComponentInChildren<AnimationScript>().isLeft = isLeft;
		
		timer = Time.timeSinceLevelLoad + minTime[currentState] + Random.Range(0f, randomTimeDif[currentState]);
		transform.position = nodes[0];
		
		// set to ignore
		ignoreMe = true;
	}
	
	//--------------------------------------------------------------------------
	//-----------------------------Move-----------------------------------------	
	protected override void move()
	{
		// set sprite offset to jump
		GetComponentInChildren<AnimationScript>().transform.position = transform.position + spriteOffset;
		
		// pop node 0
		if((transform.position.x >= nodes[nextNode].x && !isLeft) || (transform.position.x <= nodes[nextNode].x && isLeft))
		{
			// walk between node 3 and 4
			if(thrownPowerup || nextNode < 3)
				nextNode++;
			else
			{
				nodes.Reverse();
				isLeft = !isLeft;
				GetComponentInChildren<AnimationScript>().isLeft = isLeft;
			}	
		}
		
		// destroy this
		if((transform.position.x >= nodes[5].x && !isLeft) || (transform.position.x <= nodes[5].x && isLeft))
		{
			globalGameObject.GetComponent<GlobalGameObject>().sisterInPlay = false;
			Destroy(this.gameObject);		
			return;
		}
		
		// move enemy
		if(animationMode == AnimationMode.WALK && currentEvent != GlobalGameObject.GameEvent.INLOVE)
		{
			moveVec = nodes[nextNode] - transform.position;		
			moveVec.Normalize();
			transform.position += moveVec * speed[currentState] * Time.timeScale;
		}
		
	}
	//--------------------------------------------------------------------------
	//----------------------------update----------------------------------------	
	protected override void myUptade()
	{
		if(currentEvent == GlobalGameObject.GameEvent.GAMEOVER && !thrownPowerup)
			thrownPowerup = true;
	}
	
	//--------------------------------------------------------------------------
	//----------------------------Animation-------------------------------------	
	protected override void animationFunc()
	{
		// set to jump
		if(animationMode == AnimationMode.WALK 
			&& timer <= Time.timeSinceLevelLoad 
			&& transform.position.x > -pasThisLineToStartThrow[currentState] 
			&& transform.position.x < pasThisLineToStartThrow[currentState] 
			&& !thrownPowerup)
		{
			GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.JUMP, 15, false, 20);
			animationMode = AnimationMode.JUMP;
			GetComponent<Animation>().Play("SisterJumpAnimation");
		}
		// set to waitidle
		else if(animationMode == AnimationMode.THROW && GetComponentInChildren<AnimationScript>().endOfAnimation && !thrownObjects)
		{
			GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.WAITIDLE, 15, true, 20);
			animationMode = AnimationMode.WAITIDLE;
		}
		// set to walk
		else if(((animationMode == AnimationMode.SADFACE || (animationMode == AnimationMode.HAPPYFACE && (thrownObjects || currentEvent != GlobalGameObject.GameEvent.CATCHFIVE))) 
				&& GetComponentInChildren<AnimationScript>().endOfAnimation) 
				|| (currentEvent == GlobalGameObject.GameEvent.GAMEOVER && animationMode != AnimationMode.WALK))
		{
			GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.WALK, 15, true, 20);
			animationMode = AnimationMode.WALK;
			GetComponent<Animation>().Play("SisterLeapAnimation");
		}
	}
	
	//--------------------------------------------------------------------------
	//----------------------------Throwing--------------------------------------
	protected override void throwFunc()
	{
		if(currentEvent == GlobalGameObject.GameEvent.NOEVENT)
		{		
			// Throwing
			if(animationMode == AnimationMode.JUMP && GetComponentInChildren<AnimationScript>().endOfAnimation)
			{
				//timeToThrow = true;
				GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.THROW, 15, false, 20);
				animationMode = AnimationMode.THROW;
				
				
				
				// create object						
				GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilityThrow()], transform.position, transform.rotation);
				
				newObject.transform.position = new Vector3(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1), transform.position.y + trashStartOffset.y, 0f);
				newObject.transform.parent = transform.parent.transform;
				
				float randomDir = Random.Range(-600f, 601f);
				if(randomDir <= 0)
				{
					randomDir = -1;
				}
				else
				{
					randomDir = 1;
				}
				
				Vector3 tempDir = dir;									
				tempDir.x += Random.Range(0f, randomDirDif.x);
				tempDir.x *= randomDir;
				tempDir.y += Random.Range(0f, randomDirDif.y);
														
				newObject.GetComponent<Trash>().dir = tempDir;
				
				thrownPowerup = true;
			}
		}
		
		if(currentEvent == GlobalGameObject.GameEvent.CATCHFIVE)
		{
			if(animationMode == AnimationMode.HAPPYFACE && GetComponentInChildren<AnimationScript>().endOfAnimation && !thrownObjects)
			{	
				for(int i = 0; i < 5; ++i)
				{
					GameObject newObject = (GameObject)Instantiate(bonusObjectToThrow, transform.position + objectStartPos[i], transform.rotation);
					newObject.transform.parent = transform.parent.transform;
				}
				thrownObjects = true;
			}
		}
	}
	
	// setHappyFace
	public void setHappyFaceAnimation()
	{
		GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.HAPPYFACE, 15, true, 20);
		animationMode = AnimationMode.HAPPYFACE;
		
	}
	
	// set to sadFace
	public void setSadFaceAnimation()
	{
		GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.SADFACE, 15, true, 20);
		animationMode = AnimationMode.SADFACE;
	}
	
	// probability of eatch trash
	protected override int probabilityThrow()
	{
		
		float probability = Random.value;
		int objectToThrowIndex = 0;
		
		// if 2 objects
		if(objectToSpawn.Length == 2)
		{
			if(probability < 0.60f) 
				objectToThrowIndex = 0; // 60%

			else
				objectToThrowIndex = 1; // 40 %
		}
		
		// if three objects
		else if(objectToSpawn.Length == 3)
		{
			if(probability < 0.50f) 
				objectToThrowIndex = 0; // 50%

			else if(probability < 0.75f) 
				objectToThrowIndex = 1; // 25 %
			
			else
				objectToThrowIndex = 2; // 25 %
		}
		
		
		return objectToThrowIndex;
	}
}
