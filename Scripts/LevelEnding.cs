using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelEnding : MonoBehaviour {


	public string levelToLoad;
	public string levelToUnlock;

	private PlayerController thePlayer;
	private CameraController theCamera;
	private LevelManager theLevelManager;

	public float waitToMove;
	public float waitToLoad;

	private bool movePlayer;

	public Sprite flagClosed;
	public Sprite flagOpen;

	private SpriteRenderer theSpriteRenderer;

	public bool lastLevel;


	// Use this for initialization
	void Start () 
	{
		thePlayer = FindObjectOfType<PlayerController>();
		theCamera = FindObjectOfType<CameraController>();
		theLevelManager = FindObjectOfType<LevelManager>();

		theSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (movePlayer) 
		{
			thePlayer.myRigidBody.velocity = new Vector3 (thePlayer.moveSpeed, thePlayer.myRigidBody.velocity.y, 0f);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			//SceneManager.LoadScene(levelToLoad);
			if (SceneManager.GetActiveScene ().name == "Level3") {
				lastLevel = true;
			} else {
				lastLevel = false;
			}
			StartCoroutine ("LevelEndCo");
			theSpriteRenderer.sprite = flagOpen;
		}
//		if (other.gameObject.tag == "Player") 
//		{
//			theLevelManager.addCollectables(5);
//			SceneManager.LoadScene(levelToLoad);
//		}
	}

	public IEnumerator LevelEndCo()
	{
		thePlayer.canMove = false;
		theCamera.followTarget = false;
		theLevelManager.invincible = true;

		theLevelManager.levelMusic.Stop();
		theLevelManager.gameOverMusic.Play();


//		if (!lastLevel) 
//		{
//			theLevelManager.gameOverMusic.Play();
//		}


		thePlayer.myRigidBody.velocity = Vector3.zero;

		PlayerPrefs.SetInt("collectableCount",theLevelManager.collectableCount);
		PlayerPrefs.SetInt("currentLives",theLevelManager.currentLives);
		PlayerPrefs.SetInt (levelToUnlock, 1);



		yield return new WaitForSeconds(waitToMove);

		movePlayer = true;


		if (lastLevel) 
		{
			yield return new WaitForSeconds(waitToLoad);

			thePlayer.gameObject.SetActive(false);
			theLevelManager.gameWinScreen.SetActive(true);

			//theLevelManager.levelMusic.Stop ();
			//theLevelManager.gameOverMusic.Play();
			//SceneManager.LoadScene(levelToLoad);
		} 
		else 
		{
			yield return new WaitForSeconds(waitToLoad);
			SceneManager.LoadScene(levelToLoad);
		}

	}
}
