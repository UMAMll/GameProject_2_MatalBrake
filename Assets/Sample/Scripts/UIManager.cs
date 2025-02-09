using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Turn;
    public GameObject ProfilePanel;
    public Image Picture;
    public TextMeshProUGUI nametext;
    public GameObject ActionpointPanel;
    public Image[] Heart;
    public Sprite fullhealth;
    public Sprite emptyhealth;

    public Image[] StatusIcon;
    public Sprite LeaderIcon;
    public Sprite CMErrorIcon;

    public GameObject leaderpanel;
    public Image leader;

    public bool IsShowProfile;
    public bool IsshowFollowupUI;

    public GameObject InGameCanvas;
    public bool Showmode;
    public GameObject WinCanvas, LoseCanvas;
    public Sprite fullstar, nullstar;
    public Image[] star;

    public GameObject MenuCanvas;
    public bool Menushow;
    public float speedmode;

    public static UIManager instance;
    
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("UIManager");
                    instance = singletonObject.AddComponent<UIManager>();
                }
            }

            return instance;
        }
    }
    void Start()
    {
        MenuCanvas.SetActive(false);
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        InGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(Menushow)
            {
                MenuCanvas.SetActive(false);
                Menushow = false;
                Time.timeScale = speedmode;
            }
            else if(!Menushow)
            {
                MenuCanvas.SetActive(true) ;
                Time.timeScale = 0;
                Menushow = true;
            }

        }
        if (!IsShowProfile)
        {
            ProfilePanel.SetActive(false);
        }

        if (TurnManager.Instance.IsStartGame)
        {
            leaderpanel.SetActive(false);
            if (!Showmode)
            {
                InGameCanvas.SetActive(true);
            }
        }

        if (!TurnManager.Instance.IsStartGame)
        {
            InGameCanvas.SetActive(false );
            TurnManager.Instance.RemoveThisTurn();
        }
    }
    public void SetLeader(Sprite img)
    {
        leaderpanel.SetActive(true);
        leader.sprite = img;
    }
    public void SetProfilePanel(string name, Sprite image, int maxhp, int curranthp,int statusCount,string status)
    {
        IsShowProfile = true;
        ProfilePanel.SetActive(true);
        Picture.sprite = image;
        nametext.text = name;

        //Hp
        for (int i = 0; i < Heart.Length; i++)
        {
            if (i < curranthp)
            {
                Heart[i].sprite = fullhealth;
            }
            else
            {
                Heart[i].sprite = emptyhealth;
            }

            if (i < maxhp)
            {
                Heart[i].enabled = true;
            }
            else
            {
                Heart[i].enabled = false;
            }
        }

        //status
        if(status == "")
        {
            for (int i = 0; i < StatusIcon.Length; i++)
            {
                StatusIcon[i].enabled = false;
            }
        }
        if(status != "")
        {
            for (int i = 0; i < StatusIcon.Length; i++)
            {
                StatusIcon[i].enabled = false;
            }
            for (int i = 0; i < StatusIcon.Length; i++)
            {
                if (i < statusCount)
                {
                    StatusIcon[i].enabled = true;
                    if (status == "Leader")
                    {
                        StatusIcon[i].sprite = LeaderIcon;
                    }
                    if (status == "CMError")
                    {
                        StatusIcon[i].sprite = CMErrorIcon;
                    }
                }
                else if (i > statusCount)
                {
                    StatusIcon[i].enabled=false;
                }
            }
            
        }
            
           
        
    }
    public void UpDateUITurn(int turn)
    {
        Turn.text = turn.ToString();
    }
}
