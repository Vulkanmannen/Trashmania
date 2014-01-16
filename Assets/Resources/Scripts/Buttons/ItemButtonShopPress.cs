using UnityEngine;
using System.Collections;

public class ItemButtonShopPress : Button 
{
	public int item = 0;
	Shop shop;

	public override void myStart()
	{
		shop = transform.parent.GetComponent<Shop>();
	}

	public override void action()
	{
		if(!shop.zoom)
		{
			shop.zoomItem(item);
		}
	}

	public override void onPress()
	{
		if(!shop.zoom)
		{
			renderer.material.mainTexture = pressedTexture;
			canBePressed = true;
		}
	}
	public override void onNotPressedNoMore()
	{
		if(!shop.zoom)
		{
			renderer.material.mainTexture = texture;
			canBePressed = false;
		}
	}
}
