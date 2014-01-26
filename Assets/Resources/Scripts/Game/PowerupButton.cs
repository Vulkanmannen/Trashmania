using UnityEngine;
using System.Collections;

public class PowerupButton : MonoBehaviour 
{
	private RaycastHit raycastHit;	
	public Player.Mode mode = Player.Mode.NORMAL;
	private Player player;

	public int number = 0;

	Texture texture;
	Texture emptyTexture;
	Texture pressedTexture;

	void Start() 
	{
		player = GameObject.FindWithTag("myPlayer").GetComponent<Player>();

		texture = Resources.Load("Textures/Interface/sprite_button_powerup_full") as Texture;
		emptyTexture = Resources.Load("Textures/Interface/sprite_button_powerup_empty") as Texture;
		pressedTexture = Resources.Load("Textures/Interface/sprite_button_powerup_pressed") as Texture;

		renderer.material.mainTexture = emptyTexture;
	}

	void Update() 
	{
		if(Input.GetMouseButton(0) && player.currentEvent != GlobalGameObject.GameEvent.GAMEOVER)
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Vector3 fwd = Vector3.forward;
			if(Physics.Raycast(origin, fwd, out raycastHit))
			{
				if(raycastHit.transform.gameObject.GetType() == gameObject.GetType())
				{

				}

				if(raycastHit.transform.GetComponent<PopUpPowerup>() && raycastHit.transform.parent.GetComponent<PowerupButton>().number == number)
				{
					if(!raycastHit.transform.GetComponent<PopUpPowerup>().closePopUp)
						raycastHit.transform.GetComponent<PopUpPowerup>().close();

					if(mode != Player.Mode.NORMAL)
						pressedPowerup();
				}

				else if(mode == Player.Mode.NORMAL)
				{
					renderer.material.mainTexture = emptyTexture;
				}

			}
		}
		else if(mode == Player.Mode.NORMAL)
		{
			renderer.material.mainTexture = emptyTexture;
		}
	}

	public void setMode(Player.Mode newMode)
	{
		if(mode != Player.Mode.NORMAL)
			GetComponentInChildren<PopUpPowerup>().close();

		mode = newMode;
		GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/PopUpPowerup"), transform.position + new Vector3(0f, 0f, -20f), transform.rotation);
		newObject.transform.parent = transform;
		newObject.GetComponent<PopUpPowerup>().setTexture((int)mode - 1);

		renderer.material.mainTexture = texture;
	}

	private void pressedPowerup()
	{
		if(mode == Player.Mode.TRUCK)
			player.setMode(mode);
		else if(mode == Player.Mode.ICECREAM)
			player.setAdditionalMode(mode, 2f);
		else
			player.setAdditionalMode(mode);

		mode = Player.Mode.NORMAL;
		renderer.material.mainTexture = pressedTexture;
	}
}
