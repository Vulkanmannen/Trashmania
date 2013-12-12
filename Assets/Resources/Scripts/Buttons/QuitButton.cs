using UnityEngine;
using System.Collections;

public class QuitButton : Button 
{
	public override void action()
	{
		Application.Quit();
	}
}
