using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour 
{
	protected bool playdEndAnimation = false;
	
	void Start () 
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
		if(!GetComponent<Animation>().IsPlaying("PopupOutAnimation") && playdEndAnimation)
			Destroy(this.gameObject);
	}
	
	public void close()
	{
		GetComponent<Animation>().Play("PopupOutAnimation");
		playdEndAnimation = true;
	}
	
	public void setTexture(string name)
	{
		string loadTexture = "Textures/" + name;
		renderer.material.mainTexture = Resources.Load(loadTexture) as Texture;
	}
}
