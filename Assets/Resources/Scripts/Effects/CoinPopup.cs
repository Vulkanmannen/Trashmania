using UnityEngine;
using System.Collections;

public class CoinPopup : PopUp 
{
	GetAchievement achievement;

	// myStart
	protected override void myStart()
	{
		GetComponent<Animation>().Play("PopupCoinInAnimation");
		achievement = GameObject.FindWithTag("GlobalGameObject").GetComponent<GetAchievement>();
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
		{
			achievement.moneyGone = true;
			Destroy(this.gameObject);
		}
	}
}
