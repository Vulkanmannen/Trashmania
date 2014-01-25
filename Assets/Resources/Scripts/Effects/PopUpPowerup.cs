using UnityEngine;
using System.Collections;

public class PopUpPowerup : MonoBehaviour {
	
	public bool closePopUp = false;

	Player player;

	void Start () 
	{
		player = GameObject.FindWithTag("myPlayer").GetComponent<Player>();

		GetComponent<Animation>().Play("PowerupInAnimation");
	}
	
	void Update () 
	{
		if(!GetComponent<Animation>().IsPlaying("PowerupOutAnimation") && closePopUp)
			Destroy(this.gameObject);
	}

	public void close()
	{
		GetComponent<Animation>().Play("PowerupOutAnimation");
		closePopUp = true;
	}
	
	public void setTexture(int index)
	{
		GetComponentInChildren<AnimationScript>().index = index;
	}
}
