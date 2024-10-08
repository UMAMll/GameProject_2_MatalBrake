using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    void Start()
    {
        startgameObject.SetActive(false);
        PlayerButton[0].onClick.AddListener(() => PlayerGenerate(0));
        PlayerButton[1].onClick.AddListener(() => PlayerGenerate(1));
        PlayerButton[2].onClick.AddListener(() => PlayerGenerate(2));
        PlayerButton[3].onClick.AddListener(() => PlayerGenerate(3));
        PlayerButton[4].onClick.AddListener(() => PlayerGenerate(4));
        PlayerButton[5].onClick.AddListener(() => PlayerGenerate(5));
        PlayerButton[6].onClick.AddListener(() => PlayerGenerate(6));
        PlayerButton[7].onClick.AddListener(() => PlayerGenerate(7));
        startGamebutton.onClick.AddListener(StartGame);

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

    public GameObject PlayerGenerate(int i)
    {
        print("Spawn");
        GameObject Player = Instantiate(PlayerPrefabs[i], new Vector3(spawnposition.position.x,1.5f,spawnposition.position.z), Quaternion.identity);
        print(Player.name);
        selectCanvas.SetActive(false);
        TurnManager.Instance.PlayerUpdate();
        TurnManager.Instance.AddCommandError();
        PlayerButton[i].interactable = false;
        startgameObject.SetActive(true);

        if(standpos != null)
        {
            standpos.SetPlayer(Player , i);
        }
        return Player;
    }

    public void Resetbutton(int i)
    {
        PlayerButton[i].interactable = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
