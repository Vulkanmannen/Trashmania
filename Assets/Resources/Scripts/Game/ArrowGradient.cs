using UnityEngine;
using System.Collections;

public class ArrowGradient : MonoBehaviour 
{
	
	public float power = 0f;

	float increase = 0f;

	void Update()
	{
		Vector3 scale = transform.localScale;	
		Vector3 pos = transform.localPosition;

		float increaseTo = 0.30f / 600f * power;


		if(increaseTo > 0.30f)
			increaseTo = 0.30f;
		else if(increaseTo < -0.30f)
			increaseTo = -0.30f;

		if(increase < 0)
		{
			if(increase > increaseTo)
				increase -= 0.01f;
			else if(increase < increaseTo)
				increase += 0.01f;
		}
		else
		{
			if(increase < increaseTo)
				increase += 0.01f;
			else if(increase > increaseTo)
				increase -= 0.01f;
		}

		if(increase > increaseTo - 0.015 && increase < increaseTo + 0.015)
			increase = increaseTo;

		float currentPower = increase / 0.3f;

		scale.x = increase;
		pos.x = -(increase / 2f);

		transform.localScale = scale;
		transform.localPosition = pos;
		Vector2 size = new Vector2((increase < 0f ? Mathf.Max(currentPower, -1f) : Mathf.Min(currentPower, 1f)), 1f);

		if(currentPower < 0f)
			size.x *= -1;
		renderer.material.mainTextureScale = size;
	}
}
