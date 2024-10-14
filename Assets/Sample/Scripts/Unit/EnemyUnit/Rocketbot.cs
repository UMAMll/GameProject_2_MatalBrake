using System.Collections.Generic;
using UnityEngine;

public class Rocketbot : EnemyUnit
{
    private void Start()
    {
        if (StandPosition == null)
        {
            StandPosition = FindNearestStandTarget();
        }
        Init();
    }
    private void Update()
    {
        if (!TurnManager.Instance.IsStartGame)
        {
            HPCanvas.SetActive(false);
            return;
        }

        if (TurnManager.Instance.IsStartGame)
        {
            HPCanvas.SetActive(true);
        }

        if (TurnManager.Instance.EnemyTurn)
        {

            if (IsMyturn)
            {
                OverLabFindArea();
                OverLabEscapeArea();
                OverLabAttackArea();
                if (IsCharge)
                {
                    CanMove = false;
                }
                else
                {
                    CanMove = true;
                }

                if (!LowHealth)
                {
                    if (!PlayerNearest)
                    {
                        if (CanAttack1)
                        {
                            Invoke("Attack1", 3.0f);
                        }
                    }

                }

                if (currentWalkstack <= 0)
                {
                    CanMove = false;
                }
                else if (currentWalkstack > 0)
                {
                    CanMove = true;
                }

                ManageSkillCD();

                if (CanMove && IsMyturn)
                {
                    if ((!CanAttack1 || playersCanAttack.Count == 0) || PlayerNearest)
                    {
                        if (!moving)
                        {
                            FindNearestTarget();
                            if (PlayerNearest)
                            {
                                CalculatePathEscapePlayer();
                            }
                            else if (!PlayerNearest)
                            {
                                if (LowHealth)
                                {
                                    CalculatePathEscapePlayer();
                                }
                                else
                                {
                                    CalculatePathFollowPlayer();
                                }
                            }
                            FindSelectableTilesWalk();
                            actualTargetTile.target = true;
                        }
                        else
                        {
                            Move();
                        }
                    }

                }
                if ((!CanMove) && ( IsCharge || !CanAttack1 || playersCanAttack.Count == 0) && (!CanAttack2 || playersCanAttack.Count == 0))
                {
                    TurnManager.Instance.ReMoveEnemyTurn();
                    TurnManager.Instance.NextEnemyTurn(EnemyNumber + 1);
                }

            }

        }
        // health
        HealthManage();
    }

    public List<Tile> targetAttack = new List<Tile>();
    
    public void Attack1()
    {
        if (CanAttack1)
        {
            if (playersCanAttack.Count > 0)
            {
                if (currentSkill1CD == 0)
                {
                    if(playersCanAttack.Count < 4)
                    {
                        currentSkill1CD = Skill1CD;
                        CanAttack1 = false;
                    }
                    if (!IsCharge && playersCanAttack.Count >= 4)
                    {
                        RaycastHit hit;
                        for (int i = 0; i < playersCanAttack.Count; i++)
                        {

                            if (Physics.Raycast(playersCanAttack[i].transform.position, -Vector3.up, out hit, 1))
                            {
                                Tile tiles;
                                tiles = hit.collider.GetComponent<Tile>();
                                targetAttack.Add(tiles);
                            }
                        }
                        print("Charge");
                        currentWalkstack = 0;
                        IsCharge = true;
                        Chargeturn = MaxChargeturn;
                    }
                    if (IsCharge && Chargeturn == 0)
                    {
                        RaycastHit hit;
                        for (int i = 0; i < targetAttack.Count; i++)
                        {

                            if (Physics.Raycast(targetAttack[i].transform.position, Vector3.up, out hit, 1))
                            {
                                if (hit.collider.CompareTag("Player"))
                                {
                                    PlayerUnit targetUnit = hit.collider.GetComponent<PlayerUnit>();
                                    targetUnit.currentHp -= 5;
                                    targetUnit.IsBoomHit();

                                }
                            }
                        }
                        IsCharge = false;
                        targetAttack.Clear();
                        print("Attack");
                        currentWalkstack = 0;
                        moving = false;
                        currentSkill1CD = Skill1CD;
                    }
                }
            }
        }
    }
}