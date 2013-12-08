using UnityEngine;
using System.Collections;

public class OldMan : Enemy
{
	public float[] probabilityOfBigTrash = {0.40f, 0.50f, 0.60f, 0.70f, 0.80f, 1.0f};
	
	//--------------------------------------------------------------------------
	//----------------------------Throwing--------------------------------------
	protected override void throwFunc()
	{
		if(currentEvent == GlobalGameObject.GameEvent.NOEVENT)
		{
			// Check if its posible to throw
			
			canThrow = true;
			
			firstTrash = true;
			
			foreach(Transform trash in globalGameObject.transform)
			{
				if(trash.GetComponent<Trash>() && trash.tag != "BonusTrash")
				{
					if(yDif > transform.position.y - trash.transform.position.y || firstTrash)
					{
						firstTrash = false;
						yDif = transform.position.y + trashStartOffset.y - trash.transform.position.y;
						xDif = transform.position.x + (trashStartOffset.x  * (isLeft ? -1 : 1)) - trash.transform.position.x;
					}
				}
			}
			 
			if(!firstTrash && 
				(	
					//Mathf.Abs(xDif) < minDistanceToClosestTrashX[currentState] || 	
					//Mathf.Abs(xDif) > maxDistanceToClosestTrashX[currentState] || 	
			 		yDif < distanceToClosestTrashY[currentState] + Mathf.Abs(trashStartOffset.y))
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
					
					//if(globalGameObject.GetComponent<GlobalGameObject>().timeForDoubleBigTrash)
					//{
					//	globalGameObject.GetComponent<GlobalGameObject>().timeForDoubleBigTrash = false;
					//	//throw two big ones
					//	GameObject newObject0 = (GameObject)Instantiate(objectToSpawn[0], transform.position + new Vector3((trashStartOffset.x + 60) * (isLeft ? -1 : 1), trashStartOffset.y, -50f), transform.rotation);
					//	GameObject newObject1 = (GameObject)Instantiate(objectToSpawn[0], transform.position + new Vector3((trashStartOffset.x - 60) * (isLeft ? -1 : 1), trashStartOffset.y, -50f), transform.rotation);
					//	
					//	newObject0.transform.parent = transform.parent.transform;
					//	newObject1.transform.parent = transform.parent.transform;
					//	
					//	newObject0.GetComponent<Trash>().dir = new Vector3(100f * (isLeft ? -1 : 1), dir.y, dir.z);
					//	newObject1.GetComponent<Trash>().dir = new Vector3(-100f * (isLeft ? -1 : 1), dir.y, dir.z);
					//}
					//else
					{
						GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilityThrow()], transform.position, transform.rotation);
						
						newObject.transform.position = new Vector3(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1), transform.position.y + trashStartOffset.y, 0f);
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
	}
	
	//protected override int probabilityThrow()
	//{
	//	// probability of eatch trash
	//	
	//	float probability = Random.value;
	//	int objectToThrowIndex = 0;
	//	
	//	if(probability < probabilityOfBigTrash[currentState]) 
	//		objectToThrowIndex = 0; // 40% 50% 60% 70% 80% 100%
	//
	//	else
	//		objectToThrowIndex = 1; //
	//	
	//	return objectToThrowIndex;
	//}
}
