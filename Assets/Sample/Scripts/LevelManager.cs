using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Sprite UnlockSpritelevel1, LockSprite, ClearedSpritelevel1, UnlockSpritelevel2, ClearedSpritelevel2, UnlockSpritelevel3, ClearedSpritelevel3, UnlockSpritelevel4, ClearedSpritelevel4, UnlockSpritelevel5, ClearedSpritelevel5, UnlockSpritelevel6, ClearedSpritelevel6;
    public Button Level1button, Level2button, Level3button, Level4button, Level5button, Level6button;
    void Start()
    {
        CheckLevel();
    }

    public void CheckLevel()
    {
        int level1Status = PlayerPrefs.GetInt("Level1");
        int level2Status = PlayerPrefs.GetInt("Level2");
        int level3Status = PlayerPrefs.GetInt("Level3");
        int level4Status = PlayerPrefs.GetInt("Level4");
        int level5Status = PlayerPrefs.GetInt("Level5");
        int level6Status = PlayerPrefs.GetInt("Level6");

        // 0 = lock / 1 = unlock / 2 = cleared
        // level 1
        if(level1Status == 0)
        {
            Level1button.image.sprite = UnlockSpritelevel1;
            Level1button.interactable = true;
        }
        if (level1Status == 1)
        {
            Level1button.image.sprite = UnlockSpritelevel1;
            Level1button.interactable = true;
        }
        else if (level1Status == 2)
        {
            Level1button.image.sprite = ClearedSpritelevel1;
            Level1button.interactable = true;
        }
        //level 2
        if (level2Status == 0)
        {
            Level2button.image.sprite = LockSprite;
            Level2button.interactable = false;
        }
        else if (level2Status == 1)
        {
            Level2button.image.sprite = UnlockSpritelevel2;
            Level2button.interactable = true;
        }
        else if (level2Status == 2)
        {
            Level2button.image.sprite = ClearedSpritelevel2;
            Level2button.interactable = true;
        }
        //level 3
        if (level3Status == 0)
        {
            Level3button.image.sprite = LockSprite;
            Level3button.interactable = false;
        }
        else if (level3Status == 1)
        {
            Level3button.image.sprite = UnlockSpritelevel3;
            Level3button.interactable = true;
        }
        else if (level3Status == 2)
        {
            Level3button.image.sprite = ClearedSpritelevel3;
            Level3button.interactable = true;
        }
        //level 4
        if (level4Status == 0)
        {
            Level4button.image.sprite = LockSprite;
            Level4button.interactable = false;
        }
        else if (level4Status == 1)
        {
            Level4button.image.sprite = UnlockSpritelevel4;
            Level4button.interactable = true;
        }
        else if (level4Status == 2)
        {
            Level4button.image.sprite = ClearedSpritelevel4;
            Level4button.interactable = true;
        }
        //level 5
        if (level5Status == 0)
        {
            Level5button.image.sprite = LockSprite;
            Level5button.interactable = false;
        }
        else if (level5Status == 1)
        {
            Level5button.image.sprite = UnlockSpritelevel5;
            Level5button.interactable = true;
        }
        else if (level5Status == 2)
        {
            Level5button.image.sprite = ClearedSpritelevel5;
            Level5button.interactable = true;
        }
        //level 6
        if (level6Status == 0)
        {
            Level6button.image.sprite = LockSprite;
            Level6button.interactable = false;
        }
        else if (level6Status == 1)
        {
            Level6button.image.sprite = UnlockSpritelevel6;
            Level6button.interactable = true;
        }
        else if (level6Status == 2)
        {
            Level6button.image.sprite = ClearedSpritelevel6;
            Level6button.interactable = true;
        }
    }
}
