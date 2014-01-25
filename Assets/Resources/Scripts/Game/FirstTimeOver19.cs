using UnityEngine;
using System.Collections;

public class FirstTimeOver19 : MonoBehaviour 
{
	GlobalGameObject globalGameObject;

	void Start () 
	{
		globalGameObject = GetComponent<GlobalGameObject>();
	}
	

	void Update () 
	{
		if(globalGameObject.numberOfCaughtTrash > 19 && PlayerPrefs.GetInt("TrashMeterTutorial") == 0)
			GetComponent<ShowTutorial>().firstTimeOver19 = true;
	}
}
