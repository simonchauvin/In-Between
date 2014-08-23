using UnityEngine;
using System.Collections;

public class WestTeleport : MonoBehaviour {

	public Transform eastTeleport;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		// Teleport player to the other side
		if (other.CompareTag("Player"))
		{
			other.transform.position = new Vector3(eastTeleport.transform.position.x - 2, other.transform.position.y, other.transform.position.z);
		}
	}
}
