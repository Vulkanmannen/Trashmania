using UnityEngine;
using System.Collections;

public class ThreeStars : MonoBehaviour 
{
	public string thisLevel = "LevelStars1";

	void Start() 
	{
		int stars;

		stars = PlayerPrefs.GetInt(thisLevel);

		for(int i = 0; i < stars; ++i)
		{
			foreach(Transform star in GetComponentsInChildren<Transform>())
			{
				if(star.localPosition.x == -70 + i*70)
					star.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/Map/sprite_star_full") as Texture;
			}
		}
	}
}
