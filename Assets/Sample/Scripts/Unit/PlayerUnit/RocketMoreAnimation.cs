using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoreAnimation : MonoBehaviour
{
    public RocketUnitScript unit;
    public ParticleSystem fireGun;
    public void IsPlayIdleAnimation()
    {
        unit.moreGameobj.SetActive(false);
        unit.idleGameobj.SetActive(true);
    }

    public void playParticleShot()
    {
        fireGun.Play();
    }
}
