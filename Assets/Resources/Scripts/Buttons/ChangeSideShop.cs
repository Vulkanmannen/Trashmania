using UnityEngine;
using System.Collections;

public class ChangeSideShop : Button
{
	public override void action()
	{
		transform.parent.GetComponent<Shop>().changeSide(transform.position.x > 0);
	}
}
