using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour 
{
	public int number = 0;
	public Texture achieved;
	public Texture notAchieved;


	void Start () 
	{
		string achievements = "Achievement" + number.ToString();

		if(PlayerPrefs.GetInt(achievements) == 1)
			renderer.material.mainTexture = achieved;
		else
			renderer.material.mainTexture = notAchieved;
	}

}
