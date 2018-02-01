using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelDoorScript : MonoBehaviour 
{

	public string LevelToLoad;
	public bool Unlocked;

	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;

	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorTop;
	public SpriteRenderer doorBottom;


	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.SetInt("Level2", 0);
		PlayerPrefs.SetInt ("Level1", 1);
		PlayerPrefs.SetInt ("LevelSelect", 1);
		if (PlayerPrefs.GetInt(LevelToLoad) == 1) {
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
