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
		if(newIndex < 3 && newIndex >= 0)
		{
			index = newIndex;
			side = 0;
			zoomdItem = 0;
			setInfo();
		}
		else
			Debug.Log(newIndex);
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
		zoomAnimation.Play("ZoomWindowOutAnimation");
		setInfo();
		zoom = false;
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

		item = new ItemButtonStruct(2,		"5 Coins",		"shop_icon_coins5",			"menu_shop_coins_icon",	5,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(5,		"15 Coins",		"shop_icon_coins15",		"menu_shop_coins_icon",	15,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(10,		"45 Coins",		"shop_icon_coins50",		"menu_shop_coins_icon",	45,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);
		item = new ItemButtonStruct(15,		"85 Coins",		"shop_icon_coins50",		"menu_shop_coins_icon",	85,		ItemButtonStruct.ItemButtonType.COIN);
		items[index].Add(item);

		
		index = 1;
		int equiped = PlayerPrefs.GetInt("Equiped");
		item = new ItemButtonStruct(0,		"Larry",		"shop_icon_larry",			"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.SKIN,		true, 													equiped == 0);
		items[index].Add(item);
		item = new ItemButtonStruct(10,		"PolarBear",	"shop_icon_larrypolarbear",	"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.SKIN,		PlayerPrefs.GetInt("BoughtItem1") == 1 ? true : false,	equiped == 1);
		items[index].Add(item);
		item = new ItemButtonStruct(0,		"1 Powerup",	"shop_icon_powerup1",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem2") == 1 ? true : false,	equiped == 2);
		items[index].Add(item);
		item = new ItemButtonStruct(25,		"2 Powerup",	"shop_icon_powerup2",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem3") == 1 ? true : false,	equiped == 3);
		items[index].Add(item);
		item = new ItemButtonStruct(85,		"3 Powerup",	"shop_icon_powerup3",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem4") == 1 ? true : false,	equiped == 4);
		items[index].Add(item);
		item = new ItemButtonStruct(0,		"X1 Coins",		"shop_icon_coingainx1",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem5") == 1 ? true : false,	equiped == 5);
		items[index].Add(item);
		item = new ItemButtonStruct(25,		"X2 Coins",		"shop_icon_coingainx2",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem6") == 1 ? true : false,	equiped == 6);
		items[index].Add(item);
		item = new ItemButtonStruct(85,		"X3 Coins",		"shop_icon_coingainx3",		"menu_shop_coins_icon",	0,		ItemButtonStruct.ItemButtonType.POWERUP,	PlayerPrefs.GetInt("BoughtItem7") == 1 ? true : false,	equiped == 7);
		items[index].Add(item);
		
		index = 2;
		item = new ItemButtonStruct(5,		"1 Lives",		"shop_icon_life1",			"menu_shop_coins_icon",	1,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(20,		"5 Lives",		"shop_icon_life3",			"menu_shop_coins_icon",	5,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(35,		"10 Lives",		"shop_icon_life10",			"menu_shop_coins_icon",	10,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);
		item = new ItemButtonStruct(45,		"15 Lives",		"shop_icon_life10",			"menu_shop_coins_icon",	15,		ItemButtonStruct.ItemButtonType.LIVES);
		items[index].Add(item);


		index = 1;
	}
}
