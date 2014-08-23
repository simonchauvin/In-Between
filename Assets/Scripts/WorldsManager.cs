using UnityEngine;
using System.Collections;

public class WorldsManager : MonoBehaviour {

	public static GameObject[] worlds;
	public static GameObject[] allMonoliths;

	// Use this for initialization
	void Start () {
		worlds = new GameObject[3];
		worlds[0] = GameObject.FindGameObjectWithTag("World");
		worlds[1] = GameObject.FindGameObjectWithTag("WorldUp");
		worlds[2] = GameObject.FindGameObjectWithTag("WorldDown");

		allMonoliths = GameObject.FindGameObjectsWithTag("Monolith");
		for (int i = 0; i < allMonoliths.Length; i++)
		{
			worlds[0].GetComponent<World>().addMonolith(allMonoliths[i]);
			allMonoliths[i].GetComponent<Monolith>().setCurrentWorld(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
