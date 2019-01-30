using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public GUISkin skin;
	//public GUISkin skin1;
	public Color saveWarning;

	void OnGUI()
	{
		GUI.skin = skin;

		GUI.Label (new Rect (10,10,400,75), "Maze Game");  //Title

		//Not sure if there`s any need of Continue button (the commented part) if there`s a World Map feature to select the Level you want

		/* if (PlayerPrefs.GetInt ("Level Completed") > 1  ) 
		{
			if(GUI.Button (new Rect (10, 100, 100, 45), "Continue")) 
			{
				SceneManager.LoadScene (PlayerPrefs.GetInt("Level Completed"));    
			}
		}
        */
	

		//skin1.GetStyle ("Warning").normal.textColor = saveWarning ; 
		saveWarning = Color.HSVToRGB (0f, 0.7f, 0.8f);
		skin.GetStyle ("Warning").normal.textColor = saveWarning;

		if(PlayerPrefs.GetInt ("Level Completed") > 1)     // If there is a saved game Show a message to warn the player
		{
			GUI.Label (new Rect(10, 45, 300, 75),  "You have a saved game!", skin.GetStyle ("Warning"));
			//GUI.Label (new Rect(10, 45, 300, 75),  "You have a saved game!", skin1.GetStyle ("Warning"));
			//GUI.Label (new Rect (10, 45, 300, 75), "You have a saved game!", skin.GetStyle ("Warning"));
		}

		if(GUI.Button (new Rect (10, 100, 100, 45), "World Map"))  //Loadin the World Map
		{
			SceneManager.LoadScene ("World Select");    
		}

		if(GUI.Button (new Rect (10, 155, 100, 45), "New Game"))  //Starting a new game; Maybe add message box to eliminate missclicing the button
		{
			PlayerPrefs.SetInt ("Level Completed", 1); 
			SceneManager.LoadScene (1);
		}

		if(GUI.Button (new Rect (10,210, 100, 45), "Quit"))   //Quit the game; Add Message Box to eliminate quitting by mistake
		{
			Application.Quit ();
		}





	}

}
