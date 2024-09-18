using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnit : TacticSystem
{
    public Image[] Heart;
    public Sprite fullhealth;
    public Sprite emptyhealth;


    private void Update()
    {
        if (currentHp <= 0)
        {
            StartCoroutine(WaitForDead());
        }
        if (currentHp > HpPoint)
        {
            currentHp = HpPoint;
        }

        for (int i = 0; i < Heart.Length; i++)
        {
            if (i < currentHp)
            {
                Heart[i].sprite = fullhealth;
            }
            else
            {
                Heart[i].sprite = emptyhealth;
            }

            if (i < HpPoint)
            {
                Heart[i].enabled = true;
            }
            else
            {
                Heart[i].enabled = false;
            }
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {

            Tile tile = other.gameObject.GetComponent<Tile>();
            if (!tile.walkable)
            {
                tile.walkable = true;
            }

        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (attackable)
            {
                print("Find tile and correct");
                tile.attackable = true;
            }
            if(!attackable)
            {
                tile.attackable = false;
            }
            if (tile.walkable)
            {
                tile.walkable = false;
                tile.selectable = false;
            }

        }
    }
    

    IEnumerator WaitForDead()
    {
        Debug.Log("EnemyDied");
        yield return new WaitForSeconds(2);
        print(gameObject);
        TurnManager.Instance.EnemyUnits.Remove(gameObject);
        Destroy(gameObject);
        //TurnManager.Instance.UpDateEnemys();
        print("Destroy");
        TurnManager.Instance.ResetTile();
    }
}
