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
		if(transform.position.x > -340 && transform.position.x < 340 || targetX > -340 && targetX < 340)
		{
			float diffX = targetX - transform.position.x;
			transform.position = new Vector3(transform.position.x + diffX/10f, 0f, transform.position.z);
		}
	}
}
