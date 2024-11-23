using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUPUI : MonoBehaviour
{
    public SoundManager SoundManager;

    private void Update()
    {
        if (SoundManager != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SoundManager.PlaySound1();
            }
        }
    }

}
