using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnit : TacticSystem
{
    public GameObject target;
    public Tile targetTile;

    public int EnemyNumber;

    public bool CanMove;
    public bool CanAttack;

    //heath
    public Image[] Heart;
    public Sprite fullhealth;
    public Sprite emptyhealth;

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (!TurnManager.Instance.IsStartGame)
        {
            return;
        }
        if (TurnManager.Instance.EnemyTurn)
        {
            
            if (IsMyturn)
            {
                if(currentWalkstack <=0)
                {
                    CanMove = false;
                }
                else if(currentWalkstack >0)
                {
                    CanMove=true;
                }
                if (CanMove && IsMyturn)
                {
                    if (!moving)
                    {
                        FindNearestTarget();
                        CalculatePath();
                        FindSelectableTilesWalk();
                        actualTargetTile.target = true;
                    }
                    else
                    {
                        Move();
                    }
                }
                if (!CanMove && !CanAttack)
                {
                    TurnManager.Instance.ReMoveEnemyTurn();
                    TurnManager.Instance.NextEnemyTurn(EnemyNumber +1);
                }
            }
            /*if(!CanMove && !CanAttack)
            {
                TurnManager.Instance.ReMoveEnemyTurn();
            }*/
        }
        // health
        HealthManage();
    }

    void HealthManage()
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
    void CalculatePath()
    {
        targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
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
            if (!attackable)
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