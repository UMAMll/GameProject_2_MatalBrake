using UnityEngine;

public class Spacialbot : EnemyUnit
{
    public BigBossBot boss;
    private void Start()
    {
        if (TurnManager.Instance.IsStartGame)
        {
            HPCanvas.SetActive(true);
        }

        boss = FindObjectOfType<BigBossBot>();
        if (boss != null)
        {
            StandPosition = boss.gameObject;
        }

        GameObject Sound = GameObject.FindGameObjectWithTag("WalkSoundRobot");
        WalkSound = Sound.GetComponent<SoundManager>();
        GameObject es = GameObject.FindGameObjectWithTag("EffectSoundRobot");
        EffectSound = es.GetComponent<SoundManager>();

        Init();
    }
    private void Update()
    {
        CheckTurnUnit();

        if (TurnManager.Instance.EnemyTurn)
        {

            if (IsMyturn)
            {
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
                            FindBoss();
                            CalculatePathFollowPlayer();
                            FindSelectableTilesWalk();
                            actualTargetTile.target = true;

                        }
                        else
                        {
                            if (target == null)
                            {
                                FindBoss();
                                CalculatePathFollowPlayer();
                                FindSelectableTilesWalk();
                                actualTargetTile.target = true;
                            }
                            Move();
                            CheckBoss();
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

    public void CheckBoss()
    {
        float sphereRadius = 2;
        Vector3 sphereCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject == boss.gameObject)
            {
                print("To boss");
                if(currentHp > 0)
                {
                    boss.currentHp++;
                    boss.IsHeal();
                }
                currentHp = 0;
            }
        }
    }
}
