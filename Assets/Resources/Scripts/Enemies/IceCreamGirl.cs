using UnityEngine;
using System.Collections;

public class IceCreamGirl : Enemy 
{
	bool thrown = false;

	protected override void throwFunc()
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
						xDif = transform.position.x + (trashStartOffset.x  * (isLeft ? -1 : 1)) - trash.transform.position.x;
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

			if(globalGameObject.GetComponent<GlobalGameObject>().powerUpOnScreen)
				canThrow = false;


			// Throwing
			if(timer <= Time.timeSinceLevelLoad && canThrow)
			{
				thrown = true; // fix!
				if(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1) > -pasThisLineToStartThrow[currentState] && transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1) < pasThisLineToStartThrow[currentState])
				{
					// set trowing, set animation
					throwing = true;
					//timeToThrow = true;
					GetComponentInChildren<AnimationScript>().setAnimation(1 + (typeOfEnemy[typeOfEnemyIndex] * 2), throwFrames, false, throwFramerate);
					
					timer = Time.timeSinceLevelLoad + minTime[currentState] + Random.Range(0f, randomTimeDif[currentState]);
					
					GameObject newObject = (GameObject)Instantiate(objectToSpawn[0], transform.position, transform.rotation);
					
					newObject.transform.position = new Vector3(transform.position.x + trashStartOffset.x * (isLeft ? -1 : 1), transform.position.y + trashStartOffset.y, 0f);
					newObject.transform.parent = transform.parent.transform;
				}
			}
		}
	}
}
