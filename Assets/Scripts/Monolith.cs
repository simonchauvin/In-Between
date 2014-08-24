using UnityEngine;
using System.Collections;

public class Monolith : MonoBehaviour {
	public const string WORLD = "world";
	public const string WORLD_UP = "worldUp";
	public const string WORLD_DOWN = "worldDown";
	
	public Material worldMaterial;
	public Material worldUpMaterial;
	public Material worldDownMaterial;

	private string currentWorld;
	private string nextWorld;

	private bool canTeleport;

	// Use this for initialization
	void Start () {
		currentWorld = WORLD;
		nextWorld = WORLD;

		canTeleport = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit (Collider other)
	{
		// The player cannot be teleported to a new world anymore
		if (other.CompareTag("Player"))
		{
			canTeleport = false;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		// Cross fade between world colors
		if (other.CompareTag("Player"))
		{
			StartCoroutine("crossFade");
			canTeleport = true;
		}

		// Monolith transportation to world
		if (other.CompareTag("World")) {
			if (nextWorld.Equals(WORLD) && !currentWorld.Equals(WORLD))
			{
				if (currentWorld.Equals(WORLD_UP))
				{
					WorldsManager.worlds[1].GetComponent<World>().removeMonolith(gameObject);
				} else if (currentWorld.Equals(WORLD_DOWN))
				{
					WorldsManager.worlds[2].GetComponent<World>().removeMonolith(gameObject);
				}
				WorldsManager.worlds[0].GetComponent<World>().addMonolith(gameObject);
				currentWorld = WORLD;
			}
		}
		// Monolith transportation to world up
		if (other.CompareTag("WorldUp")) {
			if (nextWorld.Equals(WORLD_UP) && !currentWorld.Equals(WORLD_UP))
			{
				if (currentWorld.Equals(WORLD))
				{
					WorldsManager.worlds[0].GetComponent<World>().removeMonolith(gameObject);
					// Stop monolith rotation with world

				} else if (currentWorld.Equals(WORLD_DOWN))
				{
					WorldsManager.worlds[2].GetComponent<World>().removeMonolith(gameObject);
				}
				WorldsManager.worlds[1].GetComponent<World>().addMonolith(gameObject);
				currentWorld = WORLD_UP;
			}
		}
		// Monolith transportation to world down
		if (other.CompareTag("WorldDown")) {
			if (nextWorld.Equals(WORLD_DOWN) && !currentWorld.Equals(WORLD_DOWN))
			{
				if (currentWorld.Equals(WORLD))
				{
					WorldsManager.worlds[0].GetComponent<World>().removeMonolith(gameObject);
					// Stop monolith rotation with world

				} else if (currentWorld.Equals(WORLD_UP))
				{
					WorldsManager.worlds[1].GetComponent<World>().removeMonolith(gameObject);
				}
				WorldsManager.worlds[2].GetComponent<World>().addMonolith(gameObject);
				currentWorld = WORLD_DOWN;
			}
		}
	}

	IEnumerator crossFade ()
	{
		// World selection
		Material newMaterial = worldUpMaterial;
		if (nextWorld.Equals(WORLD))
		{
			nextWorld = WORLD_UP;
			newMaterial = worldUpMaterial;
		} else if (nextWorld.Equals(WORLD_UP))
		{
			nextWorld = WORLD_DOWN;
			newMaterial = worldDownMaterial;
		} else if (nextWorld.Equals(WORLD_DOWN))
		{
			nextWorld = WORLD;
			newMaterial = worldMaterial;
		}
		// Fade out
		Color currentColor = renderer.material.color;
		for (float i = renderer.material.color.a; i >= 0.0f; i = i - 0.02f)
		{
			renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, i);
			yield return new WaitForSeconds(0.00000001f);
		}
		// Material change
		renderer.material = newMaterial;
		// Fade in
		currentColor = renderer.material.color;
		for (float i = 0.0f; i <= 1.0f; i = i + 0.02f)
		{
			renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, i);
			yield return new WaitForSeconds(0.00000001f);
		}
	}

	public void setCurrentWorld (int type)
	{
		if (type == 0)
		{
			currentWorld = WORLD;
		} else if (type == 1)
		{
			currentWorld = WORLD_UP;
		} else if (type == 2)
		{
			currentWorld = WORLD_DOWN;
		}
	}
}
