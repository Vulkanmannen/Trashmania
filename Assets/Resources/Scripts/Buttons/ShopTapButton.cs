using UnityEngine;
using System.Collections;

public class ShopTapButton : Button
{
	public bool activeButton = false; 
	public string tabName = "skins";

	void Start()
	{	
		renderer.material.mainTexture = texture;
	}
	
	void Update()
	{
		if(canBePressed && !Input.GetMouseButton(0))
		{
			action();
			onNotPressedNoMore();
		}

		if(activeButton)
			renderer.material.mainTexture = texture;
		else
			renderer.material.mainTexture = pressedTexture;
	}
	
	public override void onPress()
	{
		canBePressed = true;
	}
	public override void onNotPressedNoMore()
	{
		canBePressed = false;
	}

	public override void action()
	{
		transform.parent.GetComponent<ShopTab>().changeActiveTab(tabName);
	}	

}
