using UnityEngine;
using System.Collections;

public class ShowResult : MonoBehaviour
{
	GlobalGameObject globalGameObject;

	private bool scoreIsSet = false;

	void Start()
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
	}

	void Update()
	{
		if(globalGameObject.currentEvent == GlobalGameObject.GameEvent.GAMEOVER && !scoreIsSet)
		{
			scoreIsSet = true;
			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.name == "NumberOfCaughtTrash")
				{
					string text = "Trash" + globalGameObject.numberOfCaughtTrash.ToString();
					t.GetComponent<TextMesh>().text = text;
				}
			}
		}
	}
}
