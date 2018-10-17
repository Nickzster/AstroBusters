using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
	/* Main Menu Buttons */
    public Button playButton; //enters the game
    public Button quitButton; //exits the game
	public Button helpEnterButton; //enters the help menu
	public Button ShipMenuEnterButton; //enters the ship selection menu
	/* Common Buttons */
	public Button[] backButtons;
	/* Ship Menu Buttons */
	public Button shipSelector;
	public Button leftButton;
	public Button rightButton;
	/* Menu Game Objects */
	public GameObject mainMenu;
	public GameObject helpMenu;
	public GameObject shipMenu;
	/* Ship Menu variables */
	private int ShipMenuCounter; //Keeps track of which menu to display
	private int SelectedShip;
	public Button[] shipMenuButtons; //Keeps all of the ship buttons in an array. + Used to select which ship the player wants to play as.
	public GameObject[] shipsMenu; //Keeps track of each individual ship menu.
	public GameObject Locked;
//	public GameObject[] Menus;
//	public GameObject[] shipSelection;
    public Text highScoreText; //Displays the high score in the menu
	public Text shipSelectionText; //Displays page # in ship menu.
	bool updateShipMenu;
	bool isUnlocked;
	bool SCOREDEBUGGING;

	public int[] scoreThresholds; //50,100,200,500,1000
	// Use this for initialization
	void Start () 
	{
		//SCOREDEBUGGING = true;
		//Initalize menu buttons
        Button play = playButton.GetComponent<Button>();
        Button quit = quitButton.GetComponent<Button>();
		Button help = helpEnterButton.GetComponent<Button>();
		Button shipSelectMenu = ShipMenuEnterButton.GetComponent<Button>();
		//initalize back buttons
		Button backZero = backButtons[0].GetComponent<Button>();
		Button backOne = backButtons [1].GetComponent<Button>();
		//initalize ship menu buttons
		Button LeftButton = leftButton.GetComponent<Button>();
		Button RightButton = rightButton.GetComponent<Button> ();
		Button PreferedShip = shipSelector.GetComponent<Button>();
		Button[] ships = new Button[shipMenuButtons.Length]; //Local button object to keep track of ship buttons
		for (int i = 0; i < shipMenuButtons.Length; i++) 
		{
			ships[i] = shipMenuButtons [i].GetComponent<Button>();
		}
		//Add listeners for the buttons
        play.onClick.AddListener(Onplay);
        quit.onClick.AddListener(Onquit);
		help.onClick.AddListener(OnHelp);
		shipSelectMenu.onClick.AddListener(OnShipMenuEnter);

		backZero.onClick.AddListener(OnBack);
		backOne.onClick.AddListener (OnBack);

		LeftButton.onClick.AddListener (OnLeftButton);
		RightButton.onClick.AddListener (OnRightButton);

		PreferedShip.onClick.AddListener (OnSelectedShip);



		//Initalize the menu counter
		ShipMenuCounter = 0;

        //Post the High Score
        int highScore = PlayerPrefs.GetInt("High_Score");
        if(highScore <= 0)
        {
            highScoreText.text = "Play for a High Score!";
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }
		if (SCOREDEBUGGING) //Wipe the preferences.
		{
			PlayerPrefs.DeleteAll ();
			/*for (int i = 0; i < 6; i++)
			{
				PlayerPrefs.SetInt(("ship" + i), 1);
			} */
		}
	}

	void displayMenu()
	{
		shipsMenu [ShipMenuCounter].SetActive (true);
		shipSelectionText.text = (ShipMenuCounter + 1) + " out of " + (shipMenuButtons.Length);
		isUnlocked = true;
	}

	void Update()
	{
		if (updateShipMenu) //If the ship menu needs to be updated
		{
			//We need to determine which sub menu is displayed
			Debug.Log("ship" + ShipMenuCounter);
			Debug.Log (PlayerPrefs.GetInt ("ship" + ShipMenuCounter));
			//Then we need to show the current menu...
			if (ShipMenuCounter == 0) //If we are on ship 1, which is unlocked by default
			{  
				displayMenu();
			} 
			else if (PlayerPrefs.GetInt(("ship" + ShipMenuCounter)) != 0) //If this ship is unlocked
			{ 
				displayMenu();
			} 
			else //Finally, if the ship is not locked.
			{ 
				Locked.SetActive(true);
				shipSelectionText.text = "Dodge " + scoreThresholds[ShipMenuCounter - 1] + " to unlock.";
				isUnlocked = false;
			}
			//And then we display the text
			updateShipMenu = false;

		}
	}
	
	// Update is called once per frame
    void Onplay()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("game");
    }

    void Onquit()
    {
        Debug.Log("Exit");

        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }
	void OnHelp()
	{
		mainMenu.SetActive(false);
		helpMenu.SetActive(true);
	}
	void toMainMenu()
	{
		helpMenu.SetActive(false);
		shipMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
	void OnBack()
	{
		toMainMenu();
	}

	void OnShipMenuEnter()
	{
		mainMenu.SetActive (false);
		shipMenu.SetActive (true);
		updateShipMenu = true;
	}

	void hideMenu()
	{
		shipsMenu[ShipMenuCounter].SetActive(false);
		Locked.SetActive (false);
	}

	void OnRightButton() //If the player presses the right button
	{
		Debug.Log ("Right button pressed");
		hideMenu();
		//First, we need to hide the current menu
		if (ShipMenuCounter >= shipMenuButtons.Length - 1) //and the player is on the last ship
		{
			ShipMenuCounter = 0; //place at the beginning of the list
		} 
		else  //and the player is not on the last ship
		{
			ShipMenuCounter++; //move the list 1 space to the right
		}
		updateShipMenu = true;
		Debug.Log (ShipMenuCounter);
	}
	void OnLeftButton() //If the player presses the left button
	{
		Debug.Log ("Left Button Pressed");
		hideMenu();
		Debug.Log (ShipMenuCounter);
		if (ShipMenuCounter <= 0) //and the player is on the beginning ship
		{
			ShipMenuCounter = shipMenuButtons.Length - 1; //place at the end of the list 
		} 
		else //and the player is not on the beginning ship
		{
			ShipMenuCounter--; //move the list one space to the left
		}
		updateShipMenu = true; //and tell update to update the menu
		Debug.Log (ShipMenuCounter);
	}
	void OnSelectedShip()
	{
		if (isUnlocked) //If the ship is unlocked
		{  
			PlayerPrefs.SetInt ("selected_ship", ShipMenuCounter);
		} 
		else //If the ship is not unlocked, but the player presses select
		{
			PlayerPrefs.SetInt ("selected_ship", 0);
		}
		Debug.Log ("Player Prefered Ship: " + PlayerPrefs.GetInt("selected_ship"));
		toMainMenu();
	}


/*    void OnGUI()
    {
        //Debug.Log("ONGUI");
        if(playButton.onClick.Equals(true))
        {

        }
        if(quitButton.onClick.Equals(true))
        {

        }
    }*/
}