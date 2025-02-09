using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public bool Inscene;

    private void Start()
    {
        MusicMainSoundManager.instance.Inlevel = Inscene;
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
