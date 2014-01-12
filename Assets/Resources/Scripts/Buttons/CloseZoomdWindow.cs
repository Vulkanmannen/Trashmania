using UnityEngine;
using System.Collections;

public class CloseZoomdWindow : Button
{
	public override void action()
	{
		transform.parent.parent.GetComponent<Shop>().zoomOutItem();
	}
}
