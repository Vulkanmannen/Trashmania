using UnityEngine;
using System.Collections;

public class ChangeToLevelIfAvailable : Button
{
	public bool unlocked = true;
	public override void action()
	{
		if(unlocked)
		{
			Application.LoadLevel((int)level);
		}
	}
}
