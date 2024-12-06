using UnityEngine;

public class ObjectInScene : MonoBehaviour
{
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
