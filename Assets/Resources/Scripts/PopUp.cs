using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

	public float timeOnScreen = 2;
	public bool playdEndAnimation = false;
	
	void Start () 
	{
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
	protected virtual void myUpdate()
	{
		if(timeOnScreen < Time.timeSinceLevelLoad && !playdEndAnimation)
		{
			GetComponent<Animation>().Play("PopupOutAnimation");
			playdEndAnimation = true;
		}
		else if(!GetComponent<Animation>().IsPlaying("PopupOutAnimation") && playdEndAnimation)
			Destroy(this.gameObject);
	}
	
	public void setTexture(string texture)
	{
		renderer.material.mainTexture = Resources.Load("Textures/Interface/" + texture) as Texture;
	}
	
	public void setText(string textToShow)
	{
		GetComponentInChildren<TextMesh>().text = textToShow;
	}
}
