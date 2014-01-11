using UnityEngine;
using System.Collections;

public class SaveVariables : MonoBehaviour 
{
	void Start () 
	{
		if(PlayerPrefs.GetInt("LodedVariables") != 1)
		{
			PlayerPrefs.SetInt("LodedVariables", 1);
		
			PlayerPrefs.SetInt("LevelUnlocked1", 1);
			for( int i = 2; i <= 4; ++i)
			{
				string level = "LevelUnlocked" + i.ToString();
				PlayerPrefs.SetInt(level, 0);
			}

			for( int i = 1; i <= 4; ++i)
			{
				string level = "LevelStars" + i.ToString();
				PlayerPrefs.SetInt(level, 0);
			}

			for( int i = 0; i < 10; ++i)
			{
				string highScore = "AllTimeHighScore" + i.ToString();
				PlayerPrefs.SetInt(highScore, 0);
			}
		
			PlayerPrefs.SetInt("CurrentLevel", 1);

			//  current menu in startMenu
			PlayerPrefs.SetString("CurrentMenu", "StartMenu");

			// lives and coins
			PlayerPrefs.SetInt("Lives", 5);
			PlayerPrefs.SetInt("Coins", 7);
		}
		//PlayerPrefs.DeleteAll();
	}
}
