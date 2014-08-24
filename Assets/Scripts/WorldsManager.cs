using UnityEngine;
using System.Collections;

public class WorldsManager : MonoBehaviour {
	public Vector3 WORLD_STARTING_POSITION = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 WORLD_UP_STARTING_POSITION = new Vector3(0.0f, 40.0f, 0.0f);
	public Vector3 WORLD_UP_END_POSITION = new Vector3(0.0f, 5.0f, 0.0f);
	public Vector3 WORLD_DOWN_STARTING_POSITION = new Vector3(0.0f, -40.0f, 0.0f);
	public Vector3 WORLD_DOWN_END_POSITION = new Vector3(0.0f, -5.0f, 0.0f);
	
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
		// World control
		if (Input.GetMouseButton (0) && !worlds[1].GetComponent<World>().unbalanced)
		{
			worlds[1].transform.position = Vector3.MoveTowards(worlds[1].transform.position, WORLD_UP_END_POSITION, 40.0f * Time.deltaTime);
		} else if (!worlds[1].GetComponent<World>().unbalanced)
		{
			worlds[1].transform.position = Vector3.MoveTowards(worlds[1].transform.position, WORLD_UP_STARTING_POSITION, 40.0f * Time.deltaTime);
		}
		
		if (Input.GetMouseButton (1) && !worlds[2].GetComponent<World>().unbalanced)
		{
			worlds[2].transform.position = Vector3.MoveTowards(worlds[2].transform.position, WORLD_DOWN_END_POSITION, 40.0f * Time.deltaTime);
		} else if (!worlds[2].GetComponent<World>().unbalanced)
		{
			worlds[2].transform.position = Vector3.MoveTowards(worlds[2].transform.position, WORLD_DOWN_STARTING_POSITION, 40.0f * Time.deltaTime);
		}

		int worldUpMonolithsNumber = worlds[1].GetComponent<World>().monoliths.Count,
			worldDownMonolithsNumber = worlds[2].GetComponent<World>().monoliths.Count;
		// Too much monoliths on the world up
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
