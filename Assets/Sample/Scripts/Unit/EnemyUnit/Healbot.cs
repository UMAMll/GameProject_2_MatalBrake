public class Healbot : EnemyUnit
{
    public bool Isheal;
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
                    if(!Isheal)
                    {
                        CanAttack2 = false;
                    }
                    if(Isheal)
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
                    if ((!CanAttack2 ||!CanAttack1 || playersCanAttack.Count == 0))
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
        if (currentSkill1CD == 0)
        {
            foreach (var target in TurnManager.Instance.EnemyUnits)
            {

                EnemyUnit enemyUnit = target.GetComponent<EnemyUnit>();

                if (enemyUnit != null)
                {
                    if (enemyUnit.currentHp <= (enemyUnit.HpPoint / 2))
                    {
                        Healbot me = this.gameObject.GetComponent<Healbot>();
                        me.Isheal = true;
                    }
                    if (Isheal)
                    {
                        if (!moving && CanAttack2)
                        {
                            enemyUnit.currentHp += 2;
                            enemyUnit.IsHeal();
                            currentSkill2CD = Skill2CD;
                        }

                    }

                }
            }
        }
    }
        
    
}
