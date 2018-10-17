using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public Button tryagain;
    public Button gotoMenu;

    // Use this for initialization
    void Start ()
    {
        tryagain.onClick.AddListener(TryAgain);
        gotoMenu.onClick.AddListener(ReturnToMenu);
    }
    void TryAgain()
    {
        SceneManager.LoadScene("game");
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
