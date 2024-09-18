using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInScene : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
       
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            tile.walkable = false;
            tile.selectable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            tile.walkable = true;
        }
    }

}
