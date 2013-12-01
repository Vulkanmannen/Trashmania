using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour 
{
	public float alpha = 0.5f;
	public bool fadeOut = false;
	public Color color = Color.green;
	void Start () 
	{
	
	}

	void Update () 
	{
		if(alpha < 0.5f)
			fadeOut = false;
		if(alpha > 0.8f)
			fadeOut = true;


		if(fadeOut)
			alpha -= 0.015f;
		else
			alpha += 0.015f;
		renderer.material.SetColor("_Color",new Color(color.r, color.g, color.b, alpha));
	}
}
