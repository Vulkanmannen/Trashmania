using UnityEngine;
using System.Collections;

public class SaveVariables : MonoBehaviour 
{
	void Start () 
	{
		PlayerPrefs.DeleteAll();

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

			// items
			for( int i = 0; i < 8; ++i)
			{
				string items = "BoughtItem" + i.ToString();
				PlayerPrefs.SetInt(items, 0);
			}

			// equiped
			PlayerPrefs.SetInt("Equiped", 0);

			// slots
			PlayerPrefs.SetInt("PowerUpSlots", 1);
			// coin multiplyer
			PlayerPrefs.SetInt("CoinMultiplyer",1);

			PlayerPrefs.SetInt("CurrentLevel", 1);

			//  current menu in startMenu
			PlayerPrefs.SetString("CurrentMenu", "StartMenu");

			// lives and coins
			PlayerPrefs.SetInt("Lives", 5);
			PlayerPrefs.SetInt("Coins", 7);

			// regenerate lives
			System.DateTime time = System.DateTime.Now;

			PlayerPrefs.SetInt("Day", time.Day);
			PlayerPrefs.SetInt("Hoer", time.Hour);

			// achievements
			for( int i = 0; i < 5; ++i)
			{
				string achievements = "Achievement" + i.ToString();
				PlayerPrefs.SetInt(achievements, 0);
			}

			// jewels
			for( int i = 0; i < 4; ++i)
			{
				string jewels = "Jewel" + i.ToString();
				PlayerPrefs.SetInt(jewels, 0);
			}

			// tutorials
			PlayerPrefs.SetInt("PowerupTutorial", 0);
			PlayerPrefs.SetInt("ComboTutorial", 0);
			PlayerPrefs.SetInt("TrashMeterTutorial", 0);
		}
	}
}
