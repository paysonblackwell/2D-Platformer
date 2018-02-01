using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpecialLevelDoorScript : MonoBehaviour {

	public string LevelToLoad;
	public bool Unlocked;

	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;

	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorTop;
	public SpriteRenderer doorBottom;

	public LevelManager theLevelManager;

	public Text SpecialDoorText;

	public int BambooRequired;


	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.SetInt("Level2", 0);
		PlayerPrefs.SetInt ("Level1", 1);
		if (PlayerPrefs.GetInt (LevelToLoad) == 1) {
			Unlocked = true;
		} else {
			Unlocked = false;
		}


		if (Unlocked) {
			doorTop.sprite = doorTopOpen;
			doorBottom.sprite = doorBottomOpen;
		} 
		else {
			doorTop.sprite = doorTopClosed;
			doorBottom.sprite = doorBottomClosed;
		}

		theLevelManager = FindObjectOfType<LevelManager> ();
		if (PlayerPrefs.GetInt("collectableCount") >= 10) 
		{
			//SpecialLevel1
			Unlocked = true;
			PlayerPrefs.SetInt(LevelToLoad, 1);
			doorTop.sprite = doorTopOpen;
			doorBottom.sprite = doorBottomOpen;
		}

		if (PlayerPrefs.GetInt("collectableCount") >= BambooRequired) 
		{
			SpecialDoorText.text = "Special Level: Unlocked";


		} else 
		{
			SpecialDoorText.text = "Special Level: Locked " + (BambooRequired - PlayerPrefs.GetInt("collectableCount")) + " More Bamboo Required";
		}


	}

	// Update is called once per frame
	void Update () 
	{

	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			if (Input.GetButtonDown("Jump") && Unlocked) 
			{
				SceneManager.LoadScene (LevelToLoad);
			}
		}


	}
}
