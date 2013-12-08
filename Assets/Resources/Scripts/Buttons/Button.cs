using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour {

	public Texture texture;	
	public Texture textureDown;	
	
	public enum Level {STARTMENU, LEVEL1, LEVEL2, SHOP};
	public Level level = Level.LEVEL1;
	
	private bool canBePressed = false;
	
	void Start()
	{	
		renderer.material.mainTexture = texture;
	}
	
	void Update()
	{
		if(canBePressed && !Input.GetMouseButton(0))
		{
			action();
			onNotPressedNoMore();
		}
	}
	
	public void onPress()
	{
		renderer.material.mainTexture = textureDown;
		canBePressed = true;
	}
	public void onNotPressedNoMore()
	{
		renderer.material.mainTexture = texture;
		canBePressed = false;
	}
	
	public virtual void action()
	{
		Application.LoadLevel((int)level);
	}
}
