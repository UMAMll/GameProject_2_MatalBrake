using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Sound1,Sound2;

    public void PlaySound1()
    {
        print("Play1");
        audioSource.clip = Sound1;
        audioSource.Play();
    }

    public void PlaySound2()
    {
        audioSource.clip = Sound2;
        audioSource.Play();

    }
}
