using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPath : MonoBehaviour {
	
	public  List<Vector3> nodes;
	
	void Awake () 
	{
		foreach(Transform node in gameObject.GetComponentsInChildren<Transform>())
		{
			if(node.gameObject != gameObject)
				nodes.Add(node.position);
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
