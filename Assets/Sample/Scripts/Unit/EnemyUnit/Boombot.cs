using System.Threading.Tasks;
using UnityEngine;

public class Boombot : EnemyUnit
{

    private void Start()
    {
        GameObject Sound = GameObject.FindGameObjectWithTag("WalkSoundRobot");
        WalkSound = Sound.GetComponent<SoundManager>();
        GameObject es = GameObject.FindGameObjectWithTag("EffectSoundRobot");
        EffectSound = es.GetComponent<SoundManager>();

        

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
                if (!LowHealth)
                {
                        if (CanAttack1)
                        {
                            Invoke("Attack1", 3.0f);
                        }
                    
                }
                if (currentWalkstack <= 0)
                {
                    animator.SetBool("Walk",false);
                    CanMove = false;
                    //StartCoroutine(DelayTurn(2));
                }
                else if (currentWalkstack > 0)
                {
                    CanMove = true;
                }

                ManageSkillCD();

                if (CanMove && IsMyturn)
                {
                    if (target == null)
                    {
                        StartCoroutine(WaitforCamera());
                    }
                    if ((!CanAttack1 || playersCanAttack.Count == 0)|| PlayerNearest)
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
                if (!CanMove && (!CanAttack1 || playersCanAttack.Count == 0) && (!CanAttack2 || playersCanAttack.Count == 0))
                {

                    TurnManager.Instance.ReMoveEnemyTurn();
                    print("11");
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
                    if(EffectSound != null)
                    {
                        EffectSound.ExplosionSound();
                    }
                    PlayerUnit playertarget = FindNearestAttackTarget().GetComponent<PlayerUnit>();
                    transform.LookAt(playertarget.transform.position);
                    if (GunflashEffect != null)
                    {
                        GunflashEffect.Play();
                    }
                    playertarget.currentHp -= skill1Damage;
                    playertarget.IsHit();
                    currentWalkstack = 0;
                    if (animator != null)
                    {
                        animator.SetBool("Walk", false);
                    }
                    moving = false;
                    currentSkill1CD = Skill1CD;
                }
            }
        }
    }
}