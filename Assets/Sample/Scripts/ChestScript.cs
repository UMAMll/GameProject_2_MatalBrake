using UnityEngine;

public class ChestScript : MonoBehaviour
{
    
    public void Showchest(GameObject chest)
    {
        chest.SetActive(true);
    }
    public void Hidechest(GameObject chest)
    {
        chest.SetActive(false);
    }
    public void KillAllEnemy()
    {
        foreach(var enemy in TurnManager.Instance.EnemyUnits)
        {
            EnemyUnit e = enemy.GetComponent<EnemyUnit>();
            if (e != null)
            {
                e.currentHp = 0;
            }
        }
    }

    public void FullHpEnemy()
    {
        foreach (var enemy in TurnManager.Instance.EnemyUnits)
        {
            EnemyUnit e = enemy.GetComponent<EnemyUnit>();
            if (e != null)
            {
                e.currentHp = e.HpPoint;
            }
        }
    }

    public void Hp_1Enemy()
    {
        foreach (var enemy in TurnManager.Instance.EnemyUnits)
        {
            EnemyUnit e = enemy.GetComponent<EnemyUnit>();
            if (e != null)
            {
                e.currentHp = 1;
            }
        }
    }

    public void KillEnemyOne()
    {
        for(int i = 0; i < (TurnManager.Instance.EnemyUnits.Count -1); i++)
        {
            EnemyUnit enemy = TurnManager.Instance.EnemyUnits[i].GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                enemy.currentHp = 0;
            }
        }
        TurnManager.Instance.UpDateEnemys();
    }

    public void KillAllPlayer()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.currentHp = 0;
            }
        }
    }

    public void AddMaxHpPlayer()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.HpPoint = 20;
                p.currentHp = 20;
            }
        }
    }
    public void AddDamagePlayer()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.skill1Damage = 20;
                p.skill2Damage = 20;
            }
        }
    }
    public void ResetCDPlayer()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.currentSkill1CD = 0;
                p.currentSkill2CD = 0;
            }
        }
    }
    public void AddMoveArea()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.movearea = 20;
            }
        }
    }
    
    public void Hp_1()
    {
        foreach (var player in TurnManager.Instance.playerunit)
        {
            PlayerUnit p = player.GetComponent<PlayerUnit>();
            if (p != null)
            {
                p.currentHp = 1;
            }
        }
    }
}
