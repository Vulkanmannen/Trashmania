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
