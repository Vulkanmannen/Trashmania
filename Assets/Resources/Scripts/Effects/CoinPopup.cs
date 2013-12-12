using UnityEngine;
using System.Collections;

public class CoinPopup : PopUp 
{
	
	// myStart
	protected override void myStart()
	{
		GetComponent<Animation>().Play("PopupCoinInAnimation");
	}
	
	// myUpdate
	protected override void myUpdate()
	{
		if(timeOnScreen < Time.timeSinceLevelLoad && !playdEndAnimation)
		{
			GetComponent<Animation>().Play("PopupCoinOutAnimation");
			playdEndAnimation = true;
		}
		else if(!GetComponent<Animation>().IsPlaying("PopupCoinOutAnimation") && playdEndAnimation)
			Destroy(this.gameObject);
	}
}
