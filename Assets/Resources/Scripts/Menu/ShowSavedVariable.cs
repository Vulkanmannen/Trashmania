using UnityEngine;
using System.Collections;

public class ShowSavedVariable : MonoBehaviour
{
	public string savedVariable = "Coins";
	TextMesh text;

	void Start () 
	{
		text = GetComponent<TextMesh>();
	}

	void Update () 
	{
		text.text = PlayerPrefs.GetInt(savedVariable).ToString();
	}
}
