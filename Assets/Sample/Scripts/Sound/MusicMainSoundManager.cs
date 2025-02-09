using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMainSoundManager : MonoBehaviour
{
    public static MusicMainSoundManager instance {  get; private set; }

    public AudioSource audiosourch;
    public bool Inlevel;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);

            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!Inlevel)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
