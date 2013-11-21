using UnityEngine;
using System.Collections;

public class LoadCurrentLevel : Button
{
	public override void action()
	{
		Application.LoadLevel(PlayerPrefs.GetInt("CurrentLevel"));
	}
}
