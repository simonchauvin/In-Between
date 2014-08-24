using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private GameObject world;
	private GameObject[] teleporters;

	// Use this for initialization
	void Start ()
	{
		world = GameObject.FindGameObjectWithTag("World");
		teleporters = GameObject.FindGameObjectsWithTag("Teleporter");

		// Ignore collisions between player and worlds (up and down)
		Physics.IgnoreLayerCollision(8, 9, true);
	}

	// Update is called once per frame
	void Update ()
	{

	}
	
	void FixedUpdate ()
	{
		// Rotate the world as the player rotates
		world.transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
	}
}
