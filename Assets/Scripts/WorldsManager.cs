using UnityEngine;
using System.Collections;

public class WorldsManager : MonoBehaviour {
	public static GameObject[] worlds;
	public static GameObject[] allMonoliths;
	private GameObject deathZone;

	// Use this for initialization
	void Start () {
		worlds = new GameObject[3];
		worlds[0] = GameObject.FindGameObjectWithTag("World");
		worlds[1] = GameObject.FindGameObjectWithTag("WorldUp");
		worlds[2] = GameObject.FindGameObjectWithTag("WorldDown");
		deathZone = GameObject.FindGameObjectWithTag("DeathZone");

		allMonoliths = GameObject.FindGameObjectsWithTag("Monolith");
		for (int i = 0; i < allMonoliths.Length; i++)
		{
			worlds[0].GetComponent<World>().addMonolith(allMonoliths[i]);
			allMonoliths[i].GetComponent<Monolith>().setCurrentWorld(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Too much monoliths on the world up
		int worldUpMonolithsNumber = worlds[1].GetComponent<World>().monoliths.Count,
			worldDownMonolithsNumber = worlds[2].GetComponent<World>().monoliths.Count;
		if (worldUpMonolithsNumber - worldDownMonolithsNumber > 2)
		{
			Vector3 targetPosition = new Vector3(deathZone.transform.position.x, deathZone.transform.position.y + deathZone.transform.localScale.y + 10, deathZone.transform.position.z);
			worlds[1].transform.position = Vector3.MoveTowards(worlds[1].transform.position, targetPosition, 40.0f * Time.deltaTime);
			worlds[1].GetComponent<World>().unbalanced = true;
		} else {
			worlds[1].GetComponent<World>().unbalanced = false;
		}

		// Too much monoliths on the world down
		if (worldDownMonolithsNumber - worldUpMonolithsNumber > 2)
		{
			Vector3 targetPosition = new Vector3(deathZone.transform.position.x, deathZone.transform.position.y - deathZone.transform.localScale.y - 10, deathZone.transform.position.z);
			worlds[2].transform.position = Vector3.MoveTowards(worlds[2].transform.position, targetPosition, 40.0f * Time.deltaTime);
			worlds[2].GetComponent<World>().unbalanced = true;
		} else {
			worlds[2].GetComponent<World>().unbalanced = false;
		}
	}
}
