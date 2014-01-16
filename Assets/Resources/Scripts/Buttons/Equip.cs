using UnityEngine;
using System.Collections;

public class Equip : Button 
{
	Shop shop;

	public Texture equipedTexture;
	public Texture pressedEquipedTexture;

	public bool equiped = false;

	public override void action()
	{
		shop = transform.parent.parent.GetComponent<Shop>();
		int equipeThis = shop.zoomdItem;

		for(int i = 0; i < shop.items[1].Count; ++i)
		{
			ItemButtonStruct skin = shop.items[1][i];

			if(i != equipeThis)
				skin.equiped = false;
			else
				skin.equiped = true;

			shop.items[1][i] = skin;
		}

		PlayerPrefs.SetInt("Equiped", equipeThis);
		setToEquiped();
	}

	public override void onPress()
	{
		if(equiped)
			renderer.material.mainTexture = pressedEquipedTexture;
		else
			renderer.material.mainTexture = pressedTexture;
		canBePressed = true;
	}
	public override void onNotPressedNoMore()
	{
		if(equiped)
			renderer.material.mainTexture = equipedTexture;
		else
			renderer.material.mainTexture = texture;
		canBePressed = false;
	}

	public void setToNotEquiped()
	{
		renderer.material.mainTexture = texture;
		equiped = false;
	}

	public void setToEquiped()
	{
		renderer.material.mainTexture = equipedTexture;
		equiped = true;
	}
}
