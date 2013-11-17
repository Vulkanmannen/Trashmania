using UnityEngine;
using System.Collections;

public class PersonInLove : Enemy 
{
	public bool waiting = false;
	public bool thrownHart = false;
	public bool heartCaught = false;
	public bool heartDroped = false;
	public bool runCaughtHarts = false;
	public bool thrownPrize = false;
	
	public PersonInLove partner;
	
	// start
	protected override void myStart()
	{
		// is  left
		if(transform.position.x > 700)
		{
			isLeft = true;
			nodes.Reverse();
			// set kind of animation
		}

		// set isLeft in animation
		GetComponentInChildren<AnimationScript>().isLeft = isLeft;
		
		// set start pos
		transform.position = nodes[0];
		nodes.RemoveAt(0);
		
		dir.x *= (isLeft ? -1 : 1);
	}
	
	// move
	protected override void move()
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
		
		// reach middle
		if((isLeft && transform.position.x < 50 || !isLeft && transform.position.x > -50) && !waiting && !heartDroped)
		{
			GetComponentInChildren<AnimationScript>().setAnimation(1 + typeOfEnemy[0] * 2, 20, true, 20);
			waiting = true;	
		}
		
		// move enemy
		if(!waiting)
		{
			moveVec = nodes[0] - transform.position;		
			moveVec.Normalize();
			transform.position += moveVec * speed[currentState] * Time.timeScale;
		}
		
	}
	
	protected override void throwFunc()
	{
		if(((isLeft && transform.position.x < 350f) || (!isLeft && transform.position.x > -350f)) && !thrownHart)
		{
			thrownHart = true;
			
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/Trash/BrokenHeart") as GameObject,transform.position - new Vector3(0f, 0f, 35), transform.rotation);
			newObject.transform.parent = transform.parent;
			newObject.GetComponent<BrokenHeart>().thrower = this; // fix
		}
	}
	
	// update
	protected override void myUptade()
	{
		// waiting
		if(waiting)
		{
			if(heartDroped)
			{
				// set to noevent
				if(currentEvent != GlobalGameObject.GameEvent.NOEVENT)
					globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.NOEVENT);
				
				// play droped
				dropedHeart();
				partner.dropedHeart();
			}
			
			if(heartCaught && partner.heartCaught && !runCaughtHarts)
			{
				runCaughtHarts = true;
				
				if(currentEvent != GlobalGameObject.GameEvent.NOEVENT && !thrownPrize)
				{
					thrownPrize = true;
					partner.thrownPrize = true;
					
					globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.NOEVENT);
					
					GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/Trash/Heart") as GameObject,transform.position + new Vector3(50f * (isLeft ? -1 : 1), 0f, -35f), transform.rotation);
					newObject.transform.parent = transform.parent;
				}
				// play caught
				chaughtHearts();
			}	
		}
		
		// cloud of hearts
		if(runCaughtHarts && GetComponentInChildren<AnimationScript>().endOfAnimation)
		{
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/Particle") as GameObject,transform.position, transform.rotation);
			newObject.transform.parent = transform.parent;
			
			Destroy(this.gameObject);
			
		}
	}
	
	// droped
	private void dropedHeart()
	{
		heartDroped = true;
		
		GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[0] * 2, 20, true, 20);
		waiting = false;	
	}
	
	// chaught both hearts
	private void chaughtHearts()
	{
		GetComponentInChildren<AnimationScript>().setAnimation(2 + typeOfEnemy[0] * 2, 20, false, 20);
		partner.GetComponentInChildren<AnimationScript>().setAnimation(2 + typeOfEnemy[0] * 2, 20, false, 20);
	}
}
