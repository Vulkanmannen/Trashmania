using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour {

	public Texture texture;	
	public Texture pressedTexture;

	public enum Level {STARTMENU, LEVEL1, LEVEL2, SHOP};
	public Level level = Level.LEVEL1;
	
	protected bool canBePressed = false;
	
	void Start()
	{	
		if(renderer)
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
	
	public virtual void onPress()
	{
		if(renderer)
			renderer.material.mainTexture = pressedTexture;
		canBePressed = true;
	}
	public void onNotPressedNoMore()
	{
		if(renderer)
			renderer.material.mainTexture = texture;
		canBePressed = false;
	}
	
	public virtual void action()
	{
		Application.LoadLevel((int)level);
	}
}
