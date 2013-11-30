using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 1f;
	public float speedMultiplier = 1.5f;
	//public float slowAcc = 30f;
	//public float acc = 500f;
	
	public float startMoving = 1;
	//public float superAcc = 500f;

	//public float slowSpeed = 0.33f;

	//public float stillThreshold = 0.03f;
	//public float slowThreshold = 0.11f;
	//public float superThreshold = 0.3f;
	
	//public float maxSpeed = 300f;
	
	public float points;
	
	//public float superSpeed = 3.2f;
	
	enum AnimationMode {WALK, TOIDLE, IDLE, PICKUP, TURNING, WALKIDLE, TRUCK, TOTRUCK};
	AnimationMode animationMode = AnimationMode.IDLE;
	
	public enum Mode {NORMAL, TRUCK};
	public Mode mode = Mode.NORMAL;
	
	public enum AdditionalMode {NORMAL, SPEED, ICECREAM};
	public AdditionalMode additionalMode = AdditionalMode.NORMAL;
	
	public int numberOfTruckPowerups = 3;
	public int numberOfSpeedPowerups = 2;
	
	public GlobalGameObject globalGameObject;
	public GlobalGameObject.GameEvent currentEvent;
	
	//public bool jumpPressed = false;
	//public float jumpSpeed = 250;
	
	private Vector3 origin;
	private RaycastHit raycastHit;
	private float idleTimer = 0;
	
	public bool isLeft = false;
	public bool wasLeft = false;
	private float turningTimer = 0;
	private float modeTimer =  0;	
	private float additionalModeTimer =  0;
	private float fadeToBlue = 0;

	//private float[] fingerPosY = {0f, 0f, 0f};
	//private float[] fingerPosX = {0f, 0f, 0f};
	
	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		globalGameObject = GameObject.FindWithTag("GlobalGameObject").GetComponent<GlobalGameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//-------------------------------------------MOVE-------------------------------------------------
		float velocity = 0f;	

		// sideways
		if(Input.GetMouseButton(0))
		{
			origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			// move x
			Vector3 fwd = Vector3.forward;
			if(Physics.Raycast(origin, fwd, out raycastHit))
			{
				if(raycastHit.transform.name == "ArrowPad")
					velocity = (raycastHit.point.x - GameObject.FindWithTag("MainCamera").transform.position.x) * speed * (additionalMode == AdditionalMode.SPEED ? speedMultiplier : 1);
			}
		}

		// jump
		//if(Input.GetMouseButton(0))
		//{
		//	origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	
		//	// move x
		//	Vector3 fwd = Vector3.forward;
		//	if(Physics.Raycast(origin, fwd, out raycastHit))
		//	{
		//		//Debug.Log (raycastHit.point.y);
		//		if(	fingerPosY[0] == 0 
		//		    || (fingerPosY[0] != 0 && raycastHit.point.y - fingerPosY[0] < 10f) 
		//		  	|| (fingerPosY[0] != 0 && fingerPosY[1] != 0 && raycastHit.point.y - fingerPosY[1] < 10f)
		//		  	|| (fingerPosY[0] != 0 && fingerPosY[1] != 0 &&  fingerPosY[2] != 0 && raycastHit.point.y - fingerPosY[2] < 10f))
		//		{
		//			fingerPosY[0] = raycastHit.point.y;
		//			fingerPosY[1] = 0;
		//			fingerPosY[2] = 0;
		//			fingerPosX[0] = raycastHit.point.x;
		//		}
		//		else if(fingerPosY[1] == 0)
		//			fingerPosY[1] = raycastHit.point.y;
		//		else if(fingerPosY[2] == 0)
		//		{
		//			fingerPosY[2] = raycastHit.point.y;
		//			fingerPosX[2] = raycastHit.point.x;
		//		}
		//		else
		//			jumpPressed = true;
		//
		//	}
		//	if(jumpPressed && mode == Mode.NORMAL && animationMode != AnimationMode.TOTRUCK)
		//	{
		//		mode = Mode.JUMP;
		//		velocity = (fingerPosX[2] - fingerPosX[0]) * speed * (additionalMode == AdditionalMode.SPEED ? speedMultiplier : 1);
		//	}
		//}
		//else
		//{
		//	fingerPosY[0] = 0;
		//	fingerPosY[1] = 0;
		//	fingerPosY[2] = 0;
		//}


			// set velocity 
		if(	animationMode != AnimationMode.PICKUP 
			&& animationMode != AnimationMode.TOIDLE
			&&(!(transform.position.x > 600) || (velocity < 0))
			&&(!(transform.position.x < -600) || (velocity > 0)))
			rigidbody.velocity = new Vector3(velocity, 0, 0);
		else
			rigidbody.velocity = new Vector3(0, 0, 0);
		
		if(points < 0)
			points = 0;

		//-------------------------------------------------------------------------------------------------
		
		//---------------------------------------------Modes-----------------------------------------------
			
		// set back mode after a time
		if(modeTimer < Time.timeSinceLevelLoad && mode != Mode.NORMAL)
			setToNormalMode();
		
		// set back additionalmode after a time
		if(additionalModeTimer < Time.timeSinceLevelLoad && additionalMode != AdditionalMode.NORMAL)
			setMode(AdditionalMode.NORMAL);
		
		//-------------------------------------------------------------------------------------------------
		
		//---------------------------------------------Events-----------------------------------------------
	
		currentEvent = globalGameObject.currentEvent;
	
	
		//-------------------------------------------------------------------------------------------------
		
		//---------------------------------------------Animation-------------------------------------------
		
		
		//---------------------------------------------NORMAL----------------------------------------------
		if(mode == Mode.NORMAL)
		{
		
			bool endOfAnimation = GetComponentInChildren<AnimationScript>().endOfAnimation;
			
			// restart turningTimer. Will start turning after 1 second
			if((isLeft && velocity < 0) || (!isLeft && velocity > 0))
				turningTimer = Time.timeSinceLevelLoad + 1f;
			
			// can't turn if not idle or walk
			if((animationMode == AnimationMode.WALK && turningTimer <= Time.timeSinceLevelLoad) || animationMode == AnimationMode.IDLE)
			{
				if(velocity < 0)
					isLeft = true;
				else if(velocity > 0)
					isLeft = false;
			}
			
			// Play walk animation backwards if not turning yet
			if(animationMode == AnimationMode.WALK)
			{
				if(isLeft == (velocity > 0))
					GetComponentInChildren<AnimationScript>().backwards = true;
				else if(isLeft == (velocity < 0))
					GetComponentInChildren<AnimationScript>().backwards = false;
			}
					
			// set is left in animation
			GetComponentInChildren<AnimationScript>().isLeft = isLeft;
			
			// set fps in animation
			if(animationMode == AnimationMode.WALK)
				GetComponentInChildren<AnimationScript>().fps = (int)Mathf.Min(Mathf.Abs(velocity)/8f, 50f);
			
			// restart idleTimer. Will go into IDLE after 2 seconds of standing still
			if(velocity > 0.01f || velocity < -0.01f)
				idleTimer = Time.timeSinceLevelLoad + 2f;
			
			// set animationMode
			
			// set TURNING
			if(animationMode != AnimationMode.TURNING 
				&& isLeft != wasLeft
				&& turningTimer <= Time.timeSinceLevelLoad
				&& (animationMode != AnimationMode.PICKUP || endOfAnimation) 
				&& animationMode != AnimationMode.TOIDLE
				&& animationMode != AnimationMode.TOTRUCK)
			{
				GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.TURNING, 15, false, 60);
				animationMode = AnimationMode.TURNING;
			}
			// set IDLE
			else if(animationMode == AnimationMode.TOIDLE && endOfAnimation)
			{
				GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.IDLE, 16, true);
				animationMode = AnimationMode.IDLE;
			}
			// set WALKIDLE
			else if(velocity < 0.01f && velocity > -0.01f
				&& animationMode == AnimationMode.WALK)
			{
					GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.WALKIDLE, 12, true);
					animationMode = AnimationMode.WALKIDLE;
			}
			// set TOIDLE
			else if(animationMode == AnimationMode.WALKIDLE
				&& idleTimer <= Time.timeSinceLevelLoad)
			{	
					GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.TOIDLE, 13, false);
					animationMode = AnimationMode.TOIDLE;
			}
			// set WALK
			else if(animationMode != AnimationMode.WALK 
				&& (animationMode != AnimationMode.PICKUP || endOfAnimation) 
				&& (animationMode != AnimationMode.TURNING || endOfAnimation) 
				&& (animationMode != AnimationMode.TOTRUCK || endOfAnimation) 
				&& animationMode != AnimationMode.TOIDLE 
				&& (animationMode != AnimationMode.IDLE || (velocity > startMoving || velocity < -startMoving))
				&& (animationMode != AnimationMode.WALKIDLE || (velocity > startMoving || velocity < -startMoving)))
			{
				GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.WALK, 12, true);
				animationMode = AnimationMode.WALK;
				transform.localScale = new Vector3(15, 18, 20);
			}	
				
			// set wasLeft
			wasLeft = isLeft;
			
			
		}
		
		//---------------------------------------------TRUCK-----------------------------------------------
		//-------------------------------------------------------------------------------------------------
		
		if(mode == Mode.TRUCK)
		{	
			// set animation
			if(animationMode == AnimationMode.TOTRUCK && GetComponentInChildren<AnimationScript>().endOfAnimation)
			{
				GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.TRUCK, 8, true, 20, false, 8);
				animationMode = AnimationMode.TRUCK;
			}
			
			// set fps
			if(animationMode == AnimationMode.TRUCK)
				GetComponentInChildren<AnimationScript>().fps = (int)Mathf.Min(Mathf.Abs(velocity)/12f, 30f);
			
			// play backwards if velosity is negative
			if(velocity < 0)
				GetComponentInChildren<AnimationScript>().backwards = true;
			else 
				GetComponentInChildren<AnimationScript>().backwards = false;

		}
		//---------------------------------------------JUMP------------------------------------------------
		//-------------------------------------------------------------------------------------------------
		
		//if(mode == Mode.JUMP || transform.position.y > -540)
		//{
		//	rigidbody.velocity += new Vector3(0f, jumpSpeed, 0f);
		//	
		//	if(jumpSpeed < 70)
		//		jumpSpeed -= 5;
		//	if(jumpSpeed < 0)
		//		jumpSpeed -= 15;
		//	else
		//		jumpSpeed -= 25;
		//	
		//	if(jumpSpeed < 0)
		//		jumpPressed = false;
		//	
		//	if(transform.position.y < -540 && !jumpPressed)
		//	{
		//		transform.position = new Vector3(transform.position.x, -540f, transform.position.z);
		//		jumpSpeed = 650;
		//		
		//		if(mode == Mode.JUMP)
		//			mode = Mode.NORMAL;
		//	}
		//}		
		if(additionalMode == AdditionalMode.ICECREAM)
		{
			if(Input.GetMouseButton(0))
			{
				origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				// destroy trash
				Vector3 fwd = Vector3.forward;
				if(Physics.Raycast(origin, fwd, out raycastHit))
				{
					if(raycastHit.transform.GetComponent<Trash>())
					{
						Trash trash = raycastHit.transform.GetComponent<Trash>();
						int p = trash.points * globalGameObject.comboMultiplyer;
						globalGameObject.points += p;
						trash.destroyAndPoff(p.ToString());

						if(trash.GetComponent<TrashStartEvent>())
						{
							GameObject.FindWithTag("Sister").GetComponent<Sister>().setSadFaceAnimation();
						}
					}
				}
			}
		}
	}

	
	
	//---------------------------------------------GUI-------------------------------------------------
	//-------------------------------------------------------------------------------------------------
	void OnGUI()
	{
		if(globalGameObject.currentEvent != GlobalGameObject.GameEvent.GAMEOVER)
		{
			if(numberOfTruckPowerups > 0 && mode != Mode.TRUCK)
			{
				Rect rect = new Rect(Screen.width - Screen.width / 6f, Screen.height / 2f, Screen.width / 6f, Screen.width / 6f);
				GUI.DrawTextureWithTexCoords(rect, Resources.Load("Textures/Interface/Icons/Iconsspritesheet") as Texture, new Rect(0.25f, -0.25f, 0.25f, 0.25f), true);
				
				Event e = Event.current;
				
				if(e.type == EventType.MouseUp)
				{
					if(rect.Contains(e.mousePosition))
					{
						numberOfTruckPowerups--;	
						setMode(Mode.TRUCK);
					}
				}
			}
			
			if(numberOfSpeedPowerups > 0 && additionalMode != AdditionalMode.SPEED)
			{
				Rect rect = new Rect(Screen.width - Screen.width / 6f, Screen.height / 2f - Screen.width / 6f - 4f, Screen.width / 6f, Screen.width / 6f);
				GUI.DrawTextureWithTexCoords(rect, Resources.Load("Textures/Interface/Icons/Iconsspritesheet") as Texture, new Rect(0f, -0.25f, 0.25f, 0.25f), true);
				
				Event e = Event.current;
				
				if(e.type == EventType.MouseUp)
				{
					if(rect.Contains(e.mousePosition))
					{
						numberOfSpeedPowerups--;
						setMode(AdditionalMode.SPEED);
						globalGameObject.GetComponent<GlobalGameObject>().startEvent(GlobalGameObject.GameEvent.RAIN);
					}
				}
			}
		}
		if(additionalMode == AdditionalMode.ICECREAM)
		{
			GUI.color = new Color(1f, 1f, 1f, fadeToBlue);
			GUI.DrawTexture(new Rect(0f, 0f, 720f, 1280f), Resources.Load("Textures/Blue") as Texture);
			GUI.color = new Color(1f, 1f, 1f, 1f);	
			
			if(fadeToBlue < 0.15f)
				fadeToBlue += 0.02f;
		}
		else if(fadeToBlue > 0f)
		{
			GUI.color = new Color(1f, 1f, 1f, fadeToBlue);
			GUI.DrawTexture(new Rect(0f, 0f, 720f, 1280f), Resources.Load("Textures/Blue") as Texture);
			GUI.color = new Color(1f, 1f, 1f, 1f);	

			fadeToBlue -= 0.02f;
		}
	}
	
	public void setAnimationToPickUp()
	{
		if(mode != Mode.TRUCK)
		{
			GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.PICKUP, 16, false);
			animationMode = AnimationMode.PICKUP;
		}
	}
	
	// set mode
	public void setMode(Mode newMode, float timeInMode = 12)
	{
		switch(newMode)
		{
			case Mode.NORMAL:
				setToNormalMode();
				break;
			case Mode.TRUCK:
				setToTruckMode();
				break;
			default:
				break;
		}
		
		// set timer
		modeTimer = timeInMode + Time.timeSinceLevelLoad;	
	}
	
	// set additionalMode
	public void setMode(AdditionalMode newAdditionalMode, float timeInMode = 12)
	{
		AdditionalMode oldMode = additionalMode;
		
		additionalMode = newAdditionalMode;
		
		// set timer
		additionalModeTimer = timeInMode + Time.timeSinceLevelLoad;	


		// ----------to and from speed------------------
		if(additionalMode == AdditionalMode.NORMAL && oldMode == AdditionalMode.SPEED)
		{
			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.GetComponent<ParticleSystem>())
				{
					Destroy(t.gameObject);
				}
			}
		}
		
		if(additionalMode == AdditionalMode.SPEED)
		{
			GameObject speedEffect = (GameObject)Instantiate(Resources.Load("Objects/SpeedEffect"), transform.position + new Vector3(0f, 60f, 20f), transform.rotation);
			speedEffect.transform.parent = transform;
		}

		// ------------------------ to and from ice cream --------
		if(additionalMode == AdditionalMode.ICECREAM)
		{
			Time.timeScale = 0.5f;
			fadeToBlue = 0f;
		}

		if(additionalMode == AdditionalMode.NORMAL && oldMode == AdditionalMode.ICECREAM)
		{
			Time.timeScale = 1f;
		}
	}
	
	public void setToNormalMode()
	{
		Mode oldMode = mode;
		mode = Mode.NORMAL;
		GameObject collider = GameObject.FindWithTag("TrashCollider");
		collider.GetComponent<TrashCanCollider>().offset.y = 100;
		collider.GetComponent<TrashCanCollider>().transform.localScale = new Vector3(73f, 5f, 2f);
		collider.GetComponent<TrashCanCollider>().transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		
		if(oldMode == Mode.TRUCK)
		{
			GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.TOTRUCK, 7, false, 20, true, 8);
			animationMode = AnimationMode.TOTRUCK;
		}
	}
	
	public void setToTruckMode()
	{
		mode = Mode.TRUCK;
		
		// set size of object
		transform.localScale = new Vector3(30, 18, 20);
		
		// set collider to new pos
		GameObject collider = GameObject.FindWithTag("TrashCollider");
		collider.GetComponent<TrashCanCollider>().offset.y = 240;
		collider.GetComponent<TrashCanCollider>().transform.localScale = new Vector3(255f, 5f, 2f);
		collider.GetComponent<TrashCanCollider>().transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 5.3f));
		
		// set animation
		GetComponentInChildren<AnimationScript>().setAnimation((int)AnimationMode.TOTRUCK, 7, false, 20, false, 8);
		animationMode = AnimationMode.TOTRUCK;
	}
	//---------------------------------------------Collision-----------------------------------------------
	//-----------------------------------------------------------------------------------------------------
	
	void OnTriggerStay(Collider collider)
	{
		if(currentEvent != GlobalGameObject.GameEvent.GAMEOVER)
		{
			if(collider.name == "GlasBigHitbox")
			{
				float colliderXPos = collider.transform.parent.transform.position.x;
			
				if(transform.position.x > colliderXPos && !isLeft || transform.position.x < colliderXPos && isLeft)
					collider.transform.parent.GetComponent<GlasOnGround>().stepOnGlas();
			}
			
			if(collider.name == "GlasSmalHitbox")
			{
				float colliderXPos = collider.transform.parent.transform.position.x;
			
				if(transform.position.x > colliderXPos && isLeft || transform.position.x < colliderXPos && !isLeft)
					collider.transform.parent.GetComponent<GlasOnGround>().stepOnGlas();
			}
		}
	}
}
