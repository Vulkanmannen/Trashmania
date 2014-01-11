using UnityEngine;
using System.Collections;

public class HeartParticle : MonoBehaviour {

	public PersonInLove person1;
	public PersonInLove person2;
	private GlobalGameObject globalGameObject;

	public float timeOnScreen = 4;

	void Start()
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
	}

	void Update ()
	{
		timeOnScreen -= Time.deltaTime;

		if(timeOnScreen < 1f && person1 != null && person2 != null)
		{
			Destroy(person1.gameObject);
			Destroy(person2.gameObject);
			person1 = null;
			person2 = null;
		}

		if(timeOnScreen <= 0f)
		{
			globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.NOEVENT);
			Destroy(this.gameObject);
		}
	}
}
