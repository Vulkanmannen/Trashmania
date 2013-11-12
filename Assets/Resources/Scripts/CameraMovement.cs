using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	private GameObject player;
	private float targetX;
	
	void Start () 
	{
		player = GameObject.FindWithTag("myPlayer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetX = player.transform.position.x;
		if(targetX > -360 && targetX < 360)
		{
			float diffX = targetX - transform.position.x;
			transform.position = new Vector3(transform.position.x + diffX/10f, 0f, transform.position.z);
		}
	}
}
