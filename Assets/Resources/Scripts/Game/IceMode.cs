using UnityEngine;
using System.Collections;

public class IceMode : MonoBehaviour 
{
	Player player;
	GlobalGameObject globalGameObject;
	
	bool allIceCleared = true;

	void Start ()
	{
		player = GameObject.FindWithTag("myPlayer").GetComponent<Player>();
		globalGameObject = GetComponent<GlobalGameObject>();
	}

	void Update () 
	{
		if(player.additionalMode == Player.Mode.ICECREAM)
		{
			allIceCleared = false;

			foreach(Trash trash in GetComponentsInChildren<Trash>())
			{
				if(!trash.turndToIce)
				{
					GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/IceCube") as GameObject);
					newObject.transform.parent = trash.transform;
					newObject.transform.localPosition = new Vector3(0f, 0f, -2f);
					newObject.transform.localScale = new Vector3(2f, 1f, 2f);
					trash.turndToIce = true;
				}
			}


		}
		else if(player.additionalMode != Player.Mode.ICECREAM && !allIceCleared)
		{
			bool clearedIce = false;
			foreach(Trash trash in GetComponentsInChildren<Trash>())
			{
				if(trash.turndToIce)
				{
					Destroy(trash.transform.Find("IceCube(Clone)").gameObject);
					clearedIce = true;
					trash.turndToIce = false;
				}
			}
			if(!clearedIce)
				allIceCleared = true;
		}
	}
}
