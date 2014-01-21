using UnityEngine;
using System.Collections;

public class ToStartMenuFromGame : Button
{
	public string menu = "Map";

	public override void action()
	{	
		if(active)
		{
			GlobalGameObject globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();

			if(globalGameObject.pause)
			{
				globalGameObject.saveScore();
				globalGameObject.pause = false;
			}

			Time.timeScale = 1;
			PlayerPrefs.SetString("CurrentMenu", menu);
			Application.LoadLevel(0);
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

	public override void onPress()
	{
		if(active)
			renderer.material.mainTexture = pressedTexture;
		canBePressed = true;
	}
	public override void onNotPressedNoMore()
	{
		if(active)
			renderer.material.mainTexture = texture;
		canBePressed = false;
	}
}
