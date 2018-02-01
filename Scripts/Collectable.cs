using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {


	private LevelManager theLevelManager;

	public int collectableValue;
	public int healthToGive;

	public string objectName;

	public bool collected;


	// Use this for initialization
	void Start () 
	{

		gameObject.SetActive (true);
		theLevelManager = FindObjectOfType<LevelManager> ();


	}

	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt(objectName) == 1) {
			collected = true;
			gameObject.SetActive (false);
		} else {
			collected = false;
			gameObject.SetActive(true);
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			theLevelManager.GiveHealth(healthToGive);
			theLevelManager.addCollectables(collectableValue);
			//Destroy (gameObject);

			PlayerPrefs.SetInt(objectName,1);
			collected = true;
			gameObject.SetActive(false);
		}
	}
}