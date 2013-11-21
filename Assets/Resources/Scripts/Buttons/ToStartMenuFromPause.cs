using UnityEngine;
using System.Collections;

public class ToStartMenuFromPause : Button
{
	public override void action()
	{			

		GlobalGameObject globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
		globalGameObject.pause = false;
		globalGameObject.saveScore();
		Time.timeScale = 1;
		Application.LoadLevel((int)level);
	}
}
