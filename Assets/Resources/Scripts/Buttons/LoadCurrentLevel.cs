using UnityEngine;
using System.Collections;

public class LoadCurrentLevel : Button
{
	public override void action()
	{
		PlayerPrefs.SetString("CurrentMenu", "Map");
		Application.LoadLevel(PlayerPrefs.GetInt("CurrentLevel"));
	}
}
