using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPlayerTile : MonoBehaviour
{
    public Transform position;
    public SelectPlayer SelectPlayer;
    void Start()
    {
        position = GetComponent<Transform>();
        GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnMouseDown()
    {
        SelectPlayer.spawnposition = position;
    }
    private void OnMouseUp()
    {
        SelectPlayer.selectCanvas.SetActive(true);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
