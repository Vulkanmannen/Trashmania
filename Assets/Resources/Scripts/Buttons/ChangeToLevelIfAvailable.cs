using UnityEngine;
using System.Collections;

public class ChangeToLevelIfAvailable : Button
{
	public string unlockedLevel = "LevelUnlocked1";

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
			PlayerPrefs.SetString("CurrentMenu", "Map");
			Application.LoadLevel((int)level);
		}
	}
}
