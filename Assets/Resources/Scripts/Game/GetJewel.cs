using UnityEngine;
using System.Collections;

public class GetJewel : MonoBehaviour
{
	GlobalGameObject globalGameObject;
	int thisLevel = 1;
	bool sent = false;

	public int numberOfTrucks = 0;

	void Start()
	{
		globalGameObject = GetComponent<GlobalGameObject>();
		thisLevel = globalGameObject.thisLevel;
	}

	void Update () 
	{
		if(thisLevel == 1)
		{
			if(globalGameObject.numberOfCaughtTrash > 350 && PlayerPrefs.GetInt("Jewel0") == 0 && !sent)
			{
				jewel(-50);
			}
		}

		if(thisLevel == 2)
		{
			if(numberOfTrucks > 4 && PlayerPrefs.GetInt("Jewel1") == 0 && !sent)
			{
				jewel(70);
			}
		}
	}

	void jewel(int x)
	{
		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/Trash/Jewel") as GameObject, new Vector3(100, 800, x), transform.rotation);
		newObject.transform.parent = transform;
		sent = true;
	}
}
