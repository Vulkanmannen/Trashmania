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

	public void changeActiveTab(string name)
	{
		foreach(ShopTapButton button in GetComponentsInChildren<ShopTapButton>())
		{
			if(button.tabName == name)
				button.activeButton = true;
			else 
				button.activeButton = false;
		}
	}
}
