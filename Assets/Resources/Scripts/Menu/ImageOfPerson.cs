using UnityEngine;
using System.Collections;

public class ImageOfPerson : MonoBehaviour 
{
	public Texture[] textures;

	void Start () 
	{
		renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
	}
}
