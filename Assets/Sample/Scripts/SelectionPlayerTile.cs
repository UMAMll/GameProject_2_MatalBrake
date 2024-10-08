using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPlayerTile : MonoBehaviour
{
    public Transform position;
    public SelectPlayer SelectPlayer;
    public GameObject player;
    public int playernum;
    void Start()
    {
        position = GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        SelectPlayer.spawnposition = position;
        if (player != null )
        {
            Destroy(player);
            SelectPlayer.Resetbutton(playernum);
        }
        
    }
    private void OnMouseUp()
    {
        SelectPlayer.selectCanvas.SetActive(true);
        SelectPlayer.standpos = gameObject.GetComponent<SelectionPlayerTile>();
        
        
    }

    public void SetPlayer(GameObject playerObj, int num)
    {
        player = playerObj;
        playernum = num;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
