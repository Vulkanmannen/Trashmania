using UnityEngine;
using System.Collections;

public class ToStartMenuFromPause : Button
{
	public override void action()
	{			
		GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>().pause = false;
		Time.timeScale = 1;
		Application.LoadLevel((int)level);
	}
}
