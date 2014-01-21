using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour 
{
	public string currentMenu;
	public MenuTree menuTree;

	void Start()
	{
		menuTree = GameObject.FindWithTag("MenuTree").GetComponent<MenuTree>();
	}

	void Update() 
	{
		currentMenu = menuTree.currentMenu;
	}
	
	void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		style.font = Resources.Load("Font/BADABB") as Font;
		style.fontSize = Screen.width / 8;

		if(currentMenu == "HighScore")
		{
			for(int i = 0; i < 10; ++i)
			{
				string highScore = "AllTimeHighScore" + i.ToString();
				GUI.Label(new Rect(Screen.width / 2, Screen.height / 10 + i * Screen.height / 15, 20, 20), PlayerPrefs.GetInt(highScore).ToString(), style); 
			} 
		}
	}
}
