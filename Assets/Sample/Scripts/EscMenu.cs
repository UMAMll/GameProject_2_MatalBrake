using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);

    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void HomeLoad()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
