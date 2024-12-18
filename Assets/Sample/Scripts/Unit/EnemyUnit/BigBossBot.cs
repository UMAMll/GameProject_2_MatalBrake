using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossBot : EnemyUnit
{
    public int SummonCount;
    public GameObject[] summonObject;
    public GameObject summonSpacialObject;
    public GameObject[] summonTargetpos;
    public GameObject[] summonSpacialTargetpos;

    public int fate;
    private void Start()
    {
        GameObject Sound = GameObject.FindGameObjectWithTag("WalkSoundRobot");
        WalkSound = Sound.GetComponent<SoundManager>();
        GameObject es = GameObject.FindGameObjectWithTag("EffectSoundRobot");
        EffectSound = es.GetComponent<SoundManager>();

        fate = 1;
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
            CheckTurnUnit();
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


                if (CanAttack1)
                {
                    Attack1();
                    StartCoroutine(EndturnBoss(1));
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

                    CanAttack1 = true;

                }
                else if (currentSkill1CD != 0 || currentSkill1CD == Skill1CD)
                {
                    CanAttack1 = false;
                }

                if (currentSkill2CD == 0 && currentSkill2CD != Skill2CD)
                {

                    CanAttack2 = true;

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

                            CalculatePathFollowPlayer();

                            FindSelectableTilesWalk();
                            actualTargetTile.target = true;

                        }
                        else
                        {
                            if (target == null)
                            {
                                FindNearestTarget();

                                CalculatePathFollowPlayer();

                                FindSelectableTilesWalk();
                                actualTargetTile.target = true;
                            }
                            Move();
                        }
                    }


                }
                if (!CanMove && (!CanAttack1) && (!CanAttack2))
                {
                    TurnManager.Instance.ReMoveEnemyTurn();
                    TurnManager.Instance.NextEnemyTurn(EnemyNumber + 1);
                }

            }

        }
        // health
        if(fate == 1)
        {
            if(currentHp <= 1)
            {
                currentHp = 1;
                if(animator != null)
                {
                    animator.SetTrigger("Fate2");
                }
                IsPowerUp();
                fate = 2;
                WalkStack = 0;
                currentWalkstack = 0;
                currentSkill2CD = 1;
                Invoke("RestoreHealth", 3.0f);
            }
        }
        HealthManage();
    }
    
    IEnumerator EndturnBoss(int nowcd)
    {
        if(playersCanAttack.Count == 0)
        {
            yield return new WaitForSeconds(1);
            currentSkill1CD = nowcd;
            CanAttack1 = false;
        }

    }
    public void RestoreHealth()
    {
        currentHp = HpPoint/2;
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
                    if (GunflashEffect != null)
                    {
                        GunflashEffect.Play();
                    }
                    if (animator != null)
                    {
                        animator.SetTrigger("Attack");
                    }
                    if(EffectSound != null)
                    {
                        EffectSound.RifleShotSound();
                    }
                    if (fate == 1)
                    {
                        Skill1CD = 1;
                        playertarget.currentHp -= skill1Damage;
                        playertarget.IsHit();
                        currentWalkstack = 0;
                        moving = false;
                        currentSkill1CD = Skill1CD;
                    }

                    if (fate == 2)
                    {
                        Skill1CD = 3;
                        List<Spacialbot> enemyunitcount = new List<Spacialbot>();
                        enemyunitcount.Clear();
                        enemyunitcount.AddRange(FindEnemy());

                        int damage = skill1Damage + (1 * (enemyunitcount.Count));
                        playertarget.currentHp -= damage;
                        playertarget.IsHit();
                        currentWalkstack = 0;
                        moving = false;
                        currentSkill1CD = Skill1CD;
                    }
                }
            }
        }
    }
    Spacialbot[] FindEnemy()
    {
        Spacialbot[] count = FindObjectsOfType<Spacialbot>();
        return count;
    }
    public void Attack2()
    {
        if (currentSkill2CD == 0)
        {

            if (!moving && CanAttack2)
            {
                if(animator != null)
                {
                    animator.SetTrigger("Summon");
                }
                if(fate == 1)
                {
                    SummonCount = 2;
                    for (int i = 0; i < SummonCount; i++)
                    {
                        int randomObject = Random.Range(0, summonObject.Length);
                        Instantiate(summonObject[randomObject], summonTargetpos[i].transform.position, Quaternion.identity);
                    }
                    moving = false;
                    currentSkill1CD = Skill1CD;
                    currentSkill2CD = Skill2CD;
                }

                if (fate == 2)
                {
                    
                    for (int i = 0; i < summonSpacialTargetpos.Length; i++)
                    {
                        Instantiate(summonSpacialObject, summonSpacialTargetpos[i].transform.position, Quaternion.identity);
                    }
                    moving = false;
                    currentSkill1CD = Skill1CD;
                    currentSkill2CD = Skill2CD;
                }
                
            }
        }
    }

}
