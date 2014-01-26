using UnityEngine;
using System.Collections;

public class PowerupButtonWraper : MonoBehaviour 
{
	
	void Start () 
	{
		int slots = PlayerPrefs.GetInt("PowerUpSlots");
		for(int i = 0; i < slots; ++i)
		{
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PowerupButton") as GameObject, transform.position + new Vector3(285f, -180f + i * 200f, 400f), Quaternion.Euler(new Vector3(90, 180, 0)));
			newObject.GetComponent<PowerupButton>().number = i;
			newObject.transform.parent = transform;
		}
	}

	void Update () 
	{
	
	}

	public void setMode(Player.Mode mode)
	{
		int i = 0;
		bool set = false;
		PowerupButton number0 = null;

		foreach(PowerupButton button in GetComponentsInChildren<PowerupButton>())
		{
			if(button.number == i)
			{
				if(i == 0)
					number0 = button;

				if(button.mode == Player.Mode.NORMAL && !set)
				{
					button.setMode(mode);
					set = true;
				}
			}
			++i;
		}

		if(!set && number0 != null)
		{
			number0.setMode(mode);
		}
	}
}
