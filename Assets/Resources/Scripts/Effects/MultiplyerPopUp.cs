using UnityEngine;
using System.Collections;

public class MultiplyerPopUp : MonoBehaviour {

	public string[] multiplyerTextures = {"sprite_combo_1x", "sprite_combo_2x", "sprite_combo_3x", "sprite_combo_4x", "sprite_combo_5x", "sprite_combo_6x"};

	private GlobalGameObject globalGameObject;
	private bool playdEndAnimation = false;
	private int multiplyer = 0;
	private Color color;
	void Start () 
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();

		myStart();
	}
	
	// myStart
	protected virtual void myStart()
	{
		color = renderer.material.color;
		GetComponent<Animation>().Play("PopupMultiInAnimation");
	}
	
	void Update () 
	{
		myUpdate();
	}
	
	// myUpdate
	protected virtual void myUpdate()
	{
		int multi = globalGameObject.comboMultiplyer;

		if(multi != multiplyer && !playdEndAnimation)
		{
			GetComponent<Animation>().Play("PopupMultiOutAnimation");
			playdEndAnimation = true;
		}
		else if(!GetComponent<Animation>().IsPlaying("PopupMultiOutAnimation") && playdEndAnimation)
		{
			Destroy(this.gameObject);
		}

		if(playdEndAnimation && multi == 0)
		{
			color *= new Color(1f, 0.9f, 0.9f, 1f);
			if(color.g < 0.5f)
				color.g = 0.5f;
			if(color.b < 0.5f)
				color.b = 0.5f;

			renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 1f));
		}
	}
	
	public void setMultiplyer(int multi)
	{
		string texture = multiplyerTextures[multi - 1];
		multiplyer = multi;
		renderer.material.mainTexture = Resources.Load("Textures/Interface/" + texture) as Texture;
	}
}
