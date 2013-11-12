using UnityEngine;
using System.Collections;

public class PersonInLove : Enemy 
{
	
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
		
		// move enemy
		if(!throwing)
		{
			moveVec = nodes[0] - transform.position;		
			moveVec.Normalize();
			transform.position += moveVec * speed[currentState] * Time.timeScale;
		}
		
	}
	
	protected override void throwFunc()
	{
		// dont
	}
}
