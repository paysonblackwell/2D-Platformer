﻿using UnityEngine;
using System.Collections;

public class StompEnemy : MonoBehaviour {

	private Rigidbody2D playerRB;

	public float bounceForce;

	public GameObject deathSplosion;

	// Use this for initialization
	void Start () 
	{
		playerRB = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);

			Instantiate (deathSplosion, other.transform.position, other.transform.rotation);
			playerRB.velocity = new Vector3(playerRB.velocity.x,bounceForce,0f);
		}

	}
}
