using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
	
	float timeOnScreen = 4;
	
	void Update ()
	{
		timeOnScreen -= Time.deltaTime;
		
		if(timeOnScreen <= 0f)
		{
			Destroy(this.gameObject);
		}
	}
}
