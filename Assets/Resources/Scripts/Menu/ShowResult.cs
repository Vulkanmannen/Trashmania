using UnityEngine;
using System.Collections;

public class ShowResult : MonoBehaviour
{
	GlobalGameObject globalGameObject;

	private bool showScore = false;
	private int score = 0;
	private int count = 0;
	private float clock = 0;

	void Start()
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
	}

	void Update()
	{
		if(showScore && count < score)
		{
			if(clock < Time.timeSinceLevelLoad)
			{
				clock = Time.timeSinceLevelLoad + 0.01f;
				count++;


				foreach(Transform t in GetComponentsInChildren<Transform>())
				{
					if(t.name == "NumberOfCaughtTrash")
					{
						t.GetComponent<TextMesh>().text = count.ToString();
					}
					else if(t.name == "CoinsEarned")
					{
						t.GetComponent<TextMesh>().text = (count / 50).ToString();
					}
					if(t.name == "ThreeStars")
					{
						if(count == globalGameObject.numberOfTrashToWin || count == globalGameObject.numberOfTrashToGetTwoStars || count == globalGameObject.numberOfTrashToGetThreeStars)
						{
							int stars = 0;
							
							if(count == globalGameObject.numberOfTrashToWin)
								stars = 1;
							
							else if(count == globalGameObject.numberOfTrashToGetTwoStars)
								stars = 2;
							
							else if(count == globalGameObject.numberOfTrashToGetThreeStars)
								stars = 3;

							for(int i = 0; i < stars; ++i)
							{
								foreach(Transform star in t.GetComponentsInChildren<Transform>())
								{
									if(star.localPosition.x == -70 + i*70 && star.name == "Star")
										star.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/Map/sprite_star_full") as Texture;
								}
							}
						}
					}
				}
			}
		}
	}

	public void setShowScore()
	{
		score = globalGameObject.numberOfCaughtTrash;
		showScore = true;
		clock = Time.timeSinceLevelLoad + 0.2f;


		foreach(Button b in GetComponentsInChildren<Button>())
		{
			b.active = false;
		}

		animation.Play("PauseMenuInAnimation");
	}
}
