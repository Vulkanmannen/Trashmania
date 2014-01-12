using UnityEngine;
using System.Collections;

public class ItemButtonShop : MonoBehaviour {

	public int cost;
	public string itemName;
	public Texture image;
	public Texture costImage;

	public ItemButtonStruct.ItemButtonType type = ItemButtonStruct.ItemButtonType.SKIN;
	
	public int value = 5;

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
		}
	}
}
