using UnityEngine;
using System.Collections;

public class ChangeMenuButton : Button 
{
	public string changeTo = "OptionsMenu";
	
	public override void action()
	{
		if(transform.parent.GetComponent<MenuTree>())
			transform.parent.GetComponent<MenuTree>().currentMenu = changeTo;
		else if(transform.parent.parent.GetComponent<MenuTree>())
			transform.parent.parent.GetComponent<MenuTree>().currentMenu = changeTo;
	}
}
