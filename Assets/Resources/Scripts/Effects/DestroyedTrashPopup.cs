using UnityEngine;
using System.Collections;

public class DestroyedTrashPopup : MonoBehaviour {

	public float timeOnScreen = 2;
	
	void Start () 
	{
		timeOnScreen = timeOnScreen + Time.timeSinceLevelLoad;
	}
	
	void Update () 
	{
		if(timeOnScreen < Time.timeSinceLevelLoad)
			Destroy(this.gameObject);
	}
	
	public void setText(string textToShow)
	{
		GetComponentInChildren<TextMesh>().text = textToShow;
	}
}
