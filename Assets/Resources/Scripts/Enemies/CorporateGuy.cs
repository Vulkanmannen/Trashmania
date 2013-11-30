using UnityEngine;
using System.Collections;

public class CorporateGuy : Enemy
{
	public bool throwingRain = false;
	public bool doneThrowing = false;
	public float duration = 0f;
	
	public float dirDistBetweenTrash = 50;
	
	enum AnimationMode {WALK, TOTHROW, THROW};
	AnimationMode animationMode = AnimationMode.WALK;
	
	protected override void myStart()
	{
		Debug.Log("Corporate");

		if(transform.position.x > 700)
			isLeft = true;
		
		if(isLeft)
		{
			nodes.Reverse();
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
		
		dir.x *= (isLeft ? -1 : 1);
	}

	// fadeOut
	protected override void fadeOut()
	{
		if(currentEvent != GlobalGameObject.GameEvent.NOEVENT && currentEvent != GlobalGameObject.GameEvent.GAMEOVER && currentEvent != GlobalGameObject.GameEvent.RAIN)
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
	
	// Animation
	
	protected override void animationFunc()
	{
		// set to throw
		if(animationMode == AnimationMode.TOTHROW && GetComponentInChildren<AnimationScript>().endOfAnimation && !doneThrowing)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2 + (int)AnimationMode.THROW, 16, true, 20);
			animationMode = AnimationMode.THROW;
			duration = Time.timeSinceLevelLoad + 10f;
			throwingRain = true;
		}
		if(animationMode == AnimationMode.THROW && doneThrowing)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2 + (int)AnimationMode.TOTHROW, 16, true, 30, true);
			animationMode = AnimationMode.TOTHROW;
		}
		if(animationMode == AnimationMode.TOTHROW && GetComponentInChildren<AnimationScript>().endOfAnimation && doneThrowing)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2 + (int)AnimationMode.WALK, 15, true, 20);
			animationMode = AnimationMode.WALK;
		}
	}
	
	protected override void move()
	{
		// pop node 0
		if((transform.position.x >= nodes[0].x && !isLeft) || (transform.position.x <= nodes[0].x && isLeft))
		{
			nodes.RemoveAt(0);
		}
		
		// destroy this
		if(nodes.Count == 0)
		{
			Destroy(this.gameObject);	
			return;
		}
		
		// move enemy
		if(!throwingRain)
		{
			moveVec = nodes[0] - transform.position;		
			moveVec.Normalize();
			transform.position += moveVec * speed[currentState] * Time.timeScale;
		}
		
		// set to animation tothtrow
		if((isLeft && transform.position.x < 600f || !isLeft && transform.position.x > - 500) && !doneThrowing && !throwingRain && animationMode != AnimationMode.TOTHROW)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2 + (int)AnimationMode.TOTHROW, 16, false, 30);
			animationMode = AnimationMode.TOTHROW;
		}
	}
	
	protected override void throwFunc()
	{
		if(duration < Time.timeSinceLevelLoad && throwingRain)
		{
			throwingRain = false;
			doneThrowing = true;
		}
		
		// Throwing
		if(timer <= Time.timeSinceLevelLoad && throwingRain)
		{
			timer = Time.timeSinceLevelLoad + minTime[currentState];
			
			GameObject[] newObjects = new GameObject[3];
			newObjects[0] = (GameObject)Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], transform.position + new Vector3(0f + trashStartOffset.x * (isLeft ? -1 : 1), trashStartOffset.y + 0f, -35f), transform.rotation);
			newObjects[1] = (GameObject)Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], transform.position + new Vector3(0f + trashStartOffset.x * (isLeft ? -1 : 1),trashStartOffset.y + 100f, -35f), transform.rotation);
			newObjects[2] = (GameObject)Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], transform.position + new Vector3(0f + trashStartOffset.x * (isLeft ? -1 : 1),trashStartOffset.y - 100f, -35f), transform.rotation);
			
			newObjects[0].transform.parent = transform.parent.transform;
			newObjects[1].transform.parent = transform.parent.transform;
			newObjects[2].transform.parent = transform.parent.transform;
			
			Vector3 tempDir = dir;
			tempDir.y += Random.Range(-randomDirDif.y, randomDirDif.y);
													
			newObjects[0].GetComponent<Trash>().dir = tempDir;
			tempDir.y -= dirDistBetweenTrash;
			newObjects[1].GetComponent<Trash>().dir = tempDir;
			tempDir.y -= dirDistBetweenTrash;
			newObjects[2].GetComponent<Trash>().dir = tempDir;
		}
	}
}
