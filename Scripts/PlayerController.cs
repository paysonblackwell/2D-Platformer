using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed;
	private float defaultMoveSpeed;
	public Rigidbody2D myRigidBody;
	public float jumpSpeed;

	private float activeMoveSpeed;

	public bool canMove;

	public Transform groundCheck; // Only want to jump when on ground
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	public LevelManager theLevelManager;

	public GameObject stompBox;

	public float knockBackForce;
	public float knockBackLength;
	private float knockBackCounter;
	public float invincibilityLength;
	private float invincibilityCounter;

	public AudioSource jumpSound;
	public AudioSource hurtSound;

	private bool onPlatform;
	public float onPlatformSpeedModifier;

	public float leftMouseClickGravity;
	public float RightMouseClickGravity;
	public float MiddleMouseClickGravity;

	public float leftMouseClickSpeed;
	public float rightMouseClickSpeed;


	private SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator> ();

		respawnPosition = transform.position;

		theLevelManager = FindObjectOfType<LevelManager> ();

		activeMoveSpeed = moveSpeed;
		defaultMoveSpeed = moveSpeed;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKey (KeyCode.Escape)) 
		{

			PlayerPrefs.SetInt ("collectableCount", theLevelManager.collectableCount);
			PlayerPrefs.SetInt ("currentLives", theLevelManager.currentLives);

			SceneManager.LoadScene("Main Menu");
		}

		if (Input.GetMouseButtonDown(0)) 
		{
			if (myRigidBody.gravityScale == leftMouseClickGravity) 
			{
				moveSpeed = defaultMoveSpeed;
				myRigidBody.gravityScale = MiddleMouseClickGravity;
				mySpriteRenderer.color = new Color (255f,255f,255f,255f);
			} else 
			{
				moveSpeed = leftMouseClickSpeed;
				mySpriteRenderer.color = new Color (0f,50f,50f,255f);
				myRigidBody.gravityScale = leftMouseClickGravity;
			}
		}
		else if (Input.GetMouseButtonDown(1)) 
		{
			if (myRigidBody.gravityScale == RightMouseClickGravity) 
			{
				moveSpeed = defaultMoveSpeed;
				myRigidBody.gravityScale = MiddleMouseClickGravity;
				mySpriteRenderer.color = new Color (255f,255f,255f,255f);
			} else 
			{
				moveSpeed = rightMouseClickSpeed;
				myRigidBody.gravityScale = RightMouseClickGravity;
				mySpriteRenderer.color = new Color (01f,20f,0f,255f);
			}
		}
		else if (Input.GetMouseButtonDown(2)) 
		{
			moveSpeed = defaultMoveSpeed;
			mySpriteRenderer.color = new Color (255f,255f,255f,255f);
			myRigidBody.gravityScale = MiddleMouseClickGravity;
		}






		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (knockBackCounter <= 0 && canMove) 
		{

			if (onPlatform) 
			{
				activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
			} 
			else 
			{
				activeMoveSpeed = moveSpeed;
			}
			if (Input.GetAxisRaw ("Horizontal") > 0f) { //Moving to the right
				myRigidBody.velocity = new Vector3 (activeMoveSpeed, myRigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) { //Moving to the left
				myRigidBody.velocity = new Vector3 (-activeMoveSpeed, myRigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else { // Not Moving
				myRigidBody.velocity = new Vector3 (0f, myRigidBody.velocity.y, 0f);
			}
			if (Input.GetButtonDown ("Jump") && isGrounded) {
				myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, jumpSpeed, 0f);
				jumpSound.Play();
			}
		} 
		if (knockBackCounter > 0)
		{
			knockBackCounter -= Time.smoothDeltaTime;

			if (transform.localScale.x > 0) {
				myRigidBody.velocity = new Vector3 ((knockBackForce/5), knockBackForce, 0f);
			} 
			else 
			{
				myRigidBody.velocity = new Vector3 ((-knockBackForce/5), knockBackForce, 0f);
			}
		}
		if (invincibilityCounter <= 0) {
			theLevelManager.invincible = false;
		} else 
		{
			invincibilityCounter -= Time.smoothDeltaTime;
		}


		myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
		myAnim.SetBool ("Ground", isGrounded);

		if (myRigidBody.velocity.y < 0) {
			stompBox.SetActive (true);
		} 
		else 
		{
			stompBox.SetActive (false);
		}

	}

	public void KnockBack()
	{
		knockBackCounter = knockBackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}

	public void setOnPlatform(bool x)
	{
		onPlatform = x;
	}

	public void setKBC(float x)
	{
		knockBackCounter = x;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane") 
		{
			//gameObject.SetActive (false);
			//transform.position = respawnPosition;
			theLevelManager.healthCount = 0;
			theLevelManager.UpdateHeartMeter();
			theLevelManager.callRespawn();
		}
		if (other.tag == "Checkpoint") 
		{
			respawnPosition = other.transform.position;
		}
	}

	// If there is no collision exit, the player moves with the platform even if he isn't touching it
	// This could be used a wind affect in the game.
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "MovingPlatform")
		{
			transform.parent = other.transform;
			onPlatform = true;
		}
	}  
	void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.tag == "MovingPlatform")
		{
			transform.parent = null;
			onPlatform = false;
		}
	}

	public void resetPlayer()
	{
		moveSpeed = defaultMoveSpeed;
		mySpriteRenderer.color = new Color (255f,255f,255f,255f);
		myRigidBody.gravityScale = MiddleMouseClickGravity;
	}

}
