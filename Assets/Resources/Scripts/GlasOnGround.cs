using UnityEngine;
using System.Collections;

public class GlasOnGround : MonoBehaviour 
{
	float timeOnScreen = 4;
	
	void Update ()
	{
		timeOnScreen -= Time.deltaTime;
		
		// blink
		if((timeOnScreen < 1f && timeOnScreen > 0.75f) || (timeOnScreen < 0.5f && timeOnScreen > 0.25f))
		{
			transform.Find("Sprite").GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			transform.Find("Sprite").GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(timeOnScreen <= 0f)
		{
			Destroy(this.gameObject);
		}
	}
}
