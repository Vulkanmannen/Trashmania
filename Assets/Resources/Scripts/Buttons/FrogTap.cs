using UnityEngine;
using System.Collections;

public class FrogTap : Button 
{
	GetAchievement achievement;

	public override void myStart()
	{
		achievement = GameObject.FindWithTag("GlobalGameObject").GetComponent<GetAchievement>();
	}

	public override void action()
	{
		achievement.frogTap++;
	}
}
