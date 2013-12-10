using UnityEngine;
using System.Collections;

public class ScoreIndicator : MonoBehaviour 
{
	public int numberOfCaughtTrash = 0;
	public int numberOfTrashToWin = 100;

	private bool shownPopUp = false;

	GlobalGameObject globalGameObject;

	void Start () 
	{
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
		numberOfTrashToWin = globalGameObject.numberOfTrashToWin;
	}
	

	void Update () 
	{
		if(numberOfCaughtTrash < numberOfTrashToWin)
			numberOfCaughtTrash = globalGameObject.numberOfCaughtTrash;

		Vector3 scale = transform.localScale;	
		Vector3 pos = transform.localPosition;

		float increase = 0.74f / numberOfTrashToWin * numberOfCaughtTrash;

		scale.z = increase;
		pos.z = -(increase / 2);

		transform.localScale = scale;
		transform.localPosition = pos;
		Vector2 size = new Vector2(1f, (float)(numberOfCaughtTrash) / (float)(numberOfTrashToWin));

		renderer.material.mainTextureScale = size;

		if(numberOfTrashToWin == numberOfCaughtTrash && !shownPopUp)
		{
			createPopUp();
			shownPopUp = true;
		}
	}

	void createPopUp()
	{
		GameObject myCamera = GameObject.FindWithTag("MainCamera");

		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUp") as GameObject, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));
		newObject.transform.parent = myCamera.transform;
		newObject.GetComponent<PopUp>().setTexture("sprite_button_powerup");
	}
}
