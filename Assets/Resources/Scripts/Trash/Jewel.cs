using UnityEngine;
using System.Collections;

public class Jewel : Trash 
{
	protected override void start()
	{}

	public override void hitTrashCollider()
	{
		string jewel = "Jewel" + (thisLevel - 1).ToString();
		PlayerPrefs.SetInt(jewel, 1);

		destroyAndPoff("");
	}
}
