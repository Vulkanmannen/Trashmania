using UnityEngine;
using System.Collections;

public class MenuTree : MonoBehaviour 
{
	
	public string currentMenu = "StartMenu";
	public string currentLevel =  "StartMenu";
	private string prevMenu;

	void Start() 
	{
		if(currentLevel == "StartMenu")
		{
			currentMenu = PlayerPrefs.GetString("CurrentMenu");

			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.GetComponent<Menu>())
				{
					if(t.name ==  currentMenu)
						t.position = new Vector3(0f, 0f, -100f);
					else
						t.position = new Vector3(2100f, 0f, -100f);
				}
			}
		}

		prevMenu = currentMenu;

		// regenerate lives

		int lives = PlayerPrefs.GetInt("Lives");

		if(lives < 5)
		{
			bool wasOverFourLives = PlayerPrefs.GetInt("WasOverFourLives") == 1;
			System.DateTime time = System.DateTime.Now;

			if(wasOverFourLives)
			{
				PlayerPrefs.SetInt("WasOverFourLives", 0);
				PlayerPrefs.SetInt("Day", time.Day);
				PlayerPrefs.SetInt("Hoer", time.Hour);
			}

			int day = PlayerPrefs.GetInt("Day");
			int hoer = PlayerPrefs.GetInt("Hoer");

			if(day != time.Day)
			{
				PlayerPrefs.SetInt("Lives", 5);
				PlayerPrefs.SetInt("Day", time.Day);
			}
			else if(hoer < time.Hour - 3)
			{
				lives++;
				PlayerPrefs.SetInt("Lives", lives);
			}
			PlayerPrefs.SetInt("Hoer", time.Hour);
		}
	}
	
	void Update() 
	{
		if(prevMenu != currentMenu)
		{
			foreach(Transform trans in GetComponentsInChildren<Transform>())
			{
				GameObject menu = trans.gameObject;
				if(menu.name == currentMenu)
					menu.GetComponent<Animation>().Play("MenuInAnimation");
				else if(menu.name == prevMenu)
					menu.GetComponent<Animation>().Play("MenuOutAnimation");
			}

			prevMenu = currentMenu;
		}
	}
}
