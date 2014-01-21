using UnityEngine;
using System.Collections;

public class LoadCurrentLevel : Button
{
	public GameObject myCamera;
	
	public override void myStart()
	{
		myCamera = GameObject.FindWithTag("MainCamera");
	}

	public override void action()
	{
		if(active)
		{
			int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
			string currentLevelUnlocked = "LevelUnlocked" + currentLevel.ToString();

			if(PlayerPrefs.GetInt(currentLevelUnlocked) == 1)
			{
				int lives = PlayerPrefs.GetInt("Lives");

				if(currentLevel == 1 || lives > 0)
				{
					PlayerPrefs.SetString("CurrentMenu", "Map");
					Application.LoadLevel(currentLevel);
				}
				else
				{
					GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUpStay") as GameObject, myCamera.transform.position + new Vector3(0, 0, 50), Quaternion.Euler(new Vector3(90, 180, 0)));			
					newObject.transform.parent = myCamera.transform;
				}
			}
		}
	}

	protected override void myUpdate()
	{
		base.myUpdate();
		
		if(!active)
		{
			if(!Input.GetMouseButton(0))
				active = true;
		}
	}
	
	public override void onPress()
	{
		if(active)
			renderer.material.mainTexture = pressedTexture;
		canBePressed = true;
	}
	public override void onNotPressedNoMore()
	{
		if(active)
			renderer.material.mainTexture = texture;
		canBePressed = false;
	}
}
