using UnityEngine;
using System.Collections;

public class CloseTutorial : Button
{
	public override void action()
	{
		GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>().unpause();
		transform.parent.GetComponent<Tutorial>().close();
	}
}
