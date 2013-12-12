using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalGameObject : MonoBehaviour
{
	public bool stop = false;

	public int thisLevel = 1;

	public GameObject[] objectToSpawn;
	public float[] timer;
	public float timerCount = 0;
	public float[] randomTimeDifMax;
	
	public int trashOnGround = 0;
	
	public int points = 0;
	public int numberOfCaughtTrash = 0;
	public int numberOfTrashToWin = 100;

	public int numberOfNormalTrash = 0;
	public int numberOfGlasTrash = 0;

	public float lowerTimeTime = 1f;
	public float lowerTimeTimer = 1f;
	public int pointsToUnlockNextLevel = 100;
	public Texture trashOnGroundTexture;
	public int tooManyTrashOnGround = 2;
	public int comboMultiplyer = 0;
	public int trashInARow = 0;
	public int[] maxEnemies;
	public GameObject popup;
	public int howManyToGetCombo = 8;
	public int increaseHowManyToGetComboMultiply = 3;
	public int maxHowManyToGetCombo = 64;
	public bool setSisterInPlay = false;
	public bool sisterInPlay = true;
	public float sisterInPlayTimer = 0f;
	public float timeToSister = 20f;

	public int[] timeToChangeState;
	public int currentState = 0;
	public bool startLeft = true;
	
	public bool pause = false;
	
	public enum GameEvent {NOEVENT, CATCHFIVE, RAIN, INLOVE, GAMEOVER};
	public GameEvent currentEvent = GameEvent.NOEVENT;
	
	public int numberOfCatchedBonusTrash = 0;
	
	public bool timeForDoubleBigTrash = false;
	public float doubleBigTrashTimer = 0f;
	public bool canThrowBattery = false;
	public string gameOverTexture = "gameOverBigTrash";
	public bool powerUpOnScreen = false;

	private bool leftSide = false;
	private bool rightSide = false;
	private bool sideEffectFadeOut = false;
	private float sideEffectAlpha = 0f;

	private float bonusEffectAlpha = 0f;

	private int numberOfEnemies = 0;
	
	//private float gameOverPopupTime = 3f;
	//private bool gameOverPopupTimeRunOut = false;
	
	public GameObject myCamera;
	
	// textures
	public string[] popupComboTextures = {	"sprite_combotext_2x", "sprite_combotext_3x", "sprite_combotext_4x", "sprite_combotext_5x", "sprite_combotext_6x",
											"sprite_combo_2x", "sprite_combo_3x", "sprite_combo_4x", "sprite_combo_5x", "sprite_combo_6x"};
	
	void Start()
	{
		myCamera = GameObject.FindWithTag("MainCamera");
		
		doubleBigTrashTimer = Time.timeSinceLevelLoad + 20f;

		PlayerPrefs.SetInt("CurrentLevel", thisLevel);
	}
	
	void Update ()
	{
		// time left
		lowerTimeTimer -= Time.deltaTime;
		if(lowerTimeTimer < 0 && currentEvent == GameEvent.NOEVENT)
		{
			points--;
			lowerTimeTimer = lowerTimeTime;
		}

		if(points <= 0 && currentEvent == GameEvent.NOEVENT)
			startEvent(GameEvent.GAMEOVER);

		//Generate enemies if not in game over
		if(currentEvent != GameEvent.GAMEOVER && currentEvent != GameEvent.INLOVE)
		{
			//--------------------------------------GenerateEnemys---------------------------------------------------
			//-------------------------------------------------------------------------------------------------------
			
	 		if(timerCount > 0f)
	 		{
				timerCount -= Time.deltaTime;
			}
			
			numberOfEnemies = 0;
			foreach(Transform enemy in transform)
			{
				if(enemy.GetComponent<Enemy>())
					if(!enemy.GetComponent<Enemy>().ignoreMe)
						numberOfEnemies++;
			}

			// sister timer
			if(!sisterInPlay)
				sisterInPlayTimer -= Time.deltaTime;

			if(sisterInPlayTimer < 0f)
			{
				setSisterInPlay = true;
				sisterInPlayTimer = timeToSister;
			}

			// create object
			if(timerCount <= 0f && numberOfEnemies < maxEnemies[currentState])
			{	
				GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilitySpawn()], transform.position, transform.rotation);					
				newObject.transform.parent = transform;	
				
				startLeft = !startLeft;	
				
				timerCount = timer[currentState] + Random.Range(0f, randomTimeDifMax[currentState]);
			}
		}
		
		//--------------------------------------------No event---------------------------------------------------
		//-------------------------------------------------------------------------------------------------------
		if(currentEvent == GameEvent.NOEVENT)
		{
	 		//--------------------------------------CurrentState-----------------------------------------------------
			//-------------------------------------------------------------------------------------------------------
			
			if(currentState < timeToChangeState.Length && Time.timeSinceLevelLoad >= timeToChangeState[currentState])
				currentState++;
			if(currentState > 3)
				canThrowBattery = true;
			
	 		
			//--------------------------------------TrashOnGround---------------------------------------------------
			//------------------------------------------------------------------------------------------------------
			
			if(trashOnGround >= tooManyTrashOnGround)
			{
				startEvent(GameEvent.GAMEOVER);
			}
			
			if(pause && !GameObject.FindWithTag("PauseMenu").animation.isPlaying)
				Time.timeScale = 0;	
			//--------------------------------------Combo-----------------------------------------------------------
			//------------------------------------------------------------------------------------------------------
			if(trashInARow > howManyToGetCombo - 1)
			{
				trashInARow = 0;
				if(comboMultiplyer < 6)
				{
					comboMultiplyer++;
				
					howManyToGetCombo *= increaseHowManyToGetComboMultiply;
				
					//if(howManyToGetCombo > maxHowManyToGetCombo)
					//	howManyToGetCombo = maxHowManyToGetCombo;
				}
				
				GameObject newObject = (GameObject)Instantiate(popup, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));			
				newObject.GetComponent<PopUp>().setTexture(popupComboTextures[comboMultiplyer - 1]);
				newObject.transform.parent = myCamera.transform;
			}
			
			
			// dubbel bigtrash timer 
			if(timeForDoubleBigTrash)
				doubleBigTrashTimer = Time.timeSinceLevelLoad + 20f;
			
			else if(Time.timeSinceLevelLoad > doubleBigTrashTimer)
				timeForDoubleBigTrash = true;
		}
		
		//--------------------------------------Event-----------------------------------------------------------
		//-------------------------------------CatchFive--------------------------------------------------------
		if(currentEvent == GameEvent.CATCHFIVE)
		{
			// succes
			if(numberOfCatchedBonusTrash > 4)
			{
				startEvent(GameEvent.NOEVENT);
			}
		}
		
		//--------------------------------------Event-----------------------------------------------------------
		//--------------------------------------Rain------------------------------------------------------------
		if(currentEvent == GameEvent.RAIN)
		{			
			if(!GetComponentInChildren<Trash>() && GetComponentInChildren<CorporateGuy>().doneThrowing)
				startEvent(GameEvent.NOEVENT);
		}
		
		//--------------------------------------Event-----------------------------------------------------------
		//--------------------------------------Game over-------------------------------------------------------
		//if(currentEvent == GameEvent.GAMEOVER)
		//{
		//	gameOverPopupTime -= Time.deltaTime;
		//	
		//	//Borgmästaren kommer in
		//	if(gameOverPopupTime < 0f && !gameOverPopupTimeRunOut)
		//	{
		//
		//		gameOverPopupTimeRunOut = true;
		//	}
		//}

		foreach(Enemy t in GetComponentsInChildren<Enemy>())
		{
			// stop 
			if(stop)
				t.speed[currentState] = 0f;
		}
	}
	
	//|||||---------------------------------------------------------------------------------------------|||||
	//|||||--------------------------------------GUI----------------------------------------------------|||||
	//|||||---------------------------------------------------------------------------------------------|||||
	void OnGUI()
	{	
		// points
		GUIStyle style = new GUIStyle();
		style.font = Resources.Load("Font/BADABB") as Font;
		style.fontSize = Screen.width / 5;
		
		GUI.Label(new Rect(10, 10, 30, 30), points.ToString(), style); 
		
		GUI.color = new Color(1, 1, 1, 1);
		
		// combo multiplyer
		GUI.color = new Color(1, 1, 1, 1);
		if(comboMultiplyer > 1)
			GUI.DrawTexture(new Rect(0f, Screen.height - Screen.width / 2.5f, Screen.width / 5f, Screen.width / 5f), Resources.Load("Textures/Interface/" + popupComboTextures[5 + comboMultiplyer - 2]) as Texture);
		
		//-------------------------------------Pause------------------------------------------------------------
		//------------------------------------------------------------------------------------------------------
		
		GUI.color = new Color(1f, 1f, 1f, 0.7f);
		Rect rect = new Rect(Screen.width - Screen.width / 7f, 0f, Screen.width / 7f, Screen.width / 7f);
		GUI.DrawTexture(rect, Resources.Load("Textures/Interface/Pause") as Texture);
		
		Event e = Event.current;
		
		if(e.type == EventType.MouseUp)
		{
			if(rect.Contains(e.mousePosition))
			{
				if(!pause)
				{
					pauseFunc();
				}
				else
				{
					unpause();
				}
			}
		}

		// side glow 
		bool leftSideFunc = false;
		bool rightSideFunc = false;
		trashOutsideOfScreen(ref leftSideFunc, ref rightSideFunc);

		if(sideEffectAlpha < 0.5 && (leftSideFunc || rightSideFunc) && currentEvent == GameEvent.NOEVENT)
			sideEffectFadeOut = false;
		else if(sideEffectAlpha > 0.8  || (!leftSideFunc && !rightSideFunc) || currentEvent != GameEvent.NOEVENT)
			sideEffectFadeOut = true;

		if(sideEffectFadeOut && sideEffectAlpha > 0f)
			sideEffectAlpha -= 0.008f;
		else if(!sideEffectFadeOut)
			sideEffectAlpha += 0.008f;

		if(sideEffectAlpha <= 0f)
		{
			leftSide = false;
			rightSide = false;
		}

		if(leftSideFunc)
			leftSide = true;
		if(rightSideFunc)
			rightSide = true;

		if(leftSide)
		{
			GUI.color = new Color(1f, 1f, 1f, sideEffectAlpha);
			Rect sideGlowRect = new Rect(0f, 0f, Screen.width / 10f, Screen.height);
			GUI.DrawTexture(sideGlowRect, Resources.Load("Textures/glow_side_01") as Texture);
		}
		if(rightSide)
		{
			GUI.color = new Color(1f, 1f, 1f, sideEffectAlpha);
			Rect sideGlowRect = new Rect(Screen.width - Screen.width / 10f, 0f, Screen.width / 10f, Screen.height);
			GUI.DrawTextureWithTexCoords(sideGlowRect, Resources.Load("Textures/glow_side_01") as Texture, new Rect(0f, 0f, -1f, 1f));
		}

		// bonus glow

		if(currentEvent != GameEvent.NOEVENT && currentEvent != GameEvent.GAMEOVER)
		{
			if(bonusEffectAlpha < 0.7f)
				bonusEffectAlpha += 0.01f;
		}
		else
		{
			if(bonusEffectAlpha > 0f)
				bonusEffectAlpha -= 0.01f;
		}

		if(bonusEffectAlpha > 0f)
		{
			GUI.color = new Color(1f, 1f, 1f, bonusEffectAlpha);
			Rect sideGlowRect = new Rect(0f, 0f, Screen.width, Screen.height);
			GUI.DrawTexture(sideGlowRect, Resources.Load("Textures/Interface/Pause") as Texture);
		}

	}

	void trashOutsideOfScreen(ref bool leftSideFunc, ref bool rightSideFunc)
	{
		// power up on screen
		powerUpOnScreen = false;

		foreach(Transform t in GetComponentsInChildren<Transform>())
		{
			if(t.GetComponent<Trash>() && !t.GetComponent<Trash>().ignoreMe && !t.GetComponent<Trash>().dontWarnAboutMe)
			{
				float cameraPos = myCamera.transform.position.x;
				if(t.position.x < cameraPos - 360) 
					leftSideFunc = true;
				if(t.position.x > cameraPos + 360)
					rightSideFunc = true;
			}
			// power up on screen
			else if(t.GetComponent<PowerUpTrash>())
			{
				powerUpOnScreen = true;
			}
		}
	}
	public void unpause()
	{
		if(pause)
		{
			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.GetComponent<Trash>())
				{
					t.GetComponent<Trash>().enabled = true;
				}
				else if(t.GetComponent<Player>())
				{
					t.GetComponent<Player>().enabled = true;
				}
				else if(t.GetComponent<Enemy>())
				{
					t.GetComponent<Enemy>().enabled = true;
				}
				
				if(t.GetComponentInChildren<AnimationScript>())
				{
					t.GetComponentInChildren<AnimationScript>().enabled = true;	
				}
				
			}
			
			Time.timeScale = 1;
			pause = false;
		
			GameObject.FindWithTag("PauseMenu").animation.Play("PauseMenuOutAnimation");
		}
	}
	
	private void pauseFunc()
	{
		if(!pause)
		{
			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.GetComponent<Trash>())
				{
					t.GetComponent<Trash>().enabled = false;
				}
				else if(t.GetComponent<Player>())
				{
					t.GetComponent<Player>().enabled = false;
					t.gameObject.rigidbody.velocity = new Vector3();
				}
				else if(t.GetComponent<Enemy>())
				{
					t.GetComponent<Enemy>().enabled = false;
				}
				
				if(t.GetComponentInChildren<AnimationScript>())
				{
					t.GetComponentInChildren<AnimationScript>().enabled = false;	
				}
			}
			
			pause = true;
			GameObject.FindWithTag("PauseMenu").animation.Play("PauseMenuInAnimation");
		}
	}
	
	// reset combo
	public void resetCombo()
	{
		trashInARow = 0;
		comboMultiplyer = 0;
		howManyToGetCombo = 8;
	}
	
	// start new event
	public void startEvent(GameEvent newEvent)
	{
		resetEvent(currentEvent);
		
		currentEvent = newEvent;
		
		// destroy all trash
		if(currentEvent != GameEvent.NOEVENT)
		{
			foreach(Transform t in GetComponentsInChildren<Transform>())
			{
				if(t.GetComponent<Trash>() && (!t.GetComponent<Trash>().canBePickedUp || currentEvent != GameEvent.GAMEOVER))
				{
					t.GetComponent<Trash>().destroyAndPoff((t.GetComponent<Trash>().points * comboMultiplyer).ToString());
					
					if(currentEvent != GameEvent.GAMEOVER)
						points += t.GetComponent<Trash>().points * comboMultiplyer;
				}
			}
			
			// bonus text
			if(currentEvent != GameEvent.GAMEOVER)
			{
				GameObject newObject = (GameObject)Instantiate(popup, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));
				newObject.transform.parent = myCamera.transform;
				newObject.GetComponent<PopUp>().setTexture("Bonustext");
			}
		}
		
		// Rain
		if(currentEvent == GameEvent.RAIN)
		{
			//Spawn pair enemies
			GameObject newObject = Instantiate(Resources.Load("Objects/Enemy/CorporateGuy"), transform.position + new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
			newObject.transform.parent = transform;

			newObject = Instantiate(Resources.Load("Objects/Enemy/CorporateGuy"), transform.position - new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
			newObject.transform.parent = transform;
		}
		
		// InLove
		if(currentEvent == GameEvent.INLOVE)
		{
			//Spawn pair enemies
			GameObject newObject1 = Instantiate(Resources.Load("Objects/Enemy/PersonInLove"), transform.position + new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
			newObject1.transform.parent = transform;

			GameObject newObject2 = Instantiate(Resources.Load("Objects/Enemy/PersonInLove"), transform.position - new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
			newObject2.transform.parent = transform;
			
			newObject1.GetComponent<PersonInLove>().partner = newObject2.GetComponent<PersonInLove>();
			newObject2.GetComponent<PersonInLove>().partner = newObject1.GetComponent<PersonInLove>();
			
		}
		
		// GameOver
		if(currentEvent == GameEvent.GAMEOVER)
		{
			saveScore();

			GameObject.FindWithTag("GameOverMenu").animation.Play("PauseMenuInAnimation");
			//GameObject newObject = (GameObject)Instantiate(popup, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));
			//newObject.transform.parent = myCamera.transform;
			//newObject.GetComponent<PopUp>().setTexture(gameOverTexture);
			//newObject.GetComponent<PopUp>().timeOnScreen = gameOverPopupTime;
		}
		
		// CatchFive
		if(currentEvent == GameEvent.CATCHFIVE)
		{
			numberOfCatchedBonusTrash = 0;
		}
	}
	
	// reset old event, run by start event
	private void resetEvent(GameEvent oldEvent)
	{
		if(oldEvent == GameEvent.CATCHFIVE)
		{
			if(numberOfCatchedBonusTrash > 4)
			{
				GameObject newObject = (GameObject)Instantiate(Resources.Load("Objects/CoinPopUp") as GameObject, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));
				newObject.transform.parent = myCamera.transform;
			}
		}
	}
	
	// probability spawn
	private int probabilitySpawn()
	{
		// probability of eatch enemy
		
		float probability = Random.value;
		int objectToSpawnIndex = 0;

		// level 1 och 2
		if(thisLevel == 1 || thisLevel == 2)
		{
			if(probability < 0.60f) 
				objectToSpawnIndex = 0; // 60% enemy
			
			else 
				objectToSpawnIndex = 1; // 40% old man

			
			// if its time to spawn sister
			if(setSisterInPlay)
			{
				objectToSpawnIndex = 2; 
				setSisterInPlay = false;
				sisterInPlay = true;
			}
		}
		return objectToSpawnIndex;
	}

	public void saveScore()
	{
		// savePoints
		int myPoints = points;
		for(int i = 0; i < 10; ++i)
		{
			string highScore = "AllTimeHighScore" + i.ToString();
			if(myPoints > PlayerPrefs.GetInt(highScore))
			{
				int tempPoints = myPoints;
				myPoints = PlayerPrefs.GetInt(highScore);
				PlayerPrefs.SetInt(highScore, tempPoints);
			}
		}

		// unlock next level
		if(points >= pointsToUnlockNextLevel)
		{
			string nextLevel = "LevelUnlocked" + (thisLevel + 1).ToString();
			PlayerPrefs.SetInt(nextLevel, 1);
		}
	}
}
