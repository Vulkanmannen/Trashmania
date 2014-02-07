using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour 
{
	public Texture[] texture;

	void Start () 
	{
		renderer.material.mainTexture = texture[Random.Range(0, texture.Length - 1)];
	}

}
