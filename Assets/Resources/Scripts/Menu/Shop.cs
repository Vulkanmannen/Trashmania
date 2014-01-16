using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ItemButtonStruct
{
	public ItemButtonStruct(int newCost, string newName, string imageTexture, string costTexture, int newValue, ItemButtonType newType, bool newBought = false, bool newEquiped = false)
	{
		cost = newCost;
		itemName = newName;
		image = imageTexture;
		costImage = costTexture;
		
		value = newValue;
		type = newType;
		bought = newBought;
		equiped = newEquiped;
	}
	
	public int cost;
	public string itemName;
	public string image;
	public string costImage;
	public bool bought;
	public bool equiped;

	public enum ItemButtonType {SKIN, COIN, LIVES, POWERUP};
	public ItemButtonType type;
	
	public int value;
}


public class Shop : MonoBehaviour 
{

	public List<ItemButtonStruct>[] items = {new List<ItemButtonStruct>(), new List<ItemButtonStruct>(), new List<ItemButtonStruct>()};

	public int index = 1;
	public int side = 0;
	public int zoomdItem = 0;
	public bool zoom = false;

	private Vector3 origin;
	private RaycastHit raycastHit;
	private Animation zoomAnimation;

	void Start() 
	{
		init();
		setInfo();
		zoomAnimation = GameObject.FindWithTag("ZoomWindow").GetComponent<Animation>();
	}	
	
	
	void Update () 
	{

		
	}

	public void changeSide(bool increase)
	{
		if(!zoom)
		{
			if(increase)
			{
				side = (side + 1) % (items[index].Count / 4); 
			}
			else
			{
				side = (side + items[index].Count / 4 - 1) % (items[index].Count / 4); 
			}
		}
		else
		{
			if(increase)
			{
				zoomdItem = (zoomdItem + 1) % items[index].Count;
			}
			else
			{
				zoomdItem = (zoomdItem + items[index].Count - 1) % (items[index].Count);
			}
		}

		setInfo();
	}

	public void changeIndex(int newIndex)
	{
		index = newIndex;
		side = 0;
		zoomdItem = 0;
		setInfo();
	}

	public void zoomItem(int item)
	{
		zoom = true;
		zoomAnimation.Play("ZoomWindowInAnimation");
		zoomdItem = side * 4 + item;
		setInfo();
		Debug.Log("HejDå");
	}

	public void zoomOutItem()
	{

		zoomAnimation.Play("ZoomWindowOutAnimation");
		setInfo();
		zoom = false;
		Debug.Log("Hej");
	}

	void setInfo()
	{
		foreach(Transform button in GetComponentsInChildren<Transform>())
		{
			if(button.GetComponent<ItemButtonShop>())
			{
				int number = button.GetComponent<ItemButtonShop>().number;
				if(number != 4 && !zoom)
				{
					button.GetComponent<ItemButtonShop>().setInfo(items[index][number + side * 4]);
				}
				else if(number == 4 && zoom)
				{
					button.GetComponent<ItemButtonShop>().setInfo(items[index][zoomdItem]);
				}
			}
		}
	}

	void init()
	{
		index = 0;
		ItemButtonStruct item;
		item = new ItemButtonStruct(1,		"10 Coins",		"menu_shop_close",			"menu_shop_sprite_coin",	10,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(2,		"25 Coins",		"menu_shop_sprite_coin",	"menu_shop_byebutton",		25,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(5,		"75 Coins",		"menu_shop_byebutton",		"menu_shop_sprite_coin",	75,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(10,		"200 Coins",	"menu_shop_sprite_coin",	"menu_shop_close",			200,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(20,		"500 Coins",	"menu_shop_byebutton",		"menu_shop_sprite_coin",	500,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(30,		"1000 Coins",	"menu_shop_sprite_coin",	"menu_shop_sprite_coin",	1000,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(40,		"2000 Coins",	"menu_shop_close",			"menu_shop_sprite_coin",	2000,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(50,		"4000 Coins",	"menu_shop_sprite_coin",	"menu_shop_byebutton",		4000,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		
		index = 1;
		int equiped = PlayerPrefs.GetInt("Equiped");
		item = new ItemButtonStruct(100,	"PolarBear",	"menu_shop_close",			"menu_shop_sprite_coin",	0,		ItemButtonStruct.ItemButtonType.SKIN,		true, 													equiped == 0);
		items[index].Add(item);
		item = new ItemButtonStruct(120,	"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_byebutton",		0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem1") == 1 ? true : false,	equiped == 1);
		items[index].Add(item);
		item = new ItemButtonStruct(49,		"PolarBear",	"menu_shop_byebutton",		"menu_shop_sprite_coin",	0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem2") == 1 ? true : false,	equiped == 2);
		items[index].Add(item);
		item = new ItemButtonStruct(10,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_close",			0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem3") == 1 ? true : false,	equiped == 3);
		items[index].Add(item);
		item = new ItemButtonStruct(300,	"PolarBear",	"menu_shop_byebutton",		"menu_shop_sprite_coin",	0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem4") == 1 ? true : false,	equiped == 4);
		items[index].Add(item);
		item = new ItemButtonStruct(93,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_sprite_coin",	0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem5") == 1 ? true : false,	equiped == 5);
		items[index].Add(item);
		item = new ItemButtonStruct(23,		"PolarBear",	"menu_shop_close",			"menu_shop_sprite_coin",	0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem6") == 1 ? true : false,	equiped == 6);
		items[index].Add(item);
		item = new ItemButtonStruct(56,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_byebutton",		0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem7") == 1 ? true : false,	equiped == 7);
		items[index].Add(item);
		
		index = 2;
		item = new ItemButtonStruct(10,		"3 Lives",		"menu_shop_close",			"menu_shop_sprite_coin",	3,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(20,		"8 Lives",		"menu_shop_sprite_coin",	"menu_shop_byebutton",		8,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(30,		"15 Lives",		"menu_shop_byebutton",		"menu_shop_sprite_coin",	15,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(50,		"50 Lives",		"menu_shop_sprite_coin",	"menu_shop_close",			50,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);


		index = 1;
	}
}
