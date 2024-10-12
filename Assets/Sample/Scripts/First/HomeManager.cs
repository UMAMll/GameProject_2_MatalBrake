using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    int SaveGame;
    public GameObject UISetNewGame;
    public GameObject UISetContinue;

    private void Start()
    {
        SaveGame = PlayerPrefs.GetInt("NewGame");
    }
    public void ContinuneGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("NewGame", 1);
        SaveGame = PlayerPrefs.GetInt("NewGame");
        SceneManager.LoadScene("LevelSelect");
    }
    public void CloseGame()
    {
        Application.Quit();

    }
    private void Update()
    {
        if(SaveGame == 1)
        {
            UISetNewGame.SetActive(false); 
            UISetContinue.SetActive(true);
        }
        else
        {
            UISetNewGame.SetActive(true);
            UISetContinue.SetActive(false);
        }
    }
}
