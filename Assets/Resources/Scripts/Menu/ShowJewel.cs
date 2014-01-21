using UnityEngine;
using System.Collections;

public class ShowJewel : MonoBehaviour 
{
	public Texture nothing;
	public Texture showJewel;

	void Start () 
	{
		int number = (int)transform.parent.parent.GetComponent<Button>().level - 1;
		string jewel = "Jewel" + number;

		if(PlayerPrefs.GetInt(jewel) == 1)
			renderer.material.mainTexture = showJewel;
		else
			renderer.material.mainTexture = nothing;
	}
}
