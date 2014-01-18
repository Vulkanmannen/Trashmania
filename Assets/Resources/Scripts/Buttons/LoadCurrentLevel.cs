using UnityEngine;
using System.Collections;

public class LoadCurrentLevel : Button
{
	public override void action()
	{
		if(active)
		{
			PlayerPrefs.SetString("CurrentMenu", "Map");
			Application.LoadLevel(PlayerPrefs.GetInt("CurrentLevel"));
		}
	}

	protected override void myUpdate()
	{
		base.myUpdate();
		
		if(!active)
		{
			if(!Input.GetMouseButton(0))
				active = true;
		}
	}
	
	public virtual void onPress()
	{
		if(active)
			renderer.material.mainTexture = pressedTexture;
		canBePressed = true;
	}
	public virtual void onNotPressedNoMore()
	{
		if(active)
			renderer.material.mainTexture = texture;
		canBePressed = false;
	}
}
