using UnityEngine;
using System.Collections;

public class FromGameToShop : Button	
{
	public override void action()
	{			
		GlobalGameObject globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
		globalGameObject.pause = false;
		globalGameObject.saveScore();
		Application.LoadLevel((int)level);
	}
}
