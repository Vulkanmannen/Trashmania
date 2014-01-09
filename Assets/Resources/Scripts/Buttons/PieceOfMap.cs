using UnityEngine;
using System.Collections;

public class PieceOfMap : MonoBehaviour
{
	public Texture[] textures;
	public int textureIndex = 2;
	public bool locked = true;

	private bool pressed = false;

	void Start () 
	{
		renderer.material.mainTexture = textures[textureIndex];
	}
	

	void Update () 
	{
		if(!locked)
		{
			textureIndex = 0;
			if(pressed)
				textureIndex = 1;

			renderer.material.mainTexture = textures[textureIndex];

			pressed = false;
		}
	}


	public void press()
	{
		pressed = true;
	}
}
