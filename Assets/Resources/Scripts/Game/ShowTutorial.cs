using UnityEngine;
using System.Collections;

public class ShowTutorial : MonoBehaviour 
{
	GlobalGameObject globalGameObject;
	GameObject myCamera;

	public bool firstPowerup = false;
	public bool firstCombo = false;
	public bool firstTimeOver19 = false;


	void Start () 
	{
		globalGameObject = GetComponent<GlobalGameObject>();
		myCamera = GameObject.FindWithTag("MainCamera");
	}
	

	void Update ()
	{
		if(PlayerPrefs.GetInt("PowerupTutorial") == 0)
		{
			if(firstPowerup)
			{
				popUp("Blue");
				globalGameObject.pauseFunc();
				PlayerPrefs.SetInt("PowerupTutorial", 1);
			}
		}

		if(PlayerPrefs.GetInt("ComboTutorial") == 0)
		{
			if(firstCombo)
			{
				popUp("Blue");
				globalGameObject.pauseFunc();
				PlayerPrefs.SetInt("ComboTutorial", 1);
			}
		}

		
		if(PlayerPrefs.GetInt("TrashMeterTutorial") == 0)
		{
			if(firstTimeOver19)
			{
				popUp("Blue");
				globalGameObject.pauseFunc();
				PlayerPrefs.SetInt("TrashMeterTutorial", 1);
			}
		}

	}

	void popUp(string name)
	{
		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUpTutorial") as GameObject, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));			
		newObject.transform.parent = myCamera.transform;
		newObject.GetComponent<Tutorial>().setTexture(name);
	}
}
