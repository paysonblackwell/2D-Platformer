using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{


	public GameObject target;
	public float followAhead;

	private Vector3 targetPosition;

	public float smoothing;

	public bool followTarget;


	// Use this for initialization
	void Start () 
	{
		followTarget = true;
	}

	// Update is called once per frame
	void Update () 
	{
		// Setting the camera to the player
		// targetPosition = new Vector3(target.transform.position.x,transform.position.y,transform.position.z);

		// moves the camera ahead of the player
		// change the y position to make the camera move above/below the player
		if(followTarget)
		{
			if (target.transform.localScale.x > 0f) {
				targetPosition = new Vector3 ((target.transform.position.x + followAhead), transform.position.y, transform.position.z);
			} else {
				targetPosition = new Vector3 ((target.transform.position.x - followAhead), transform.position.y, transform.position.z);
			}

			// transform.position = targetPosition;
			transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing*Time.smoothDeltaTime); // time.deltaTime is time between frames
		}
	}
}
