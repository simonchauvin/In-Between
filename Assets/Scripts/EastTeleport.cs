using UnityEngine;
using System.Collections;

public class EastTeleport : MonoBehaviour {

	public Transform westTeleport;

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
			other.transform.position = new Vector3(westTeleport.transform.position.x + 2, other.transform.position.y, other.transform.position.z);
		}
	}
}
