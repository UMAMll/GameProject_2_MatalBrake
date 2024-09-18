using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    PlayerUnit playerLook;
    EnemyUnit enemyLook;

    Vector3 UnitPosition;

    Rigidbody rb;

    public bool InRangeAttack;

    public GameObject barrier;

    GameObject unitPlayer;
    GameObject unitEnemy;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        UnitPosition = TurnManager.Instance.BarrierLook();

        Vector3 direction = new Vector3(UnitPosition.x - transform.position.x, 0, UnitPosition.z - transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 2));

        if(InRangeAttack )
        {
            barrier.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
        }
        if(!InRangeAttack )
        {
            barrier.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerUnit player = other.GetComponent<PlayerUnit>();
            if (player != null)
            {
                player.Inbarier = true;
            }
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyUnit enemy = other.GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                enemy.Inbarier = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerUnit player = other.GetComponent<PlayerUnit>();
            unitPlayer = other.GetComponent<GameObject>();
            if (player != null)
            {
                player.Inbarier = false;
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyUnit enemy = other.GetComponent<EnemyUnit>();
            unitEnemy = other.GetComponent<GameObject>();
            if (enemy != null)
            {
                enemy.Inbarier = false;
            }
        }
    }

    public void IsAttack()
    {
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2);
        TurnManager.Instance.Barriers.Clear();
        TurnManager.Instance.RemoveInBarrier();
        TurnManager.Instance.ResetTile();
        Destroy(barrier);
        //yield return new WaitForSeconds(1);
        
    }
}
