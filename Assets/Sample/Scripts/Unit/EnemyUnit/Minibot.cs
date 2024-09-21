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
                OverLabAttackArea();
                if (!moving)
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
                            CalculatePath();
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
}
