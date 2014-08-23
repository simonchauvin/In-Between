using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit (Collider other)
	{
		// End the game
		if (other.gameObject.layer == LayerMask.NameToLayer("World"))
		{
			//TODO
		}
	}
}
