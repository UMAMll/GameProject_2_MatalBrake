using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScripts : MonoBehaviour
{
    public void Missioncomplete(int level)
    {
        PlayerPrefs.SetInt("Level" + level, 2);
        PlayerPrefs.SetInt("Level" + (level+1), 1);

        SceneManager.LoadScene("LevelSelect");
    }
}
