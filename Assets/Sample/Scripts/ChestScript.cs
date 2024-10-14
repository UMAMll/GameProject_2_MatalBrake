using UnityEngine;

public class ChestScript : MonoBehaviour
{
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
}
