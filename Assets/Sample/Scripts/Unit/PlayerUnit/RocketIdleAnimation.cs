using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketIdleAnimation : MonoBehaviour
{
    public RocketUnitScript unit;
    public void IsPlayMoreAnimation()
    {
        unit.moreGameobj.SetActive(true);
        unit.idleGameobj.SetActive(false);
    }
}
