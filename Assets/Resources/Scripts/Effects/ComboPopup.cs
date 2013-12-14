using UnityEngine;
using System.Collections;

public class ComboPopup : MonoBehaviour 
{
	// textures
	public string[] popupComboTextures = {"sprite_combotext_2x", "sprite_combotext_3x", "sprite_combotext_4x", "sprite_combotext_5x", "sprite_combotext_6x"};

	public float timeOnScreen = 2;
	public int textureIndex = 0;
	private GameObject myCamera;
	private int multiplyer = 0;
	protected bool playdEndAnimation = false;
	
	void Start () 
	{
		myCamera = GameObject.FindWithTag("MainCamera");

		timeOnScreen = timeOnScreen + Time.timeSinceLevelLoad;
		
		myStart();
	}
	
	// myStart
	protected virtual void myStart()
	{
		GetComponent<Animation>().Play("PopupInAnimation");
	}
	
	void Update () 
	{
		myUpdate();
	}

	// myUpdate
	private void myUpdate()
	{
		if(timeOnScreen < Time.timeSinceLevelLoad && !playdEndAnimation)
		{
			GetComponent<Animation>().Play("PopupComboOutAnimation");
			playdEndAnimation = true;
		}
		else if(!GetComponent<Animation>().IsPlaying("PopupComboOutAnimation") && playdEndAnimation)
		{
			Destroy(this.gameObject);
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/MultiplyerPopup") as GameObject, myCamera.transform.position + new Vector3(-280, -400, 100), Quaternion.Euler(new Vector3(90, 180, 0)));			
			newObject.GetComponent<MultiplyerPopUp>().setMultiplyer(multiplyer);
			newObject.transform.parent = myCamera.transform;
		}
	}

	public void setTexture(int index)
	{
		string texture = popupComboTextures[index];
		multiplyer = index + 1;
		renderer.material.mainTexture = Resources.Load("Textures/Interface/" + texture) as Texture;
	}

}
