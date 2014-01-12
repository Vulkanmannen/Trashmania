using UnityEngine;
using System.Collections;

public class ShopTab : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeActiveTab(string name, int tab)
	{
		foreach(ShopTapButton button in GetComponentsInChildren<ShopTapButton>())
		{
			if(button.tabName == name)
			{
				button.activeButton = true;
				transform.parent.GetComponent<Shop>().changeIndex(tab);
			}
			else 
				button.activeButton = false;
		}
	}
}
