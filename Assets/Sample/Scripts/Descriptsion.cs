using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Descriptsion : MonoBehaviour
{
    [TextArea]public string descriptsion;
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        print("Over");
        UIManager.Instance.IsshowFollowupUI = true;
        UIManager.Instance.UiFollowUpMouse(descriptsion);

    }
    private void OnMouseExit()
    {
        UIManager.Instance.IsshowFollowupUI = false;

    }
}
