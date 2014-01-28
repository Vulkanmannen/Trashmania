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
		Vector3 scale = transform.localScale;
		// is  left
		if(transform.position.x > 700)
		{
			isLeft = true;

			nodes.Reverse();
			// set kind of animation
			typeOfEnemyIndex = 1;

			scale.x = 95;
			scale.y = 200;
			
		}
		else
		{
			typeOfEnemyIndex = 0;
			scale.x = 120;
			scale.y = 220;
		}

		transform.localScale = scale;

		// set animation
		GetComponentInChildren<AnimationScript>().row = typeOfEnemy[typeOfEnemyIndex] * 2;

		// set isLeft in animation
		GetComponentInChildren<AnimationScript>().isLeft = isLeft;
		
		// set start pos
		transform.position = nodes[0] + new Vector3(0f, 0f, -layer);
		nodes.RemoveAt(0);
		
		dir.x *= (isLeft ? -1 : 1);
	}

	// fade out
	protected override void fadeOut()
	{
		if(currentEvent != GlobalGameObject.GameEvent.NOEVENT && currentEvent != GlobalGameObject.GameEvent.GAMEOVER && currentEvent != GlobalGameObject.GameEvent.INLOVE)
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
			GetComponentInChildren<AnimationScript>().setAnimation(1 + typeOfEnemy[typeOfEnemyIndex] * 2, 15, true, 20);
			waiting = true;	
		}
		
		// move enemy
		if(!waiting)
		{
			moveVec = nodes[0] + new Vector3(0f, 0f, -layer) - transform.position;		
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
					
					GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/Trash/Heart") as GameObject,transform.position + new Vector3(50f * (isLeft ? -1 : 1), 0f, -35f), transform.rotation);
					newObject.transform.parent = transform.parent;

					Vector3 particlePos = transform.position;
					particlePos.x = 0;
					particlePos.z -= 100;

					GameObject newObjectParticle = (GameObject)Instantiate(Resources.Load("Particles/particle_heartexplosion") as GameObject, particlePos, transform.rotation);
					newObjectParticle.transform.parent = transform.parent;

					HeartParticle hearts = newObjectParticle.GetComponent<HeartParticle>();

					hearts.timeOnScreen = 2.5f;

					hearts.person1 = gameObject.GetComponent<PersonInLove>();
					hearts.person2 = partner.gameObject.GetComponent<PersonInLove>();

				}
			}
			//if(thrownPrize)
			//	Destroy(gameObject);
		}
	}
	
	// droped
	private void dropedHeart()
	{
		heartDroped = true;
		
		GetComponentInChildren<AnimationScript>().setAnimation(typeOfEnemy[typeOfEnemyIndex] * 2, 15, true, 20);
		waiting = false;	
	}
}
