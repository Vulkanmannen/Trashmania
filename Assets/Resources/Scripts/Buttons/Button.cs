using UnityEngine;
using System.Collections;


public class Button : MonoBehaviour {

	public Texture texture;	
	
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
		renderer.material.SetColor("_Color", new Color(0.3f,0.1f,0.3f,1f));
		canBePressed = true;
	}
	public void onNotPressedNoMore()
	{
		renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
		canBePressed = false;
	}
	
	public virtual void action()
	{
		Application.LoadLevel((int)level);
	}
}
