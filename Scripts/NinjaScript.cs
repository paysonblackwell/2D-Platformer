using UnityEngine;
using System.Collections;

public class NinjaScript : MonoBehaviour {

	public float moveSpeed;
	private bool canMove;

	public Transform leftPoint;
	public Transform rightPoint;

	public bool movingRight;


	private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () 
	{
		myRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (movingRight && transform.position.x > rightPoint.position.x) 
//		{
//			movingRight = false;
//		}
//		if (!movingRight && transform.position.x < leftPoint.position.x) 
//		{
//			movingRight = true;
//		}
//
//		if (movingRight) 
//		{
//			myRigidBody.velocity = new Vector3 (moveSpeed, myRigidBody.velocity.y, 0f);
//		} 
//		else 
//		{
//			myRigidBody.velocity = new Vector3 (-moveSpeed, myRigidBody.velocity.y, 0f);
//		}


		if (canMove) 
		{
			myRigidBody.velocity = new Vector3 (-moveSpeed,myRigidBody.velocity.y,0f);
		}
	}

	void OnBecameVisible()
	{
		canMove = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane") 
		{
			//Destroy (gameObject);
			gameObject.SetActive(false);
		}
	}
	void OnEnable()
	{
		canMove = false;
	}
}
