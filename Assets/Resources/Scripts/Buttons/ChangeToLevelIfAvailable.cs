using UnityEngine;
using System.Collections;

public class ChangeToLevelIfAvailable : Button
{
	public string unlockedLevel = "LevelUnlocked1";
	public GameObject myCamera;

	public override void myStart()
	{
		myCamera = GameObject.FindWithTag("MainCamera");
	}

	public override void onPress()
	{
		PieceOfMap p = GetComponentInChildren<PieceOfMap>();
		p.press();
		canBePressed = true;
	}

	public override void action()
	{
		if(PlayerPrefs.GetInt(unlockedLevel) == 1)
		{
			int lives = PlayerPrefs.GetInt("Lives");
			if((int)level == 1 || lives > 0)
			{
				PlayerPrefs.SetString("CurrentMenu", "Map");
				Application.LoadLevel((int)level);
			}
			else
			{
				GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUpStay") as GameObject, myCamera.transform.position + new Vector3(0, 0, 50), Quaternion.Euler(new Vector3(90, 180, 0)));			
				newObject.transform.parent = myCamera.transform;
			}
		}
	}
}
