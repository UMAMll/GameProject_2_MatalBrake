using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.AxisState;

public class SearchMode : MonoBehaviour
{
    public bool searchmode;
    public Sprite ModeOn, ModeOff;
    public Button searchModeButton;

    [Header("UI")]
    public RectTransform uipanel;
    public Image Image;
    
    public TextMeshProUGUI nametext, walkareatext;
    [Header("Skill1")]
    public TextMeshProUGUI damagetext;
    public TextMeshProUGUI damagecounttext, attackareatext, attacktargettext, cooldowntext;
    [Header("Skill2")]
    public TextMeshProUGUI damagetext2;
    public TextMeshProUGUI damagecounttext2, attackareatext2, attacktargettext2, cooldowntext2;



    private static SearchMode instance;
    public static SearchMode Instance
    {
        get
        {
            if (instance == null)
            {
                // If no instance exists, find one in the scene or create a new one
                instance = FindObjectOfType<SearchMode>();

                if (instance == null)
                {
                    // If no instance exists in the scene,u7 create a new GameObject and attach the singleton script
                    GameObject singletonObject = new GameObject("Serchmode");
                    instance = singletonObject.AddComponent<SearchMode>();
                }
            }

            return instance;
        }
    }
    void Start()
    {
        searchmode = false;
        uipanel.gameObject.SetActive(false);
        searchModeButton.onClick.AddListener(SearchModeSet);
    }

    void SearchModeSet()
    {
        if (searchmode)
        {
            searchmode = false;
            searchModeButton.image.sprite = ModeOff;

        }
        else
        {
            searchmode = true;
            searchModeButton.image.sprite = ModeOn;
        }

    }

    public void UISet1Skill(Sprite image,string name, string walkarea, string damagetype, string damage, string attackarea, string targettype, string cooldown)
    {
        Vector2 mouseposition = Input.mousePosition;
        uipanel.position = new Vector2(mouseposition.x + (uipanel.sizeDelta.x / 2), mouseposition.y + (uipanel.sizeDelta.y / 2));

        Image.sprite = image;
        nametext.text = name;
        walkareatext.text = walkarea;
        damagetext.text = damagetype;
        damagecounttext.text = damage;
        attackareatext.text = attackarea;
        attacktargettext.text = targettype;
        cooldowntext.text = cooldown;
    }

    public void UISet2Skill(string damagetype, string damage, string attackarea, string targettype, string cooldown)
    {
        damagetext2.text = damagetype;
        damagecounttext2.text = damage;
        attackareatext2.text = attackarea;
        attacktargettext2.text = targettype;
        cooldowntext2.text = cooldown;
    }
}
