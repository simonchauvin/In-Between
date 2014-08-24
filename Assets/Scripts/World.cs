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
				yPosition = ground.transform.position.y;
			}
			monolith.transform.position = new Vector3(monolith.transform.position.x, yPosition, monolith.transform.position.z);
		}
	}

	public void addMonolith (GameObject monolith)
	{
		monoliths.Add(monolith);
		if (ground.CompareTag("World"))
		{
			monolith.transform.parent = transform;
		}
	}

	public void removeMonolith (GameObject monolith)
	{
		monoliths.Remove(monolith);
		if (ground.CompareTag("World"))
		{
			monolith.transform.parent = null;
		}
	}
}
