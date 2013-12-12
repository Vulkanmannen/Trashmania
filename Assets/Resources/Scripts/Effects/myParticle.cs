using UnityEngine;
using System.Collections;

public class myParticle : MonoBehaviour {
	
	public float timeOnScreen = 4;
	
	void Update ()
	{
		timeOnScreen -= Time.deltaTime;
		
		if(timeOnScreen <= 0f)
		{
			Destroy(this.gameObject);
		}
	}
}
