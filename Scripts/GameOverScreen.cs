using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour 
{
	public string levelSelect;
	public string mainMenu;
	private LevelManager theLevelManager;
	public Collectable[] collectableObjectArray;
	public string[] collectableStringArray;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();

//		collectableObjectArray = FindObjectsOfType<Collectable>();
//		for (int i = 0; i < collectableObjectArray.Length; i++) 
//		{
//			collectableStringArray[i] = collectableObjectArray[i].objectName;
//		}


			

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RestartLevel()
	{

//		for(int i = 0; i < collectableStringArray.Length; i++)
//		{
//			PlayerPrefs.SetInt(collectableStringArray[i], 0);
//		}
		PlayerPrefs.SetInt ("isNewGame", 0);

		//Resting Collectable Count
		PlayerPrefs.SetInt ("collectableCount", 0);
		PlayerPrefs.SetInt ("currentLives", theLevelManager.startingLives);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LevelSelect()
	{
//		for(int i = 0; i < collectableStringArray.Length; i++)
//		{
//			PlayerPrefs.SetInt(collectableStringArray[i], 0);
//		}
		PlayerPrefs.SetInt ("isNewGame", 1);
		//Resting Collectable Count
		PlayerPrefs.SetInt ("collectableCount", 0);
		PlayerPrefs.SetInt ("currentLives", theLevelManager.startingLives);
		SceneManager.LoadScene(levelSelect);
	}

	public void WinLevelSelect()
	{
		//		for(int i = 0; i < collectableStringArray.Length; i++)
		//		{
		//			PlayerPrefs.SetInt(collectableStringArray[i], 0);
		//		}
		PlayerPrefs.SetInt ("isNewGame", 1);
		//Resting Collectable Count
		SceneManager.LoadScene(levelSelect);
	}

	public void QuitToMain()
	{
//		for(int i = 0; i < collectableStringArray.Length; i++)
//		{
//			PlayerPrefs.SetInt(collectableStringArray[i], 0);
//		}

		SceneManager.LoadScene(mainMenu);
	}
}
