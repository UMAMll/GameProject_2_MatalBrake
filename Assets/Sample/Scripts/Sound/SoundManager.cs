using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Walk,Hurt,Dead,PowerUp,MachineGunShot,ShotgunShot,RifleShot,AutoGunShot,Explosion,GameWin,GameLose;

    public void PlayWalkSound()
    {
        print("Play1");
        audioSource.clip = Walk;
        audioSource.Play();
    }
    public void StopSoundLoop()
    {
        audioSource.Stop();
    }
    
    public void HurtSound()
    {
        audioSource.clip = Hurt;
        audioSource.Play();
    }
    
    public void DeadSound()
    {
        audioSource.clip = Dead;
        audioSource.Play();
    }
    public void PowerUpSound()
    {
        audioSource.clip = PowerUp;
        audioSource.Play();
    }
    public void MachineGunShotSound()
    {
        audioSource.clip = MachineGunShot;
        audioSource.Play();
    }

    public void ShotgunShotSound()
    {
        audioSource.clip = ShotgunShot;
        audioSource.Play();
    }

    public void AutoGunShotSound()
    {
        audioSource.clip = AutoGunShot;
        audioSource.Play();
    }

    public void RifleShotSound()
    {
        audioSource.clip = RifleShot;
        audioSource.Play();
    }

    public void ExplosionSound()
    {
        audioSource.clip = Explosion;
        audioSource.Play();
    }

    public void GameWinSound()
    {
        audioSource.clip = GameWin;
        audioSource.Play();
    }

    public void GameLoseSound()
    {
        audioSource.clip = GameLose;
        audioSource.Play();
    }
}
