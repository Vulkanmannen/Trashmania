using UnityEngine;
using System.Collections;

public class PowerupButton : MonoBehaviour 
{
	private RaycastHit raycastHit;	
	public Player.Mode mode = Player.Mode.NORMAL;
	private Player player;

	public int number = 0;

	void Start() 
	{
		player = GameObject.FindWithTag("myPlayer").GetComponent<Player>();
		setMode(Player.Mode.ICECREAM);
	}

	void Update() 
	{
		if(Input.GetMouseButton(0))
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Vector3 fwd = Vector3.forward;
			if(Physics.Raycast(origin, fwd, out raycastHit))
			{
				if(raycastHit.transform.gameObject.GetType() == gameObject.GetType())
				{

				}

				if(raycastHit.transform.GetComponent<PopUpPowerup>())
				{

					if(!raycastHit.transform.GetComponent<PopUpPowerup>().closePopUp)
						raycastHit.transform.GetComponent<PopUpPowerup>().close();

					if(mode != Player.Mode.NORMAL)
						pressedPowerup();
				}
			}
		}
	}

	public void setMode(Player.Mode newMode)
	{
		mode = newMode;
		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUpPowerup"), transform.position + new Vector3(0f, 0f, -20f), transform.rotation);
		newObject.transform.parent = transform;
	}

	private void pressedPowerup()
	{
		if(mode == Player.Mode.TRUCK)
			player.setMode(mode);
		else if(mode == Player.Mode.ICECREAM)
			player.setMode(mode, 2f);
		else
			player.setMode(mode);

		mode = Player.Mode.NORMAL;
	}
}
