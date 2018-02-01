using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour 
{

	public string firstLevel;
	public string levelSelect;
	public string[] levelNames;
	public int startingLives;

	public GameObject MainMenuScreen;
	public GameObject CreditsScreen;
	public GameObject StoryScreen;


	// Use this for initialization
	void Start () 
	{
		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape)) 
		{
			Application.Quit();
		}
	}

	public void NewGame()
	{
		for (int i = 0; i < levelNames.Length; i++) 
		{
			PlayerPrefs.SetInt(levelNames[i], 0);
		}

		PlayerPrefs.SetInt ("isNewGameLevel1", 1);
		PlayerPrefs.SetInt ("isNewGameLevel2", 1);
		PlayerPrefs.SetInt ("isNewGameLevel3", 1);
		PlayerPrefs.SetInt ("isNewGameSpecialLevel1", 1);

		PlayerPrefs.SetInt ("collectableCount", 0);
		PlayerPrefs.SetInt ("currentLives", startingLives);
		SceneManager.LoadScene(levelSelect);
	}
	public void ContinueGame()
	{
		PlayerPrefs.SetInt ("isNewGame", 0);
		SceneManager.LoadScene(levelSelect);
	}
	public void QuitGame()
	{
		Application.Quit();
	}

	public void Credits()
	{
		MainMenuScreen.SetActive(false);
		CreditsScreen.SetActive(true);

	}

	public void Story()
	{
		MainMenuScreen.SetActive(false);
		StoryScreen.SetActive(true);
	}

	public void BackToMain()
	{
		StoryScreen.SetActive(false);
		CreditsScreen.SetActive(false);
		MainMenuScreen.SetActive(true);
	}

	public void AddCollectables()
	{
		PlayerPrefs.SetInt ("collectableCount",(PlayerPrefs.GetInt ("collectableCount")+10));
	}
	public void AddLives()
	{
		PlayerPrefs.SetInt ("currentLives", (PlayerPrefs.GetInt ("currentLives")+10));
	}
	public void unlockLevels()
	{
		for (int i = 0; i < levelNames.Length; i++) 
		{
			PlayerPrefs.SetInt(levelNames[i], 1);
		}
	}

}
