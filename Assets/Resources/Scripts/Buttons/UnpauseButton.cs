using UnityEngine;
using System.Collections;

public class UnpauseButton : Button
{
	public override void action()
	{
		GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>().unpause();		
	}
}
