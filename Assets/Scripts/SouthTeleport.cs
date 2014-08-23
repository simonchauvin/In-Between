using UnityEngine;
using System.Collections;

public class SouthTeleport : MonoBehaviour {

	public Transform northTeleport;

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
			other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, northTeleport.position.z - 2);
		}
	}
}
