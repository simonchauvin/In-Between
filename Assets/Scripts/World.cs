using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public bool unbalanced { get; private set;}

	private GameObject ground;
	private List<GameObject> monoliths;
	private GameObject deathZone;

	// Use this for initialization
	void Awake () {
		monoliths = new List<GameObject>();
		ground = GetComponent<Transform>().gameObject;
		deathZone = GameObject.FindGameObjectWithTag("DeathZone");
		unbalanced = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Keep the monoliths attached
		foreach (GameObject monolith in monoliths)
		{
			float yPosition = ground.transform.position.y + monolith.transform.localScale.y;
			if (ground.CompareTag("WorldUp"))
			{
				yPosition = ground.transform.position.y - monolith.transform.localScale.y;
			}
			monolith.transform.position = new Vector3(monolith.transform.position.x, yPosition, monolith.transform.position.z);
		}
		// World monoliths balance
		/*if (!ground.CompareTag("World") && monoliths.Count > WorldsManager.allMonoliths.Length / 2) {
			Vector3 targetPosition = new Vector3(deathZone.transform.position.x, deathZone.transform.position.y + deathZone.transform.localScale.y + 10, deathZone.transform.position.z);
			if (ground.CompareTag("WorldDown"))
			{
				targetPosition = new Vector3(deathZone.transform.position.x, deathZone.transform.position.y - deathZone.transform.localScale.y - 10, deathZone.transform.position.z);
			}
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, 40.0f * Time.deltaTime);
			unbalanced = true;
		}*/
	}

	public void addMonolith (GameObject monolith)
	{
		monoliths.Add(monolith);
	}

	public void removeMonolith (GameObject monolith)
	{
		monoliths.Remove(monolith);
	}
}
