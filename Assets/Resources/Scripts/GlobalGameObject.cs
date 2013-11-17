using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GlobalGameObject : MonoBehaviour
{
	public GameObject[] objectToSpawn;
	public float[] timer;
	public float timerCount = 0;
	public float[] randomTimeDifMax;
	
	public int trashOnGround = 0;
	
	public int points = 0;
	public Texture trashOnGroundTexture;
	public int tooManyTrashOnGround = 2;
	public int comboMultiplyer = 1;
	public int trashInARow = 0;
	public int[] maxEnemies;
	public GameObject popup;
	public int howManyToGetCombo = 4;
	public int increaseHowManyToGetComboMultiply = 2;
	public int maxHowManyToGetCombo = 16;
	public bool sisterInPlay = false;
	
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
	
	private int numberOfEnemies = 0;
	
	private float gameOverPopupTime = 3f;
	private bool gameOverPopupTimeRunOut = false;
	
	public GameObject myCamera;
	
	// textures
	public string[] popupComboTextures = {	"Combotaunt01", "Combotaunt02", "Combotaunt03", "Combotaunt04", "Combotaunt05",
											"Combotext01", "Combotext02", "Combotext03", "Combotext04", "Combotext05"};
	
	void Start()
	{
		myCamera = GameObject.FindWithTag("MainCamera");
		
		doubleBigTrashTimer = Time.timeSinceLevelLoad + 20f;
	}
	
	void Update ()
	{
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
			
			// create object
			if(timerCount <= 0f && numberOfEnemies < maxEnemies[currentState])
			{	
				GameObject newObject = (GameObject)Instantiate(objectToSpawn[probabilityThrow()], transform.position, transform.rotation);					
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
				if(comboMultiplyer < 5)
				{
					comboMultiplyer++;
				
					howManyToGetCombo *= increaseHowManyToGetComboMultiply;
				
					if(howManyToGetCombo > maxHowManyToGetCombo)
						howManyToGetCombo = maxHowManyToGetCombo;
				}
				
				GameObject newObject = (GameObject)Instantiate(popup, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));	
				newObject.GetComponent<PopUp>().setTexture(popupComboTextures[comboMultiplyer - 2]);
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
			if(!GetComponentInChildren<Trash>() && GetComponentInChildren<PairEnemy>().doneThrowing)
				startEvent(GameEvent.NOEVENT);
		}
		
		//--------------------------------------Event-----------------------------------------------------------
		//--------------------------------------Game over-------------------------------------------------------
		if(currentEvent == GameEvent.GAMEOVER)
		{
			gameOverPopupTime -= Time.deltaTime;
			
			//Borgmästaren kommer in
			if(gameOverPopupTime < 0f && !gameOverPopupTimeRunOut)
			{
				GameObject.FindWithTag("GameOverMenu").animation.Play("PauseMenuInAnimation");
				gameOverPopupTimeRunOut = true;
			}
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
		style.fontSize = 50;
		
		GUI.Label(new Rect(10, 10, 30, 30), points.ToString(), style); 
		
		GUI.color = new Color(1, 1, 1, 1);

		// arrow
		Rect arroRect = new Rect(Screen.width / 12f, Screen.height - Screen.height / 9f, Screen.width - Screen.width / 6f, Screen.height / 9f);
		GUI.color = new Color(1, 1, 1, 1);
		

		
		if(Input.GetMouseButtonDown(0))
		{
			if(arroRect.Contains(Input.mousePosition))
			{
				GUI.color = new Color(1f, 1f, 1f, 0.3f);
			}
		}

		GUI.DrawTexture(arroRect, Resources.Load("Textures/Interface/Arrow") as Texture);
		
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
		comboMultiplyer = 1;
		howManyToGetCombo = 4;
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
				if(t.GetComponent<Trash>() && !t.GetComponent<Trash>().canBePickedUp)
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
			GameObject newObject = Instantiate(Resources.Load("Objects/Enemy/PairEnemy"), transform.position + new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
			newObject.transform.parent = transform;

			newObject = Instantiate(Resources.Load("Objects/Enemy/PairEnemy"), transform.position - new Vector3(800f, 0f, 0f), transform.rotation) as GameObject;					
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
			GameObject newObject = (GameObject)Instantiate(popup, myCamera.transform.position + new Vector3(0, 0, 100), Quaternion.Euler(new Vector3(90, 180, 0)));
			newObject.transform.parent = myCamera.transform;
			newObject.GetComponent<PopUp>().setTexture(gameOverTexture);
			newObject.GetComponent<PopUp>().timeOnScreen = gameOverPopupTime;
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
	
	// probability Throw
	private int probabilityThrow()
	{
		// probability of eatch enemy
		
		float probability = Random.value;
		int objectToSpawnIndex = 0;
		
		// level 1
		if(EditorApplication.currentScene == "Assets/Resources/Scenes/MainGame.unity")
		{
			if(probability < 0.20f) 
				objectToSpawnIndex = 0; // 20%
			
			else if(probability < 0.40f)
				objectToSpawnIndex = 1; // 20% 
			
			else if(probability < 0.70f)
				objectToSpawnIndex = 2; // 30%
			
			else if(!sisterInPlay)
			{
				objectToSpawnIndex = 3; // 30%
				sisterInPlay = true;
			}
		}
		
		// level 2
		if(EditorApplication.currentScene == "Assets/Resources/Scenes/Level2.unity")
		{
			if(probability < 0.20f) 
				objectToSpawnIndex = 0; // 20%
			
			else if(probability < 0.40f)
				objectToSpawnIndex = 1; // 20% 
			
			else if(probability < 0.70f)
				objectToSpawnIndex = 2; // 30%
			
			else if(!sisterInPlay)
			{
				objectToSpawnIndex = 3; // 30%
				sisterInPlay = true;
			}
		}
		return objectToSpawnIndex;
	}
}
