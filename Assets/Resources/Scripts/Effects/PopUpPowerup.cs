using UnityEngine;
using System.Collections;

public class PopUpPowerup : MonoBehaviour {

	public float timeOnScreen = 2;
	protected bool playdEndAnimation = false;
	
	void Start () 
	{
		timeOnScreen = timeOnScreen + Time.timeSinceLevelLoad;
		
		myStart();
	}
	
	// myStart
	protected virtual void myStart()
	{
		GetComponent<Animation>().Play("PowerupInAnimation");
	}
	
	void Update () 
	{
		myUpdate();
	}
	
	// myUpdate
	protected virtual void myUpdate()
	{
		if(timeOnScreen < Time.timeSinceLevelLoad && !playdEndAnimation)
		{
			close();
		}
		else if(!GetComponent<Animation>().IsPlaying("PowerupInAnimation") && playdEndAnimation)
			Destroy(this.gameObject);
	}
	
	public void close()
	{
		transform.parent.GetComponent<Player>().playedPowerup = true;
		playdEndAnimation = true;
	}
	
	public void setTexture(int index)
	{
		GetComponentInChildren<AnimationScript>().index = index;
	}
}
