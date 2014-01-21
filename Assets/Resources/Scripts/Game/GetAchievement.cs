using UnityEngine;
using System.Collections;

public class GetAchievement : MonoBehaviour 
{
	GameObject myCamera;
	public int numberOfIcecream = 0;
	public int heartBonus = 0;
	public int sameKindInARow = 0;

	public int starsWhitIcecream = 0;
	private bool doneWhitsStars = false;
	public bool moneyGone = false;

	public int frogTap = 0;

	void Start()
	{
		myCamera = GameObject.FindWithTag("MainCamera");
	}

	void Update ()
	{
		if(numberOfIcecream > 4 && PlayerPrefs.GetInt("Achievement0") == 0)
		{
			popUp("Start active");
			PlayerPrefs.SetInt("Achievement0", 1);
		}

		if(heartBonus > 4 && PlayerPrefs.GetInt("Achievement1") == 0)
		{
			popUp("Start active");
			PlayerPrefs.SetInt("Achievement1", 1);
		}

		if(sameKindInARow > 4 && PlayerPrefs.GetInt("Achievement2") == 0)
		{
			popUp("Start active");
			PlayerPrefs.SetInt("Achievement2", 1);
		}

		if(starsWhitIcecream > 4 && PlayerPrefs.GetInt("Achievement3") == 0)
		{
			doneWhitsStars = true;
			PlayerPrefs.SetInt("Achievement3", 1);
		}

		if(doneWhitsStars && moneyGone)
		{
			doneWhitsStars = false;
			popUp("Start active");
		}
		moneyGone = false;

		if(frogTap > 19 && PlayerPrefs.GetInt("Achievement4") == 0)
		{
			popUp("Start active");
			PlayerPrefs.SetInt("Achievement4", 1);
		}
	}

	void popUp(string texture)
	{
		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUp") as GameObject, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));			
		newObject.transform.parent = myCamera.transform;
		newObject.GetComponent<PopUp>().setTexture(texture);
	}

}
