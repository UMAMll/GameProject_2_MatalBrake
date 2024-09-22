using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minibot : EnemyUnit
{
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
                OverLabFindArea();
                OverLabEscapeArea();
                OverLabAttackArea();
                if (LowHealth)
                {
                    CanAttack1 = false;
                }
                if (CanAttack1)
                {
                    Invoke("Attack1", 3.0f);
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
                    if (!CanAttack1 || playersCanAttack.Count == 0)
                    {
                        
                        if (!moving)
                        {
                            FindNearestTarget();
                            if (LowHealth)
                            {
                                CalculatePathEscapePlayer();
                            }
                            else
                            {
                                CalculatePathFollowPlayer();
                            }
                            FindSelectableTilesWalk();
                            actualTargetTile.target = true;
                            
                        }
                        else
                        {
                            if(target == null)
                            {
                                FindNearestTarget();
                                if (LowHealth)
                                {
                                    CalculatePathEscapePlayer();
                                }
                                else
                                {
                                    CalculatePathFollowPlayer();
                                }
                                FindSelectableTilesWalk();
                                actualTargetTile.target = true;
                            }
                            Move();
                        }
                    }
                    

                }
                if (!CanMove && (!CanAttack1 || playersCanAttack.Count == 0) && (!CanAttack2 || playersCanAttack.Count == 0))
                {
                    TurnManager.Instance.ReMoveEnemyTurn();
                    TurnManager.Instance.NextEnemyTurn(EnemyNumber + 1);
                }

            }

        }
        // health
        HealthManage();
    }


    public void Attack1()
    {
        if (CanAttack1)
        {
            if (playersCanAttack.Count > 0)
            {
                if (currentSkill1CD == 0)
                {
                    PlayerUnit playertarget = FindNearestAttackTarget().GetComponent<PlayerUnit>();
                    playertarget.currentHp -= 2;
                    transform.LookAt(playertarget.transform.position);
                    currentWalkstack = 0;
                    moving = false;
                    currentSkill1CD = Skill1CD;
                }
            }
        }
    }
}