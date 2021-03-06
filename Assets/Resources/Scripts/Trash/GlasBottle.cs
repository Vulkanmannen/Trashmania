﻿using UnityEngine;
using System.Collections;

public class GlasBottle : Trash
{
	// collision
	protected override void myCollision(Collision collision)
	{
		if(collision.collider.name != "wall")
		{
			dir = new Vector3(0,0,0);
			bounce = true;
		}
		if(collision.collider.gameObject.CompareTag("TrashCollider"))
		{
			hitTrashCollider();
		}
		if(collision.collider.name == "Ground")
		{
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/GlasOnGround") as GameObject, transform.position, Quaternion.Euler(0f, 0f, 0f));
			newObject.transform.parent = transform.parent.transform;
			globalGameObject.GetComponent<GlobalGameObject>().resetCombo();

			destroyAndPoff("", 1);
		}
	}
	public override void hitTrashCollider()
	{
		globalGameObject.GetComponent<GlobalGameObject>().trashInARow++;
		globalGameObject.GetComponent<GlobalGameObject>().numberOfCaughtTrash++;
		globalGameObject.GetComponent<GlobalGameObject>().numberOfGlasTrash++;
		int totalPoints = points * globalGameObject.GetComponent<GlobalGameObject>().comboMultiplyer;
		globalGameObject.GetComponent<GlobalGameObject>().points += totalPoints;
		destroyAndPoff(totalPoints.ToString());
	}
}
