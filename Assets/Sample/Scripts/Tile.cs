using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public bool attackable = false;
    public bool attacktarget = false;
    public bool attackselectable = false;

    public bool healPosition = false;

    public List<Tile> adjacencyList = new List<Tile>();

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public Material material;
    public GameObject tile;
    public GameObject Guide;
    public Renderer renderer;

    //For A*
    public float f = 0;
    public float g = 0;
    public float h = 0;

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;
            }
            if (TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);
        }
        else if (target)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.cyan;
            }
            if(TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);
        }
        else if (selectable)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(0f, 1f, 0f,1.0f);
            }
            if (TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);

            if (healPosition)
            {
                if (renderer != null)
                {
                    renderer.material.color = new Color(1f, 0f, 1f,1.0f);
                }
                if (TurnManager.Instance.PlayerTurn)
                    Guide.gameObject.SetActive(true);
            }
        }
        else if (attackable)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
            if (TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);
        }
        else if (attacktarget)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.magenta;
            }
            if (TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);
        }
        else if (attackselectable)
        {
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(0f, 1f, 1f, 1.0f);
            }
            if (TurnManager.Instance.PlayerTurn)
                Guide.gameObject.SetActive(true);
        }
        else
        {
            Guide.gameObject.SetActive(false);
            renderer = Guide.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(1f, 1f, 1f, 1.0f);
            }
        }

        if (TurnManager.Instance.EnemyTurn)
        {
            Reset();
        }
    }
    public void Reset()
    {
        adjacencyList.Clear();
        current = false;
        target = false;
        selectable = false;

        attackable = false;
        attackselectable = false;
        attacktarget = false;

        healPosition = false;
        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }
    public void Findneighbors(float jumpheight, Tile target)
    {
        Reset();
        CheckTile(Vector3.forward, jumpheight, target);
        CheckTile(Vector3.right, jumpheight,target);
        CheckTile(-Vector3.forward, jumpheight,target);
        CheckTile(-Vector3.right, jumpheight,target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                if (tile.walkable)
                {
                    RaycastHit hit;

                    adjacencyList.Add(tile);

                    
                }
                if (tile == target)
                {
                    print(target);
                    adjacencyList.Add(tile);

                }
            }
        }
    }
    
}
