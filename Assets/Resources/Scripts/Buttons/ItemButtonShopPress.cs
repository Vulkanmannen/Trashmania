using UnityEngine;
using System.Collections;

public class ItemButtonShopPress : Button 
{
	public int item = 0;

	public override void action()
	{
		Shop shop = transform.parent.GetComponent<Shop>();
		if(!shop.zoom)
		{
			shop.zoomItem(item);
		}
	}
}
