using UnityEngine;
using System.Collections;

public class TrashCanCollider : MonoBehaviour
{
	public GameObject player;
	public GameObject globalGameObject;
	public Vector3 offset = new Vector3(0f, 100f, 0f);
	public bool isLeft;
	
	private float offsetX;
	
	void Start ()
	{
		player = GameObject.FindWithTag("myPlayer");
		globalGameObject = GameObject.FindWithTag("GlobalGameObject");
		offsetX = offset.x;
	}
	
	void Update ()
	{
		isLeft = player.GetComponent<Player>().isLeft;
		offset = new Vector3(offsetX * (isLeft ? -1 : 1), offset.y, offset.z);
		
		transform.position = player.transform.position + offset;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.GetComponent<Trash>())
		{	
			// increase points
			globalGameObject.GetComponent<GlobalGameObject>().points += collider.GetComponent<Trash>().pointsFromGround;
			
			// decrease trashOnGround
			globalGameObject.GetComponent<GlobalGameObject>().trashOnGround--;
			
			player.GetComponent<Player>().setAnimationToPickUp();
			collider.gameObject.GetComponent<Trash>().destroy();
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.GetComponent<Trash>())
		{
			// if cambonatiable
			//if(collision.collider.name == "Trash")
		}
	}
}
