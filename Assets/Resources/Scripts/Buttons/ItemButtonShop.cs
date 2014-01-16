using UnityEngine;
using System.Collections;

public class ItemButtonShop : MonoBehaviour {

	public int cost;
	public string itemName;
	public Texture image;
	public Texture costImage;

	public ItemButtonStruct.ItemButtonType type;
	
	public int value;
	public bool bought;
	public bool equiped;

	public int number = 0;

	public void setInfo(ItemButtonStruct item)
	{
		cost = item.cost;
		itemName = item.itemName;

		string getImage = "Textures/Shop/" + item.image;
		image = Resources.Load(getImage) as Texture;

		getImage = "Textures/Shop/" + item.costImage;
		costImage = Resources.Load(getImage) as Texture;

		value = item.value;
		type = item.type;
		bought = item.bought;
		equiped = item.equiped;

		foreach(Transform info in GetComponentsInChildren<Transform>())
		{
			if(info.name == "cost")
			{
				info.renderer.material.mainTexture = costImage;
			}
			else if(info.name == "costText")
			{
				info.GetComponent<TextMesh>().text = cost.ToString();
			}
			else if(info.name == "image")
			{
				info.renderer.material.mainTexture = image;
			}
			else if(info.name == "name")
			{
				info.GetComponent<TextMesh>().text = itemName;
			}

			if(type == ItemButtonStruct.ItemButtonType.SKIN)
			{
				Vector3 y = info.transform.localPosition;

				if(info.GetComponent<ByeButton>())
				{
					if(bought)
						y.y = -100;
					else
						y.y = 5;
				}
				else if(info.GetComponent<Equip>())
				{
					if(bought)
						y.y = 5;
					else
						y.y = -100;
					if(equiped)
						info.GetComponent<Equip>().setToEquiped();
					else
						info.GetComponent<Equip>().setToNotEquiped();
				}
				else if(info.name == "upgrade")
				{
					y.y = -100;
				}
			
				info.transform.localPosition = y;
			}
			else if(type == ItemButtonStruct.ItemButtonType.POWERUP)
			{
				Vector3 y = info.transform.localPosition;
				if(info.GetComponent<ByeButton>())
				{
					if(bought)
						y.y = -100;
					else
						y.y = 5;
				}
				else if(info.GetComponent<Equip>())
				{
					y.y = -100;
				}
				else if(info.name == "upgrade")
				{
					if(bought)
						y.y = 5;
					else
						y.y = -100;
				}
				info.transform.localPosition = y;
			}
		}
	}
}
