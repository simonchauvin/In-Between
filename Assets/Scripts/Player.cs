using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float TIME_TO_DISPLAY_TITLE = 8;

	// Worlds
	private GameObject world;
	private GameObject worldUp;
	private GameObject worldDown;

	// GUI
	private GameObject titleLabel;
	private GameObject endTitleLabel;
	private GameObject worldSyncLabel;
	private GameObject tutoSwitchBetweenWorldsLabel;
	private GameObject tutoConnectLabel;
	private GameObject tutoSyncLabel;
	private GameObject tutoUnbalancedWorldsLabel;
	private GameObject tutoUnificationLabel;
	private bool tutoSwitchAlreadyDisplayed;
	private bool tutoSyncAlreadyDisplayed;
	private float timeTitleDisplayed;

	// Use this for initialization
	void Start ()
	{
		// Worlds
		world = GameObject.FindGameObjectWithTag("World");
		worldUp = GameObject.FindGameObjectWithTag("WorldUp");
		worldDown = GameObject.FindGameObjectWithTag("WorldDown");

		// GUI texts
		titleLabel = GameObject.Find("TitleLabel");
		endTitleLabel = GameObject.Find("EndTitleLabel");
		worldSyncLabel = GameObject.Find("WorldSyncLabel");
		tutoSwitchBetweenWorldsLabel = GameObject.Find("TutoSwitchBetweenWorldsLabel");
		tutoSyncLabel = GameObject.Find("TutoSyncLabel");
		tutoConnectLabel = GameObject.Find("TutoConnectLabel");
		tutoUnbalancedWorldsLabel = GameObject.Find("TutoUnbalancedWorldsLabel");
		tutoUnificationLabel = GameObject.Find("TutoUnificationLabel");
		// GUI init
		endTitleLabel.SetActive(false);
		worldSyncLabel.SetActive(false);
		tutoSwitchBetweenWorldsLabel.SetActive (false);
		tutoConnectLabel.SetActive(false);
		tutoSyncLabel.SetActive(false);
		tutoUnbalancedWorldsLabel.SetActive(false);
		tutoUnificationLabel.SetActive(false);
		tutoSwitchAlreadyDisplayed = false;
		tutoSyncAlreadyDisplayed = false;

		// Ignore collisions between player and worlds (up and down)
		Physics.IgnoreLayerCollision(8, 9, true);

		// Lock and hide cursor
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	// Update is called once per frame
	void Update ()
	{
		// Title display
		if (timeTitleDisplayed > TIME_TO_DISPLAY_TITLE)
		{
			StartCoroutine("fadeTitle");
		} else {
			timeTitleDisplayed += Time.deltaTime;
		}
		if (Input.GetButton("Use"))
		{
			// Rotate the world up as the player rotates
			worldUp.transform.Rotate(0.0f, 2, 0.0f);
			// Rotate the world down as the player rotates
			worldDown.transform.Rotate(0.0f, -2, 0.0f);

			worldSyncLabel.SetActive(true);
			if (tutoSyncLabel.activeSelf)
			{
				tutoSyncAlreadyDisplayed = true;
			}
		} else {
			worldSyncLabel.SetActive(false);
		}

		// Display tuto for switching between worlds
		if (!tutoSwitchAlreadyDisplayed)
		{
			tutoSwitchBetweenWorldsLabel.SetActive(true);
			foreach (GameObject monolith in world.GetComponent<World>().monoliths)
			{
				if (!monolith.GetComponent<Monolith>().nextWorld.Equals("world"))
				{
					tutoSwitchAlreadyDisplayed = true;
					tutoSwitchBetweenWorldsLabel.SetActive(false);
				}
			}
		}

		// Display tuto for syncing worlds
		if (!tutoSyncAlreadyDisplayed && worldUp.GetComponent<World>().monoliths.Count > 0 && worldDown.GetComponent<World>().monoliths.Count > 0)
		{
			tutoSyncLabel.SetActive(true);
		} else
		{
			tutoSyncLabel.SetActive(false);
		}

		// Display unbalance alert
		if (worldUp.GetComponent<World>().unbalanced || worldUp.GetComponent<World>().unbalanced)
		{
			Debug.Log("lkshdfkjsdhfk ");
			tutoUnbalancedWorldsLabel.SetActive(true);
		} else
		{
			tutoUnbalancedWorldsLabel.SetActive(false);
		}
	}

	IEnumerator fadeTitle ()
	{
		for (float i = 1.0f; i >= 0.0f; i = i - 0.005f)
		{
			titleLabel.guiText.material.color = new Color(titleLabel.guiText.material.color.r, titleLabel.guiText.material.color.g, titleLabel.guiText.material.color.b, i);
			yield return new WaitForSeconds(0.0000000001f);
		}
	}
}
