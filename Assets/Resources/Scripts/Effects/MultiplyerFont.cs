using UnityEngine;
using System.Collections;

public class MultiplyerFont : MonoBehaviour 
{
	private GlobalGameObject globalGameObject;

	public Material[] fonts;

	void Start () 
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();

		if(globalGameObject.comboMultiplyer > 0)
		{
			GetComponent<TextMesh>().renderer.material = fonts[globalGameObject.comboMultiplyer - 1];
		}
	}
}
