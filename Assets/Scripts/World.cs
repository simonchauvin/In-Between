using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public bool unbalanced { get; set; }

	private GameObject ground;
	public List<GameObject> monoliths { get; private set; }

	// Use this for initialization
	void Awake () {
		monoliths = new List<GameObject>();
		ground = GetComponent<Transform>().gameObject;
		unbalanced = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Keep the monoliths attached
		foreach (GameObject monolith in monoliths)
		{
			float yPosition = ground.transform.position.y;
			if (ground.CompareTag("WorldUp"))
			{
				yPosition = ground.transform.position.y - 5f;
			} else if (ground.CompareTag("WorldDown"))
			{
				yPosition = ground.transform.position.y + 5f;
			}
			monolith.transform.position = new Vector3(monolith.transform.position.x, yPosition, monolith.transform.position.z);
		}

		// Augmment transparency as the world is coming together
		int syncedMonoliths = 0;
		foreach (GameObject monolith in WorldsManager.allMonoliths)
		{
			if (monolith.GetComponent<Monolith>().synchronized)
			{
				syncedMonoliths++;
			}
		}
		if (CompareTag("World"))
		{
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.6f + ((float)syncedMonoliths / WorldsManager.allMonoliths.Length) * 0.4f);
		} else
		{
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.8f - ((float)syncedMonoliths / WorldsManager.allMonoliths.Length) * 0.8f);
		}
	}

	public void addMonolith (GameObject monolith)
	{
		monoliths.Add(monolith);
		if (ground.CompareTag("WorldUp") || ground.CompareTag("WorldDown"))
		{
			monolith.transform.parent = transform;
		}
	}

	public void removeMonolith (GameObject monolith)
	{
		monoliths.Remove(monolith);
		if (ground.CompareTag("WorldUp") || ground.CompareTag("WorldDown"))
		{
			monolith.transform.parent = null;
		}
	}
}
