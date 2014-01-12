using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ItemButtonStruct
{
	public ItemButtonStruct(int newCost, string newName, string imageTexture, string costTexture, int newValue, ItemButtonType newType)
	{
		cost = newCost;
		itemName = newName;
		image = imageTexture;
		costImage = costTexture;
		
		value = newValue;
		type = newType;
		value = 5;
		type = ItemButtonType.LIVES;
	}
	
	public int cost;
	public string itemName;
	public string image;
	public string costImage;
	
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
			zoomdItem = (zoomdItem + 1) % items[index].Count;
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
	}

	public void zoomOutItem()
	{
		zoom = false;
		zoomAnimation.Play("ZoomWindowOutAnimation");
		setInfo();
	}

	void setInfo()
	{
		foreach(Transform button in GetComponentsInChildren<Transform>())
		{
			if(button.GetComponent<ItemButtonShop>())
			{
				int number = button.GetComponent<ItemButtonShop>().number;
				if(!zoom)
				{
					button.GetComponent<ItemButtonShop>().setInfo(items[index][number + side * 4]);
				}
				else if(button.GetComponent<ItemButtonShop>().number == 4)
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
		item = new ItemButtonStruct(100,	"PolarBear",	"menu_shop_close",			"menu_shop_sprite_coin",	3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(120,	"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_byebutton",		3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(49,		"PolarBear",	"menu_shop_byebutton",		"menu_shop_sprite_coin",	3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(10,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_close",			3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(300,	"PolarBear",	"menu_shop_byebutton",		"menu_shop_sprite_coin",	3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(93,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_sprite_coin",	3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(23,		"PolarBear",	"menu_shop_close",			"menu_shop_sprite_coin",	3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(56,		"PolarBear",	"menu_shop_sprite_coin",	"menu_shop_byebutton",		3,	ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		
		index = 1;
		item = new ItemButtonStruct(13, "PolarBear", "menu_shop_close", "menu_shop_zoomwindow", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(145, "PolarBear", "menu_shop_zoomwindow", "menu_shop_byebutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(452, "PolarBear", "menu_shop_byebutton", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(90, "PolarBear", "menu_shop_zoomwindow", "menu_shop_close", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(34, "PolarBear", "menu_shop_byebutton", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(675, "PolarBear", "menu_shop_sprite_coin", "menu_shop_zoomwindow", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(523, "PolarBear", "menu_shop_zoomwindow", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(74, "PolarBear", "menu_shop_sprite_coin", "menu_shop_byebutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		
		index = 2;
		item = new ItemButtonStruct(2143, "PolarBear", "menu_shop_equipbutton", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(124, "PolarBear", "menu_shop_sprite_coin", "menu_shop_byebutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(4567, "PolarBear", "menu_shop_byebutton", "menu_shop_equipbutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(4132, "PolarBear", "menu_shop_sprite_coin", "menu_shop_close", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(25, "PolarBear", "menu_shop_equipbutton", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(5, "PolarBear", "menu_shop_equipbutton", "menu_shop_sprite_coin", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(14, "PolarBear", "menu_shop_close", "menu_shop_equipbutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(13, "PolarBear", "menu_shop_sprite_coin", "menu_shop_byebutton", 3, ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);

		index = 1;
	}
}
