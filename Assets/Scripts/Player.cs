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
	private GameObject lostTitleLabel;
	private GameObject retryLabel;
	private GameObject worldSyncLabel;
	private GameObject syncConfirmLabel;
	private GameObject tutoSwitchBetweenWorldsLabel;
	private GameObject tutoConnectLabel;
	private GameObject tutoSyncLabel;
	private GameObject tutoUnbalancedWorldsLabel;
	private bool tutoSwitchAlreadyDisplayed;
	private bool tutoConnectAlreadyDisplayed;
	private bool tutoSyncAlreadyDisplayed;
	private float timeTitleDisplayed;
	private float timeBeforeHidingConfirmLabel;
	// When two monoliths are confirmed to be synced
	public bool syncConfirmed { get; set; }
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
		lostTitleLabel = GameObject.Find("LostTitleLabel");
		retryLabel = GameObject.Find("RetryLabel");
		worldSyncLabel = GameObject.Find("WorldSyncLabel");
		syncConfirmLabel = GameObject.Find("SyncConfirmLabel");
		tutoSwitchBetweenWorldsLabel = GameObject.Find("TutoSwitchBetweenWorldsLabel");
		tutoSyncLabel = GameObject.Find("TutoSyncLabel");
		tutoConnectLabel = GameObject.Find("TutoConnectLabel");
		tutoUnbalancedWorldsLabel = GameObject.Find("TutoUnbalancedWorldsLabel");
		// GUI init
		endTitleLabel.SetActive(false);
		lostTitleLabel.SetActive(false);
		retryLabel.SetActive(false);
		worldSyncLabel.SetActive(false);
		syncConfirmLabel.SetActive(false);
		tutoSwitchBetweenWorldsLabel.SetActive(false);
		tutoConnectLabel.SetActive(false);
		tutoSyncLabel.SetActive(false);
		tutoUnbalancedWorldsLabel.SetActive(false);
		tutoSwitchAlreadyDisplayed = true;
		tutoConnectAlreadyDisplayed = false;
		tutoSyncAlreadyDisplayed = false;

		timeBeforeHidingConfirmLabel = 0.0f;

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
		if (titleLabel.activeSelf)
		{
			if (timeTitleDisplayed > TIME_TO_DISPLAY_TITLE)
			{
				StartCoroutine("fadeTitle");
			} else {
				timeTitleDisplayed += Time.deltaTime;
			}
		}

		// Rotate the world up as the player rotates
		if (Input.GetButton("Use1"))
		{
			worldUp.transform.Rotate(0.0f, 2, 0.0f);

			worldSyncLabel.SetActive(true);
			if (tutoSyncLabel.activeSelf)
			{
				tutoSyncAlreadyDisplayed = true;
			}
		}

		// Rotate the world down as the player rotates
		if (Input.GetButton("Use2"))
		{
			worldDown.transform.Rotate(0.0f, -2, 0.0f);
			
			worldSyncLabel.SetActive(true);
			if (tutoSyncLabel.activeSelf)
			{
				tutoSyncAlreadyDisplayed = true;
			}
		}

		// If not synchronizing hide label
		if (!Input.GetButton("Use1") && !Input.GetButton("Use2"))
		{
			worldSyncLabel.SetActive(false);
		}

		// Display tuto for switching monoliths colors
		if (!tutoSwitchAlreadyDisplayed)
		{
			if (world.GetComponent<World>().monoliths.Count == WorldsManager.allMonoliths.Length)
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

			// Show connect tuto
			if (!tutoConnectAlreadyDisplayed && !tutoSwitchBetweenWorldsLabel.activeSelf)
			{
				tutoConnectLabel.SetActive(true);
			}
		}

		// Hide tuto for connecting monoliths to other worlds
		if (world.GetComponent<World>().monoliths.Count < WorldsManager.allMonoliths.Length)
		{
			tutoConnectLabel.SetActive(false);
			tutoConnectAlreadyDisplayed = true;
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
		if (worldUp.GetComponent<World>().unbalanced || worldDown.GetComponent<World>().unbalanced)
		{
			tutoUnbalancedWorldsLabel.SetActive(true);
		} else
		{
			tutoUnbalancedWorldsLabel.SetActive(false);
		}

		// Show partial sync conformation alert
		if (syncConfirmed && !syncConfirmLabel.activeSelf)
		{
			syncConfirmLabel.SetActive(true);
			syncConfirmed = false;
		}

		// Time before hiding the partial sync confirmation alert
		if (syncConfirmLabel.activeSelf)
		{
			if (timeBeforeHidingConfirmLabel > 2.0f)
			{
				timeBeforeHidingConfirmLabel = 0.0f;
				syncConfirmLabel.SetActive(false);
			} else {
				timeBeforeHidingConfirmLabel += Time.deltaTime;
			}
		}

		// World final synchronization: win the game
		if (world.GetComponent<World>().monoliths.Count == WorldsManager.allMonoliths.Length)
		{
			bool areWorldsSynchronized = true;
			foreach (GameObject monolith in world.GetComponent<World>().monoliths)
			{
				if (!monolith.GetComponent<Monolith>().synchronized)
				{
					areWorldsSynchronized = false;
				}
			}
			if (areWorldsSynchronized)
			{
				endTitleLabel.SetActive(true);
				retryLabel.SetActive(true);
			}
		}

		// Replay
		if (endTitleLabel.activeSelf && Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel ("Game");
		}

		// Game over if a world is beyond synchronization
		/*if (worldUp.GetComponent<World>().unbalanced && worldUp || (worldDown.GetComponent<World>().unbalanced && worldDown.getComponent<World>().monoliths.Count > WorldsManager.allMonoliths.length - 1))
		{
			// TODO
		}*/
		
	}
	
	IEnumerator fadeTitle ()
	{
		for (float i = 1.0f; i >= 0.0f; i = i - 0.005f)
		{
			titleLabel.guiText.material.color = new Color(titleLabel.guiText.material.color.r, titleLabel.guiText.material.color.g, titleLabel.guiText.material.color.b, i);
			yield return new WaitForSeconds(0.0000000001f);
		}
		titleLabel.SetActive(false);
		tutoSwitchAlreadyDisplayed = false;
	}
}
