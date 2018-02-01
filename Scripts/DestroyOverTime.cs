using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour 
{
	public float lifeTime;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// time.deltaTime, time for frame to take place
		lifeTime = lifeTime - Time.smoothDeltaTime;

		if (lifeTime <= 0) 
		{
			Destroy (gameObject);
		}

		//use for debugging
		//Debug.Log (Time.deltaTime);
	}
}
