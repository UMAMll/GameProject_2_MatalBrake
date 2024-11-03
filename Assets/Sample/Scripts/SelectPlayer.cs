using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    public GameObject startgameObject;
    public Button startGamebutton;
    public GameObject selectCanvas;
    public GameObject selectStand;
    public GameObject[] PlayerPrefabs;
    public Button[] PlayerButton;
    public Transform spawnposition;
    public SelectionPlayerTile standpos;

    [SerializeField]public PlayerUnlockUI[] uiunlock;
    public Sprite Lockimg,unlockimg;
    public GameObject player1unlock, player2unlock, player3unlock, player4unlock, player5unlock, player6unlock, player7unlock, player8unlock;
    int Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8;
    void Start()
    {
        startgameObject.SetActive(false);
        CheckPlaterStats();
        startGamebutton.onClick.AddListener(StartGame);
        PlayerButton[0].onClick.AddListener(() => PlayerGenerate(0));
        PlayerButton[1].onClick.AddListener(() => PlayerGenerate(1));
        PlayerButton[2].onClick.AddListener(() => PlayerGenerate(2));
        PlayerButton[3].onClick.AddListener(() => PlayerGenerate(3));
        PlayerButton[4].onClick.AddListener(() => PlayerGenerate(4));
        PlayerButton[5].onClick.AddListener(() => PlayerGenerate(5));
        PlayerButton[6].onClick.AddListener(() => PlayerGenerate(6));
        PlayerButton[7].onClick.AddListener(() => PlayerGenerate(7));


    }

    private void StartGame()
    {
        Destroy(selectStand);
        TurnManager.Instance.IsStartGame = true;
        TurnManager.Instance.PlayerTurn = true;
        TurnManager.Instance.EnemyTurn = false;
        TurnManager.Instance.turn = 1;
        Destroy(startgameObject);
    }
    public void unlockUnit(int player)
    {
        int currentchip = PlayerPrefs.GetInt("Chips");
        PlayerPrefs.SetInt("Player" + player + "Status", 1);
        PlayerPrefs.SetInt("Chips", currentchip -= uiunlock[player - 1].price);
        CheckPlaterStats();
        uiunlock[player - 1].unlockbutton.SetActive(false);
    }

    public void CheckPlaterStats()
    {
        Player1 = PlayerPrefs.GetInt("Player1Status");
        Player2 = PlayerPrefs.GetInt("Player2Status");
        Player3 = PlayerPrefs.GetInt("Player3Status");
        Player4 = PlayerPrefs.GetInt("Player4Status");
        Player5 = PlayerPrefs.GetInt("Player5Status");
        Player6 = PlayerPrefs.GetInt("Player6Status");
        Player7 = PlayerPrefs.GetInt("Player7Status");
        Player8 = PlayerPrefs.GetInt("Player8Status");

        // 0 = lock 1 = unlock
        if(Player1 == 0)
        {
            PlayerButton[0].image.sprite = Lockimg;
            player1unlock.SetActive(false);
        }
        if(Player1 == 1)
        {
            player1unlock.SetActive(true);
            PlayerButton[0].image.sprite = unlockimg;
        }

        if (Player2 == 0)
        {
            PlayerButton[1].image.sprite = Lockimg;
            player2unlock.SetActive(false);
        }
        if (Player2 == 1)
        {
            player2unlock.SetActive(true);
            PlayerButton[1].image.sprite = unlockimg;
        }

        if (Player3 == 0)
        {
            PlayerButton[2].image.sprite = Lockimg;
            player3unlock.SetActive(false);
        }
        if (Player3 == 1)
        {
            player3unlock.SetActive(true);
            PlayerButton[2].image.sprite = unlockimg;
        }

        if (Player4 == 0)
        {
            PlayerButton[3].image.sprite = Lockimg;
            player4unlock.SetActive(false);
        }
        if (Player4 == 1)
        {
            player4unlock.SetActive(true);
            PlayerButton[3].image.sprite = unlockimg;
        }

        if (Player5 == 0)
        {
            PlayerButton[4].image.sprite = Lockimg;
            player5unlock.SetActive(false);
        }
        if (Player5 == 1)
        {
            player5unlock.SetActive(true);
            PlayerButton[4].image.sprite = unlockimg;
        }

        if (Player6 == 0)
        {
            PlayerButton[5].image.sprite = Lockimg;
            player6unlock.SetActive(false);
        }
        if (Player6 == 1)
        {
            player6unlock.SetActive(true);
            PlayerButton[5].image.sprite = unlockimg;
        }

        if (Player7 == 0)
        {
            PlayerButton[6].image.sprite = Lockimg;
            player7unlock.SetActive(false);
        }
        if (Player7 == 1)
        {
            player7unlock.SetActive(true);
            PlayerButton[6].image.sprite = unlockimg;
        }

        if (Player8 == 0)
        {
            PlayerButton[7].image.sprite = Lockimg;
            player8unlock.SetActive(false);
        }
        if (Player8 == 1)
        {
            player8unlock.SetActive(true);
            PlayerButton[7].image.sprite = unlockimg;
        }
    }
    public void PlayerGenerate(int i)
    {
        if(PlayerButton[i].image.sprite == Lockimg)
        {
            uiunlock[i].panel.SetActive(true);
            uiunlock[i].pricetext.text = PlayerPrefs.GetInt("Chips") + " / " +uiunlock[i].price;
            int currentchip = PlayerPrefs.GetInt("Chips");
            if (currentchip < uiunlock[i].price)
            {
                uiunlock[i].pricetext.color = Color.red;
                uiunlock[i].unlockbutton.GetComponent<Button>().interactable = false;
            }
            else if (currentchip >= uiunlock[i].price)
            {
                uiunlock[i].pricetext.color = Color.green;
                uiunlock[i].unlockbutton.GetComponent<Button>().interactable = true;
            }
        }
        
        if(PlayerButton[i].image.sprite == unlockimg)
        {
            GameObject Player = Instantiate(PlayerPrefabs[i], new Vector3(spawnposition.position.x, 1.625f, spawnposition.position.z), Quaternion.identity);
            print(Player.name);
            selectCanvas.SetActive(false);
            TurnManager.Instance.PlayerUpdate();
            TurnManager.Instance.AddCommandError();
            PlayerButton[i].interactable = false;
            startgameObject.SetActive(true);

            if (standpos != null)
            {
                standpos.SetPlayer(Player, i);
            }

        }
    }

    public void Resetbutton(int i)
    {
        PlayerButton[i].interactable = true;
    }

    [System.Serializable]
    public class PlayerUnlockUI
    {
        [SerializeField]public GameObject panel;
        [SerializeField] public TextMeshProUGUI pricetext;
        [SerializeField] public int price;
        [SerializeField] public GameObject unlockbutton;
    }
}
