using UnityEngine;
using System.Collections;

public class ClosePopUp : Button
{
	public override void action()
	{
		transform.parent.GetComponent<PopUp>().close();
	}
}
