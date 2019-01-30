using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour 
{
	public int levelToLoad;
	private string loadPrompt;

	public GameObject padLock;

	private bool inRange;
	private bool canLoadLevel;

	private int completedLevel;


	void Start ()
	{
		completedLevel = PlayerPrefs.GetInt ("Level Completed");
		canLoadLevel = levelToLoad <= completedLevel ? true : false;

		if (!canLoadLevel) 
		{
			Instantiate (padLock, new Vector3(transform.position.x + 2f, 0f,transform.position.z), Quaternion.Euler (new Vector3(-90f,0f,0f)) );  //rotating that damn padlock
		}
	}

	void Update()
	{
		if(canLoadLevel && Input.GetButtonDown ("Action") && inRange)
		{
			SceneManager.LoadScene ("Level" + levelToLoad.ToString ());  //maybe change Level0 to only Level if levels are more than 9 - DONE
		}
	}

	void OnTriggerStay (Collider other)
	{
		inRange = true;
		if (canLoadLevel) {
			loadPrompt = "[E] to load level " + levelToLoad.ToString ();
		} else 
		{
			loadPrompt = "Level " + levelToLoad.ToString () + " is locked.";
		}
	}

	void OnTriggerExit ()
	{
		inRange = false;
		loadPrompt = "";
	}

	void OnGUI()
	{
		GUI.Label (new Rect(30,Screen.height * .9f, 200,40),loadPrompt);
	}




}
