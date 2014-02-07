using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemButtonStruct : MonoBehaviour
{
	public ItemButtonStruct(int newCost, string newName, string imageTexture, string costTexture, int newValue, ItemButtonType newType)
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

	public int cost;
	public string itemName;
	public Texture image;
	public Texture costImage;

	public enum ItemButtonType {SKIN, COIN, LIVES, POWERUP};
	public ItemButtonType type = ItemButtonType.SKIN;

	public int value = 5;
}

public class Shop : MonoBehaviour 
{
	public List<ItemButtonStruct> skinsAndPowerups;
	public List<ItemButtonStruct> coinsAndLives;

	void Start () 
	{
		ItemButtonStruct item = new ItemButtonStruct(5, "PolarBear", "menu_shop_sprite_coin", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		coinsAndLives.Add(item);
	}	
	
	
	void Update () 
	{
		
	}
}
