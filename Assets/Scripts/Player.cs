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

	float startTime;

	// Use this for initialization
	void Start ()
	{
		world = GameObject.FindGameObjectWithTag("World");
		worldUp = GameObject.FindGameObjectWithTag("WorldUp");
		worldDown = GameObject.FindGameObjectWithTag("WorldDown");
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0))
		{
			worldUp.transform.position = Vector3.Lerp(worldUp.transform.position, world.transform.position, 0.05f);
		} else
		{
			worldUp.transform.position = Vector3.Lerp(worldUp.transform.position, WORLD_UP_STARTING_POSITION, 0.05f);
		}

		if (Input.GetMouseButton (1))
		{
			worldDown.transform.position = Vector3.Lerp(worldDown.transform.position, world.transform.position, 0.05f);
		} else
		{
			worldDown.transform.position = Vector3.Lerp(worldDown.transform.position, WORLD_DOWN_STARTING_POSITION, 0.05f);
		}
	}
}
