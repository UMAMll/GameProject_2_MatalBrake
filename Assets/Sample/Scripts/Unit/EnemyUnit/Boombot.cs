using System.Threading.Tasks;

public class Boombot : EnemyUnit
{

    private void Start()
    {
        if(StandPosition == null)
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
            CheckTurnUnit();

            if (IsMyturn)
            {
                OverLabFindArea();
                OverLabEscapeArea();
                OverLabAttackArea();
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
                    PlayerUnit playertarget = FindNearestAttackTarget().GetComponent<PlayerUnit>();
                    transform.LookAt(playertarget.transform.position);
                    playertarget.currentHp -= 3;
                    playertarget.IsHit();
                    currentWalkstack = 0;
                    moving = false;
                    currentSkill1CD = Skill1CD;
                }
            }
        }
    }
}