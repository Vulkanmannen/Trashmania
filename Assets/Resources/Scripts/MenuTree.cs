using UnityEngine;
using System.Collections;

public class MenuTree : MonoBehaviour 
{
	
	public string currentMenu = "StartMenu";
	private string prevMenu;

	void Start() 
	{
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
