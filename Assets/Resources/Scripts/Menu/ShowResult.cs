using UnityEngine;
using System.Collections;

public class ShowResult : MonoBehaviour
{
	GlobalGameObject globalGameObject;

	void Start()
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();

		foreach(Transform t in GetComponentsInChildren<Transform>())
		{
			if(t.name == "NumberOfGlasTrash")
			{
				string text = "Trash" + globalGameObject.numberOfNormalTrash.ToString();
				t.GetComponent<TextMesh>().text = text;
			}
			else if(t.name == "NumberOfNormalTrash")
			{
				string text = "Glas" + globalGameObject.numberOfGlasTrash.ToString();
				t.GetComponent<TextMesh>().text = text;
			}
		}
	}
}
