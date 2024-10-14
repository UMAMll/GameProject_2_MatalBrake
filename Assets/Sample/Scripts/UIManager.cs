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
    public RectTransform uiDescription;
    public TextMeshProUGUI descriptiontext;

    public GameObject leaderpanel;
    public TextMeshProUGUI leader;

    public bool IsShowProfile;
    public bool IsshowFollowupUI;

    public GameObject InGameCanvas;
    public GameObject WinCanvas, LoseCanvas;
    public static UIManager instance;
    
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                // If no instance exists, find one in the scene or create a new one
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    // If no instance exists in the scene,u7 create a new GameObject and attach the singleton script
                    GameObject singletonObject = new GameObject("UIManager");
                    instance = singletonObject.AddComponent<UIManager>();
                }
            }

            return instance;
        }
    }
    void Start()
    {
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        InGameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsShowProfile)
        {
            ProfilePanel.SetActive(false);
        }

        if (!IsshowFollowupUI)
        {
            uiDescription.gameObject.SetActive(false);
        }

        if (TurnManager.Instance.IsStartGame)
        {
            leaderpanel.SetActive(false);
            InGameCanvas.SetActive(true);
        }

        if (!TurnManager.Instance.IsStartGame)
        {
            InGameCanvas.SetActive(false );
            TurnManager.Instance.RemoveThisTurn();
        }
    }
    public void SetLeader(string name)
    {
        leaderpanel.SetActive(true);
        leader.text = name;
    }
    public void UiFollowUpMouse(string text)
    {
        uiDescription.gameObject.SetActive(true);
        Vector2 mouseposition = Input.mousePosition;
        uiDescription.position = new Vector2(mouseposition.x + (uiDescription.sizeDelta.x/2),mouseposition.y + (uiDescription.sizeDelta.y / 2));
        descriptiontext.text = text;
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
