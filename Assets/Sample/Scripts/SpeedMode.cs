using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMode : MonoBehaviour
{
    public bool Speedmode;
    public Sprite ModeOn, ModeOff;
    public Button SpeedModeButton;
    void Start()
    {
        Time.timeScale = 1;
        SpeedModeButton.onClick.AddListener(SpeedModeSet);
    }

    private void SpeedModeSet()
    {
        if(Speedmode)
        {
            Speedmode = false;
            SpeedModeButton.image.sprite = ModeOff;
            Time.timeScale = 1;
            UIManager.instance.speedmode = 1;

        }
        else
        {
            Speedmode = true;
            SpeedModeButton.image.sprite = ModeOn;
            Time.timeScale = 2;
            UIManager.instance.speedmode= 2;
        }
    }
}
