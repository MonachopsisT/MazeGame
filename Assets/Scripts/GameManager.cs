using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	//Count
		//Score
	public int currentScore;
	public int highScore;
		//Token
	public int tokenCount;
	private int totalTokenCount;
		//Levels
	public int currenLevel = 1 ; //keeps track of the index of the levels
	public int unlockLevel;
	public int levelNumber = 4;  // Index of last level                    
	
	//GUI Skin
	public GUISkin skin;

	//Timer variables
	public Rect timerRect;
	public Color warningColorTimer;
	public Color defaultColorTimer;
	public float starTime;
	private string currentTime;

	//Referances
	public GameObject tokenParent;

	//Win Screen
	private bool showWinScreen = false;
	public int winScreenWidth, winScreenHeight;
	private bool completed = false;

	void Update()
	{
		if(!completed)
		{
			starTime -= Time.deltaTime;
			currentTime = string.Format ("{0:0.0}", starTime);
			if (starTime <= 0)
			{
				starTime = 0;
				SceneManager.LoadScene("Main_Menu");
			}
		}


	}
		
	void Start()
	{
		totalTokenCount = tokenParent.transform.childCount;
		
		if (PlayerPrefs.GetInt ("Level Completed") > 0 && PlayerPrefs.GetInt ("Level Completed") < 4)
		{
			currenLevel = PlayerPrefs.GetInt ("Level Completed");
		} 
		else
		{
			currenLevel = 1;
		}
	
	}
		
	public void CompleteLevel()
	{
		showWinScreen = true;
		completed = true;
	}

	void LoadNextLevel ()
	{

		Time.timeScale = 1f;
		//for this to work levels should be put in order;main menu counts as level with index = 0;
		if (currenLevel <= levelNumber) 
		{  
			currenLevel += 1;
			//print (currenLevel);

			SaveGame ();
			SceneManager.LoadScene (currenLevel);   //Loads next scene;
		} 
		else 
		{
			print ("You Win!");
		}
	}
	 
	void SaveGame ()
	{
		PlayerPrefs.SetInt ("Level Completed", currenLevel);
		PlayerPrefs.SetInt ("Level " + currenLevel.ToString () + " score", currentScore);
	}

	public void AddToken()
	{
		tokenCount += 1;
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if (starTime < 5f)
		{
			skin.GetStyle ("Timer").normal.textColor = warningColorTimer;       //Changes the color of the timer when time reaches 5 or less;
		}
		else 
		{
			skin.GetStyle ("Timer").normal.textColor = defaultColorTimer;   	//Returns the default color of the timer;
		}

		GUI.Label (timerRect, currentTime, skin.GetStyle ("Timer"));

		GUI.Label (new Rect(45,100,200,200),tokenCount.ToString () + " / " +totalTokenCount.ToString ());  

		if (showWinScreen) 
		{
			Rect winScreenRect = new Rect (Screen.width / 2 - (Screen.width * .5f / 2), Screen.height / 2 - (Screen.height * .5f / 2), Screen.width * .5f, Screen.height * .5f);
			GUI.Box (winScreenRect, "Yeah"); //Ummm maaaaybe I need better text for the win screen

			int gameTime = (int)starTime;
			currentScore = tokenCount * gameTime; //Maybe change that but no idea how

			if (GUI.Button (new Rect (winScreenRect.x + winScreenRect.width - 170, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Continue"))
			{
				LoadNextLevel ();
			}

			if (GUI.Button (new Rect (winScreenRect.x +20, winScreenRect.y + winScreenRect.height - 60, 100, 40), "Quit"))
			{
				SceneManager.LoadScene ("Main_Menu");
				Time.timeScale = 1f;
			}

			GUI.Label (new Rect(winScreenRect.x + 20, winScreenRect.y + 40, 300, 50), currentScore.ToString () + " Score");
			GUI.Label (new Rect(winScreenRect.x + 20, winScreenRect.y + 70, 300, 50), "Completed Level " + currenLevel.ToString ());
		}
	}
}
