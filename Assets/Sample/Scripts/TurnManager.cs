using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int turn;
    public bool IsStartGame;
    private static TurnManager instance;
    public List<GameObject> playerunit = new List<GameObject>();
    public List<GameObject> EnemyUnits = new List<GameObject>();
    public List<GameObject> Barriers = new List<GameObject>();
    public List<GameObject> heals = new List<GameObject>();

    public Button EndturnButton;
    public GameObject Endturnobject;
    public bool PlayerTurn;
    public bool EnemyTurn;

    public Image[] comandorder;
    public Sprite fullcommand;
    public Sprite emptycommand;
    public int MaxCMOpoint;
    public int currentCMOpoint;

    public bool HaveLeader;
    public int EnemyAt;
    //public LayerMask clickableLayers;

    public bool isProcessingTurn;
    public static TurnManager Instance
    {
        get
        {
            if (instance == null)
            {
                // If no instance exists, find one in the scene or create a new one
                instance = FindObjectOfType<TurnManager>();

                if (instance == null)
                {
                    // If no instance exists in the scene,u7 create a new GameObject and attach the singleton script
                    GameObject singletonObject = new GameObject("TurnManager");
                    instance = singletonObject.AddComponent<TurnManager>();
                }
            }

            return instance;
        }
    }
    private void Start()
    {
        EndturnButton.onClick.AddListener(OnClicKEndTurn);
        ResetCMOpoint();
        UpDateEnemys();
        UpdateBarriers();
        UpdateHeal();
    }

    public void ResetTile()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().walkable = true;
            tile.GetComponent<Tile>().Reset();
        }
    }
    public void RemoveInRangePlayer()
    {
        foreach (GameObject p in playerunit)
        {
            p.GetComponent<PlayerUnit>().InRange = false;
        }
    }
    public void UpdateHeal()
    {
        heals.Clear();
        GameObject[] heal = GameObject.FindGameObjectsWithTag("Heal");
        foreach (GameObject h in heal)
        {
            heals.Add(h);
        }
    }

    public void UpdateBarriers()
    {
        Barriers.Clear();
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("Barrier");
        foreach (GameObject b in barriers)
        {
            Barriers.Add(b);
        }
    }
    public void RemoveInBarrier()
    {
        if (EnemyUnits != null)
        {
            foreach (GameObject p in EnemyUnits)
            {
                p.GetComponent<EnemyUnit>().Inbarier = false;
            }
        }
        if(playerunit != null)
        {
            foreach (GameObject p in playerunit)
            {
                p.GetComponent<PlayerUnit>().Inbarier = false;
            }
        }
    }
    private void OnClicKEndTurn()
    {
        if (PlayerTurn)
        {
            print("Click EndTurn" + turn);
            EnemyTurn = true;
            PlayerTurn = false;
            ReMoveEnemyTurn();
            AddEnemyTurn();

            if (turn > 1)
            {
                StartNewEnemyTurn();
            }

        }
    }
    //
   /* IEnumerator waitForEnemyTurn()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Enemy End Turn");
        EnemyTurn=false;
        PlayerTurn = true;
        StartNewPlayerTurn();
    }*/
    public void UpDateEnemys()
    {
        EnemyUnits.Clear();
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemyUnits)
        {
            EnemyUnits.Add(e);
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
           
            EnemyUnit enemy = EnemyUnits[i].GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                enemy.EnemyNumber = i;
            }
        }
    }
    public void ReMoveAttackableEnemy()
    {
        if (EnemyUnits != null)
        {
            foreach (GameObject p in EnemyUnits)
            {
                p.GetComponent<EnemyUnit>().attackable = false;
            }
        }
    }
    public void ReMoveAttackableBarrier()
    {
        if (Barriers != null)
        {
            foreach (GameObject b in Barriers)
            {
                b.GetComponent<Barrier>().InRangeAttack = false;
            }
        }
    }
    public void PlayerUpdate()
    {
        playerunit.Clear();
        GameObject[] playerunits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playerunits)
        {
            playerunit.Add(p);
        }
        AddCommandError();
    }
    public void RemoveThisTurn()
    {
        
        foreach (GameObject p in playerunit)
        {
            p.GetComponent<PlayerUnit>().IsMyturn = false;
        }
    }
    public void AddCommandError()
    {
        foreach (GameObject p in playerunit)
        {
            p.GetComponent<PlayerUnit>().CMError = true;
        }
    }
    public void AddWalkStack()
    {
        foreach (GameObject p in playerunit)
        {
            PlayerUnit player = p.GetComponent<PlayerUnit>();
            player.currentWalkstack = player.WalkStack;
        }
    }
    public void ResetCMOpoint()
    {
        currentCMOpoint = MaxCMOpoint;
    }

    private void Update()
    {
        if(!HaveLeader && IsStartGame)
        {
            System.Random random = new System.Random();
            int ran = random.Next(0,playerunit.Count);

            PlayerUnit playerLeader = playerunit[ran].GetComponent<PlayerUnit>();
            playerLeader.IsLeader = true;
            HaveLeader = true;
            
        }
        if(IsStartGame)
        {
            if (playerunit.Count == 0)
            {
                print("EnemyWin");
                IsStartGame = false;
                //player Lostconition
                UIManager.Instance.LoseCanvas.SetActive(true);

            }
            if (EnemyUnits.Count == 0)
            {
                print("Player win");
                IsStartGame = false;
                UIManager.Instance.WinCanvas.SetActive(true);

                //state clear
                PlayerPrefs.SetInt("Level2", 1);
                PlayerPrefs.SetInt("Level1", 2);
                //enemy lost conition
            }
        }
        
        if (currentCMOpoint > MaxCMOpoint)
        {
            currentCMOpoint = MaxCMOpoint;
        }
        if(currentCMOpoint <= 0)
        {
            OnClicKEndTurn();
        }
        for (int i = 0; i < comandorder.Length; i++)
        {
            if(i<currentCMOpoint)
            {
                comandorder[i].sprite = fullcommand;
            }
            else
            {
                comandorder[i].sprite = emptycommand;
            }

            if (i < MaxCMOpoint)
            {
                comandorder[i].enabled = true;
            }
            else
            {
                comandorder[i].enabled = false;
            }
        }
        if (PlayerTurn)
        {
            ReMoveEnemyTurn();
            print("PlayerTurn");
            UIManager.Instance.ActionpointPanel.SetActive(true);

        }
        if (EnemyTurn)
        {
            RemoveThisTurn();
            print("EnemyTurn");
            UIManager.Instance.ActionpointPanel.SetActive(false);
        }
    }

    public Transform TurnEnemy()
    {
        foreach (GameObject E in EnemyUnits)
        {
            EnemyUnit enemy = E.GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                if (enemy.IsMyturn)
                {
                    return enemy.gameObject.transform;
                }
            }

        }
        return null;
    }
    public Vector3 BarrierLook()
    {
        if (PlayerTurn)
        {

            foreach (GameObject p in playerunit)
            {
                PlayerUnit player = p.GetComponent<PlayerUnit>();
                if(player != null)
                {
                    if (player.IsMyturn)
                    {
                        return player.gameObject.transform.position;
                    }
                }
                
            }
        }
        if(EnemyTurn)
        {
            
            UpDateEnemys();
            foreach (GameObject E in EnemyUnits)
            {
                EnemyUnit enemy = E.GetComponent<EnemyUnit>();
                if (enemy != null)
                {
                    if (enemy.IsMyturn)
                    {
                        return enemy.gameObject.transform.position;
                    }
                }

            }
        }
        return Vector3.zero;
        
    }
    public void StartNewPlayerTurn(int currentturn)
    {
        UpDateEnemys();
        UpdateBarriers();
        UpdateHeal();
        PlayerUpdate();
        ReMoveAttackableEnemy();
        ReMoveAttackableBarrier();
        RemoveThisTurn();
        AddCommandError();
        AddWalkStack();
        ResetCMOpoint();
        ResetTile();
        //minus CD Skill
        foreach (GameObject p in playerunit)
        {
            p.GetComponent<PlayerUnit>().currentSkill1CD --;
            p.GetComponent<PlayerUnit>().currentSkill2CD --;
            p.GetComponent<PlayerUnit>().SpacialCommand = false;
        }
        foreach (GameObject h in heals)
        {
            h.GetComponent<HealScript>().CuldownTurn--;
        }
    }

    public void AddEnemyTurn()
    {
        print("Add Enemy 0 turn");
        EnemyUnits[0].GetComponent<EnemyUnit>().IsMyturn = true;
    }
    public void StartNewEnemyTurn()
    {
        foreach (GameObject e in EnemyUnits)
        {
            EnemyUnit enemy = e.GetComponent<EnemyUnit>();
            enemy.currentSkill1CD--;
            enemy.currentSkill2CD--;
            enemy.currentWalkstack = enemy.WalkStack;
            if(enemy.IsCharge && enemy.Chargeturn > 0)
            {
                enemy.Chargeturn--;
            }
        }
    }
    public void ReMoveEnemyTurn()
    {
        foreach (GameObject e in EnemyUnits)
        {
            e.GetComponent<EnemyUnit>().IsMyturn = false;
        }
    }
    public void NextEnemyTurn(int NextEnemy)
    {
        
        if (NextEnemy >= EnemyUnits.Count)
        {
            EnemyTurn = false;
            PlayerTurn = true;
            ReMoveEnemyTurn();
            StartNewPlayerTurn(turn);
            print("1");
            turn++;
            UIManager.Instance.UpDateUITurn(turn);
            return;
        }
        EnemyUnits[NextEnemy].GetComponent<EnemyUnit>().IsMyturn = true;
    }
}
