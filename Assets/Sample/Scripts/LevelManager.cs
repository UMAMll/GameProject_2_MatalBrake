using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Sprite UnlockSpritelevel1, LockSprite, ClearedSpritelevel1, UnlockSpritelevel2, ClearedSpritelevel2, UnlockSpritelevel3, ClearedSpritelevel3, UnlockSpritelevel4, ClearedSpritelevel4, UnlockSpritelevel5, ClearedSpritelevel5, UnlockSpritelevel6, ClearedSpritelevel6;
    public Button Level1button, Level2button, Level3button, Level4button, Level5button, Level6button;

    public Sprite Fullstar, Nullstar;
    public Levelestatus[] leveldetalsarray;
    void Start()
    {
        CheckLevel();
        CheckStarLevel();
    }
    public void CheckStarLevel()
    {
        int level1Star = PlayerPrefs.GetInt("Level1Star");
        int level2Star = PlayerPrefs.GetInt("Level2Star");
        int level3Star = PlayerPrefs.GetInt("Level3Star");
        int level4Star = PlayerPrefs.GetInt("Level4Star");
        int level5Star = PlayerPrefs.GetInt("Level5Star");
        int level6Star = PlayerPrefs.GetInt("Level6Star");

        // 0 = lock / 1 = unlock / 2 = cleared
        // level 1
        if (level1Star == 0)
        {
            leveldetalsarray[0].star[0].sprite = Nullstar;
            leveldetalsarray[0].star[1].sprite = Nullstar;
            leveldetalsarray[0].star[2].sprite = Nullstar;
        }
        if (level1Star == 1)
        {
            leveldetalsarray[0].star[0].sprite = Fullstar;
            leveldetalsarray[0].star[1].sprite = Nullstar;
            leveldetalsarray[0].star[2].sprite = Nullstar;
        }
        else if (level1Star == 2)
        {
            leveldetalsarray[0].star[0].sprite = Fullstar;
            leveldetalsarray[0].star[1].sprite = Fullstar;
            leveldetalsarray[0].star[2].sprite = Nullstar;
        }
        else if (level1Star == 3)
        {
            leveldetalsarray[0].star[0].sprite = Fullstar;
            leveldetalsarray[0].star[1].sprite = Fullstar;
            leveldetalsarray[0].star[2].sprite = Fullstar;
        }
        //level 2
        if (level2Star == 0)
        {
            leveldetalsarray[1].star[0].sprite = Nullstar;
            leveldetalsarray[1].star[1].sprite = Nullstar;
            leveldetalsarray[1].star[2].sprite = Nullstar;
        }
        if (level2Star == 1)
        {
            leveldetalsarray[1].star[0].sprite = Fullstar;
            leveldetalsarray[1].star[1].sprite = Nullstar;
            leveldetalsarray[1].star[2].sprite = Nullstar;
        }
        else if (level2Star == 2)
        {
            leveldetalsarray[1].star[0].sprite = Fullstar;
            leveldetalsarray[1].star[1].sprite = Fullstar;
            leveldetalsarray[1].star[2].sprite = Nullstar;
        }
        else if (level2Star == 3)
        {
            leveldetalsarray[1].star[0].sprite = Fullstar;
            leveldetalsarray[1].star[1].sprite = Fullstar;
            leveldetalsarray[1].star[2].sprite = Fullstar;
        }
        //level 3
        if (level3Star == 0)
        {
            leveldetalsarray[2].star[0].sprite = Nullstar;
            leveldetalsarray[2].star[1].sprite = Nullstar;
            leveldetalsarray[2].star[2].sprite = Nullstar;
        }
        if (level3Star == 1)
        {
            leveldetalsarray[2].star[0].sprite = Fullstar;
            leveldetalsarray[2].star[1].sprite = Nullstar;
            leveldetalsarray[2].star[2].sprite = Nullstar;
        }
        else if (level3Star == 2)
        {
            leveldetalsarray[2].star[0].sprite = Fullstar;
            leveldetalsarray[2].star[1].sprite = Fullstar;
            leveldetalsarray[2].star[2].sprite = Nullstar;
        }
        else if (level3Star == 3)
        {
            leveldetalsarray[2].star[0].sprite = Fullstar;
            leveldetalsarray[2].star[1].sprite = Fullstar;
            leveldetalsarray[2].star[2].sprite = Fullstar;
        }
        //level 4
        if (level4Star == 0)
        {
            leveldetalsarray[3].star[0].sprite = Nullstar;
            leveldetalsarray[3].star[1].sprite = Nullstar;
            leveldetalsarray[3].star[2].sprite = Nullstar;
        }
        if (level4Star == 1)
        {
            leveldetalsarray[3].star[0].sprite = Fullstar;
            leveldetalsarray[3].star[1].sprite = Nullstar;
            leveldetalsarray[3].star[2].sprite = Nullstar;
        }
        else if (level4Star == 2)
        {
            leveldetalsarray[3].star[0].sprite = Fullstar;
            leveldetalsarray[3].star[1].sprite = Fullstar;
            leveldetalsarray[3].star[2].sprite = Nullstar;
        }
        else if (level4Star == 3)
        {
            leveldetalsarray[3].star[0].sprite = Fullstar;
            leveldetalsarray[3].star[1].sprite = Fullstar;
            leveldetalsarray[3].star[2].sprite = Fullstar;
        }
        //level 5
        if (level5Star == 0)
        {
            leveldetalsarray[4].star[0].sprite = Nullstar;
            leveldetalsarray[4].star[1].sprite = Nullstar;
            leveldetalsarray[4].star[2].sprite = Nullstar;
        }
        if (level5Star == 1)
        {
            leveldetalsarray[4].star[0].sprite = Fullstar;
            leveldetalsarray[4].star[1].sprite = Nullstar;
            leveldetalsarray[4].star[2].sprite = Nullstar;
        }
        else if (level5Star == 2)
        {
            leveldetalsarray[4].star[0].sprite = Fullstar;
            leveldetalsarray[4].star[1].sprite = Fullstar;
            leveldetalsarray[4].star[2].sprite = Nullstar;
        }
        else if (level5Star == 3)
        {
            leveldetalsarray[4].star[0].sprite = Fullstar;
            leveldetalsarray[4].star[1].sprite = Fullstar;
            leveldetalsarray[4].star[2].sprite = Fullstar;
        }
        //level 6
        if (level6Star == 0)
        {
            leveldetalsarray[5].star[0].sprite = Nullstar;
            leveldetalsarray[5].star[1].sprite = Nullstar;
            leveldetalsarray[5].star[2].sprite = Nullstar;
        }
        if (level6Star == 1)
        {
            leveldetalsarray[5].star[0].sprite = Fullstar;
            leveldetalsarray[5].star[1].sprite = Nullstar;
            leveldetalsarray[5].star[2].sprite = Nullstar;
        }
        else if (level6Star == 2)
        {
            leveldetalsarray[5].star[0].sprite = Fullstar;
            leveldetalsarray[5].star[1].sprite = Fullstar;
            leveldetalsarray[5].star[2].sprite = Nullstar;
        }
        else if (level6Star == 3)
        {
            leveldetalsarray[5].star[0].sprite = Fullstar;
            leveldetalsarray[5].star[1].sprite = Fullstar;
            leveldetalsarray[5].star[2].sprite = Fullstar;
        }
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
            leveldetalsarray[0].cleared.SetActive(false);
            Level1button.interactable = true;
        }
        else if (level1Status == 2)
        {
            Level1button.image.sprite = ClearedSpritelevel1;
            leveldetalsarray[0].cleared.SetActive(true);
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
            leveldetalsarray[1].cleared.SetActive(false);
            Level2button.interactable = true;
        }
        else if (level2Status == 2)
        {
            Level2button.image.sprite = ClearedSpritelevel2;
            leveldetalsarray[1].cleared.SetActive(true);
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
            leveldetalsarray[2].cleared.SetActive(false);
            Level3button.interactable = true;
        }
        else if (level3Status == 2)
        {
            Level3button.image.sprite = ClearedSpritelevel3;
            leveldetalsarray[2].cleared.SetActive(true);
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
            leveldetalsarray[3].cleared.SetActive(false);
            Level4button.interactable = true;
        }
        else if (level4Status == 2)
        {
            Level4button.image.sprite = ClearedSpritelevel4;
            leveldetalsarray[3].cleared.SetActive(true);
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
            leveldetalsarray[4].cleared.SetActive(false);
            Level5button.interactable = true;
        }
        else if (level5Status == 2)
        {
            Level5button.image.sprite = ClearedSpritelevel5;
            leveldetalsarray[4].cleared.SetActive(true);
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
            leveldetalsarray[5].cleared.SetActive(true);
            Level6button.interactable = true;
        }
        else if (level6Status == 2)
        {
            Level6button.image.sprite = ClearedSpritelevel6;
            leveldetalsarray[5].cleared.SetActive(true);
            Level6button.interactable = true;
        }
    }


    [System.Serializable]
    public class Levelestatus
    {
        public Image[] star;
        public GameObject cleared;
    }
}
