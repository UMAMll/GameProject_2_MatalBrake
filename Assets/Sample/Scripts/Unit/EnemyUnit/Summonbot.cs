using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Summonbot : EnemyUnit
{
    public bool IsSummon;
    public GameObject[] summonObject;
    public GameObject[] summonTargetpos;
    private void Start()
    {
        StandPosition = FindNearestStandTarget();
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
                Attack2();
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

                if (currentSkill1CD <= 0)
                {
                    currentSkill1CD = 0;
                }

                if (currentSkill2CD <= 0)
                {
                    currentSkill2CD = 0;
                }

                if (currentSkill1CD == 0 && currentSkill1CD != Skill1CD)
                {
                    if (!LowHealth)
                    {
                        CanAttack1 = true;
                    }
                    if (LowHealth)
                    {
                        CanAttack1 = false;
                    }
                }
                else if (currentSkill1CD != 0 || currentSkill1CD == Skill1CD)
                {
                    CanAttack1 = false;
                }

                if (currentSkill2CD == 0 && currentSkill2CD != Skill2CD)
                {
                    if (!IsSummon)
                    {
                        CanAttack2 = false;
                    }
                    if (IsSummon)
                    {
                        CanAttack2 = true;
                    }
                }
                else if (currentSkill2CD != 0 || currentSkill2CD == Skill2CD)
                {
                    CanAttack2 = false;
                }


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
                            if (target == null)
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
                    transform.LookAt(playertarget.transform.position);
                    playertarget.currentHp -= 1;
                    playertarget.IsHit();
                    currentWalkstack = 0;
                    moving = false;
                    currentSkill1CD = Skill1CD;
                }
            }
        }
    }

    public void Attack2()
    {
        if (currentSkill2CD == 0)
        {
            if(currentHp > (HpPoint/2))
            {
                CanAttack2 = false;
                IsSummon = false;
            }

            if (currentHp <= (HpPoint /2))
            {
                IsSummon = true;
            }
            if (IsSummon)
            {
                if (!moving && CanAttack2)
                {
                    int randomObject = Random.Range(0, summonObject.Length);
                    int randompos = Random.Range(0, summonTargetpos.Length);

                    Instantiate(summonObject[randomObject], summonTargetpos[randompos].transform.position, Quaternion.identity);
                    currentSkill2CD = Skill2CD;
                }

            }



        }
    }


}