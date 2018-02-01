using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
	public float waitToRespawn;
	public PlayerController thePlayer;

	public GameObject deathSplosion;

	public int collectableCount;
	private int collectableBonusLifeCount;
	public int bonusLifeThreshold;

	public Text collectableText;

	public AudioSource coinSound;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public Sprite heartQuarter;
	public Sprite Heart3Quarter;


	public int maxHealth;
	public int healthCount;

	private bool respawning;


	public ResetOnRespawn[] objectsToReset;

	public bool invincible;

	public int currentLives;
	public int startingLives;

	public Text livesText;

	public GameObject gameOverScreen;

	public GameObject gameWinScreen;

	public AudioSource levelMusic;
	public AudioSource gameOverMusic;


	public Collectable[] collectableObjectArray;
	//public ArrayList collectableStringArray;

	public string leveltoReset;

	// Use this for initialization
	void Start () 
	{

		//Level Select on final win screen still resets the lives + collectable count
		//Debug.Log ("");



		thePlayer = FindObjectOfType<PlayerController> ();
		healthCount = maxHealth;
		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		if (PlayerPrefs.HasKey("collectableCount")) 
		{
			collectableCount = PlayerPrefs.GetInt("collectableCount");
		}
		if (PlayerPrefs.HasKey ("currentLives")) {
			currentLives = PlayerPrefs.GetInt ("currentLives");
		} else 
		{
			currentLives = startingLives;
		}

		collectableText.text = "Bamboo Collected: " + collectableCount;
		livesText.text = "Lives x " + currentLives;


		collectableObjectArray = FindObjectsOfType<Collectable>();
//		collectableStringArray = new ArrayList ();
//
//		for (int i = 0; i < collectableObjectArray.Length; i++) 
//		{
//			collectableStringArray[i] = collectableObjectArray[i].objectName;
//			PlayerPrefs.SetInt (collectableStringArray[i], 0);
//		}

//		if (PlayerPrefs.GetInt ("isNewGame") == 1) 
//		{
//			for(int i = 0; i <= collectableStringArray.LastIndexOf; i++)
//			{
//				PlayerPrefs.SetInt(collectableStringArray[i], 0);
//			}
//			PlayerPrefs.GetInt ("isNewGame", 0);
//
//		}
		if (PlayerPrefs.GetInt(leveltoReset) == 1) 
		{
			for(int i = 0; i < collectableObjectArray.Length; i++)
			{
				PlayerPrefs.SetInt(collectableObjectArray[i].objectName, 0);
			}
			PlayerPrefs.SetInt(leveltoReset, 0);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (healthCount <= 0 && !respawning) 
		{
			Respawn ();
			respawning = true;
		}
		// Comment this out to take out 100 coin bonus life;
		if (collectableBonusLifeCount >= bonusLifeThreshold) 
		{
			currentLives += 1;
			livesText.text = "Lives x " + currentLives;
			collectableBonusLifeCount -= bonusLifeThreshold;
		}
	}

	public void Respawn()
	{
		thePlayer.resetPlayer ();
		currentLives -= 1;
		livesText.text = "Lives x " + currentLives;

		thePlayer.transform.parent = null;
		thePlayer.setOnPlatform (false);

		if (currentLives > 0) {
			StartCoroutine ("RespawnCo");
		} 
		else 
		{
			
			for(int i = 0; i < collectableObjectArray.Length; i++)
			{
				PlayerPrefs.SetInt(collectableObjectArray[i].objectName, 0);
			}
//			for(int i = 0; i < collectableStringArray.Length; i++)
//			{
//				PlayerPrefs.SetInt(collectableStringArray[i], 0);
//			}


			collectableCount = 0;
			collectableText.text = "Bamboo Collected: " + collectableCount;

			thePlayer.gameObject.SetActive(false);
			gameOverScreen.SetActive (true);

			levelMusic.Stop ();
			gameOverMusic.Play();

			//levelMusic.volume = levelMusic.volume/3;
		}

	}

	// coroutine
	public IEnumerator RespawnCo()
	{
		thePlayer.setKBC(0f);
		thePlayer.gameObject.SetActive(false);

		Instantiate (deathSplosion, thePlayer.transform.position,thePlayer.transform.rotation);

		// If this wasn't in the coroutine then nothing could update when you use a time delay
		yield return new WaitForSeconds(waitToRespawn);

		healthCount = maxHealth;

		UpdateHeartMeter();

		//collectableCount = 0;
		//collectableText.text = "Bamboo Collected: " + collectableCount;


		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive(true);

		for (int i = 0; i < objectsToReset.Length; i++) 
		{
			objectsToReset [i].gameObject.SetActive(true);
			objectsToReset[i].ResetObject();
		}
		respawning = false;
	}

	public void addCollectables(int collectablesToAdd)
	{
		collectableCount += collectablesToAdd;

		collectableBonusLifeCount += collectablesToAdd;

		collectableText.text = "Bamboo Collected: " + collectableCount;
		coinSound.Play();
	}

	public void callRespawn()
	{
		respawning = true;
		Respawn ();
	}
	public void HurtPlayer(int damageToTake)
	{
		if (!invincible) 
		{
			healthCount -= damageToTake;
			UpdateHeartMeter();

			thePlayer.KnockBack();

			thePlayer.hurtSound.Play ();
		}

	}
	public void GiveHealth(int healthToGive)
	{
		healthCount += healthToGive;
		if (healthCount > maxHealth) 
		{
			healthCount = maxHealth;
		}
		coinSound.Play();
		UpdateHeartMeter ();
	}


	public void UpdateHeartMeter()
	{
		switch (healthCount)
		{
		//heartQuarter;
		//Heart3Quarter;


		case 12:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartFull;
			return;
		case 11:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = Heart3Quarter;
			return;
		case 10:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartHalf;
			return;
		case 9:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartQuarter;
			return;
		case 8:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartEmpty;
			return;
		case 7:
			heart1.sprite = heartFull;
			heart2.sprite = Heart3Quarter;
			heart3.sprite = heartEmpty;
			return;
		case 6:
			heart1.sprite = heartFull;
			heart2.sprite = heartHalf;
			heart3.sprite = heartEmpty;
			return;
		case 5:
			heart1.sprite = heartFull;
			heart2.sprite = heartQuarter;
			heart3.sprite = heartEmpty;
			return;
		case 4:
			heart1.sprite = heartFull;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 3:
			heart1.sprite = Heart3Quarter;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 2:
			heart1.sprite = heartHalf;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 1:
			heart1.sprite = heartQuarter;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 0:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		default:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		}
	}

	public void AddLives(int livesToAdd)
	{
		coinSound.Play();
		currentLives += livesToAdd;
		livesText.text = "Lives x " + currentLives;
	}
}
