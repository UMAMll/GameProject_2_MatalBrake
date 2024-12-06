using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public bool tablemode;
    public Sprite ModeOn, ModeOff;
    public Button tableModeButton;
    public GameObject[] tiles;
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tableModeButton.onClick.AddListener(TableModeSet);
        TableModeSet();
    }

    void TableModeSet()
    {
        
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            if (t != null)
            {
                t.TableMode = !tablemode;
                t.CheckTile();
            }
        }

        if (tablemode)
        {
            tablemode = false;
            tableModeButton.image.sprite = ModeOff;
        }
        else
        {
            tablemode = true;
            tableModeButton.image.sprite = ModeOn;
        }
    }
   
    
}
