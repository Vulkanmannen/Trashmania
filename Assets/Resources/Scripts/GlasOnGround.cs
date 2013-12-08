using UnityEngine;
using System.Collections;

public class GlasOnGround : MonoBehaviour 
{
	float timeOnScreen = 4;
	int timeLost = 10;
	
	void Update ()
	{
		timeOnScreen -= Time.deltaTime;
		
		// blink
		if((timeOnScreen < 1f && timeOnScreen > 0.75f) || (timeOnScreen < 0.5f && timeOnScreen > 0.25f))
		{
			transform.Find("Sprite").GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			transform.Find("Sprite").GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(timeOnScreen <= 0f)
		{
			Destroy(this.gameObject);
		}
	}

	public void stepOnGlas()
	{
		GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>().points -= timeLost;

		string textToShow = "-" + timeLost.ToString();

		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/DestroyedTrashPopup") as GameObject, transform.position, Quaternion.Euler(new Vector3(90, 180, 0)));
		newObject.GetComponent<DestroyedTrashPopup>().setText(textToShow);
		newObject.GetComponent<DestroyedTrashPopup>().timeOnScreen = 0.6f;
		newObject.transform.parent = transform.parent.transform;

		Destroy(gameObject);
	}
}
