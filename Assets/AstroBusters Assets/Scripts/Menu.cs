using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Button playButton;
    public Button quitButton;

	// Use this for initialization
	void Start () {
        Button play = playButton.GetComponent<Button>();
        Button quit = quitButton.GetComponent<Button>();
        play.onClick.AddListener(Onplay);
        quit.onClick.AddListener(Onquit);
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