using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public Vector3 WORLD_STARTING_POSITION = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 WORLD_UP_STARTING_POSITION = new Vector3(0.0f, 40.0f, 0.0f);
	public Vector3 WORLD_DOWN_STARTING_POSITION = new Vector3(0.0f, -40.0f, 0.0f);

	private GameObject world;
	private GameObject worldUp;
	private GameObject worldDown;

	// Use this for initialization
	void Start ()
	{
		world = GameObject.FindGameObjectWithTag("World");
		worldUp = GameObject.FindGameObjectWithTag("WorldUp");
		worldDown = GameObject.FindGameObjectWithTag("WorldDown");

		// Ignore collisions between player and worlds (up and down)
		Physics.IgnoreLayerCollision(8, 9, true);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0) && !worldUp.GetComponent<World>().unbalanced)
		{
			worldUp.transform.position = Vector3.MoveTowards(worldUp.transform.position, world.transform.position, 40.0f * Time.deltaTime);
		} else if (!worldUp.GetComponent<World>().unbalanced)
		{
			worldUp.transform.position = Vector3.MoveTowards(worldUp.transform.position, WORLD_UP_STARTING_POSITION, 40.0f * Time.deltaTime);
		}

		if (Input.GetMouseButton (1) && !worldDown.GetComponent<World>().unbalanced)
		{
			worldDown.transform.position = Vector3.MoveTowards(worldDown.transform.position, world.transform.position, 40.0f * Time.deltaTime);
		} else if (!worldDown.GetComponent<World>().unbalanced)
		{
			worldDown.transform.position = Vector3.MoveTowards(worldDown.transform.position, WORLD_DOWN_STARTING_POSITION, 40.0f * Time.deltaTime);
		}
	}

}
