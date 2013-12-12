using UnityEngine;
using System.Collections;

public class ChangeMenuButton : Button 
{
	public string changeTo = "OptionsMenu";
	
	public override void action()
	{
		transform.parent.parent.GetComponent<MenuTree>().currentMenu = changeTo;
	}
}
