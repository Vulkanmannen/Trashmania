using UnityEngine;
using System.Collections;

public class ByeButton : Button 
{
	public override void action()
	{
		ItemButtonShop item = transform.parent.GetComponent<ItemButtonShop>();

		ItemButtonStruct.ItemButtonType type = item.type;
		int cost = item.cost;
		int value = item.value;

		int money = PlayerPrefs.GetInt("Coins"); 

		if(type == ItemButtonStruct.ItemButtonType.LIVES)
		{
			if(cost < money)
			{
				int lives = PlayerPrefs.GetInt("Lives");
				money -= cost;
				lives += value;
				PlayerPrefs.SetInt("Lives", lives);
				PlayerPrefs.SetInt("Coins", money);
			}
		}

		if(type == ItemButtonStruct.ItemButtonType.COIN)
		{
			money += value;
			PlayerPrefs.SetInt("Coins", money);
		}

		if(type == ItemButtonStruct.ItemButtonType.SKIN)
		{
			if(cost < money)
			{
				Shop shop = transform.parent.parent.GetComponent<Shop>();
				
				int index = shop.zoomdItem;
				ItemButtonStruct itemStruct = shop.items[1][index];
				itemStruct.bought = true;
				shop.items[1][index] = itemStruct;

				item.bought = true;
				
				string bought = "BoughtItem" + index;
				PlayerPrefs.SetInt(bought, 1);

				// setInfoInButton
				item.setInfo(shop.items[1][index]);

				// money
				money -= cost;
				PlayerPrefs.SetInt("Coins", money);
			}
		}
	}
}
