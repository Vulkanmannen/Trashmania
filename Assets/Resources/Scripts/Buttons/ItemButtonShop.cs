using UnityEngine;
using System.Collections;

public class ItemButtonShop : MonoBehaviour {

	public int cost;
	public string itemName;
	public Texture image;
	public Texture costImage;
	
	public enum ItemButtonType {SKIN, COIN, LIVES, POWERUP};
	public ItemButtonType type = ItemButtonType.SKIN;
	
	public int value = 5;

	public void setInfo(int newCost, string newName, string imageTexture, string costTexture, int newValue, ItemButtonType newType)
	{
		cost = newCost;
		itemName = newName;
		
		string textureName = "Textures/Shop" + imageTexture;
		image = Resources.Load(textureName) as Texture;
		
		textureName = "Textures/Shop" + costTexture;
		image = Resources.Load(textureName) as Texture;
		
		value = newValue;
		type = newType;
	}


}
