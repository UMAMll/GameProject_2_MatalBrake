using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnit : TacticSystem
{
    public GameObject target;
    public GameObject StandPosition;
    public Tile targetTile;

    public int EnemyNumber;

    public float FindArea;
    public float AttackArea;

    public bool CanMove;
    public bool CanAttack1;
    public bool CanAttack2;

    //heath
    public Image[] Heart;
    public Sprite fullhealth;
    public Sprite emptyhealth;

    public List<GameObject> playersFound = new List<GameObject>();
    public List<GameObject> playersCanAttack = new List<GameObject>();
    
    public void ManageSkillCD()
    {
        if(currentSkill1CD <=0)
        {
            currentSkill1CD = 0;
        }

        if(currentSkill2CD <= 0)
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
    }
    public void HealthManage()
    {
        if (currentHp <= 0)
        {
            StartCoroutine(WaitForDead());
        }
        if (currentHp > HpPoint)
        {
            currentHp = HpPoint;
        }
        for (int i = 0; i < Heart.Length; i++)
        {
            if (i < currentHp)
            {
                Heart[i].sprite = fullhealth;
            }
            else
            {
                Heart[i].sprite = emptyhealth;
            }

            if (i < HpPoint)
            {
                Heart[i].enabled = true;
            }
            else
            {
                Heart[i].enabled = false;
            }
        }
    }
    public void OverLabFindArea()
    {
        playersFound.Clear();
        float sphereRadius = FindArea;
        Vector3 sphereCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                playersFound.Add(player);
                playersFound = RemoveDuplicateItems(playersFound);
            }
        }
    }
    public void OverLabAttackArea()
    {
        playersCanAttack.Clear();
        float sphereRadius = AttackArea;
        Vector3 sphereCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                playersCanAttack.Add(player);
                playersCanAttack = RemoveDuplicateItems(playersCanAttack);
            }
        }
    }

    public void Attack1()
    {
        if (CanAttack1)
        {
            if(playersCanAttack.Count > 0)
            {
                if(currentSkill1CD == 0)
                {
                    PlayerUnit playertarget = FindNearestAttackTarget().GetComponent<PlayerUnit>();
                    playertarget.currentHp -= 2;
                    currentSkill1CD = Skill1CD;
                }
            }
        }
    }
    List<T> RemoveDuplicateItems<T>(List<T> list)
    {
        HashSet<T> set = new HashSet<T>(list);
        return new List<T>(set);
    }
    public void CalculatePath()
    {
        targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }
    public GameObject FindNearestAttackTarget()
    {

        GameObject[] targets = playersCanAttack.ToArray();
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        return nearest;
    }
    public void FindNearestTarget()
    {
        if(playersFound.Count == 0)
        {
            target = StandPosition;
        }

        if (playersFound.Count > 0)
        {
            GameObject[] targets = playersFound.ToArray();
            GameObject nearest = null;
            float distance = Mathf.Infinity;

            foreach (GameObject obj in targets)
            {
                float d = Vector3.Distance(transform.position, obj.transform.position);

                if (d < distance)
                {
                    distance = d;
                    nearest = obj;
                }
            }

            target = nearest;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {

            Tile tile = other.gameObject.GetComponent<Tile>();
            if (attackable)
            {
                print("Find tile and correct");
                tile.attackable = true;
            }
            if (!attackable)
            {
                tile.attackable = false;
            }
            if (tile.walkable)
            {
                tile.walkable = false;
                tile.selectable = false;
            }

        }
    }


    IEnumerator WaitForDead()
    {
        Debug.Log("EnemyDied");
        yield return new WaitForSeconds(2);
        print(gameObject);
        TurnManager.Instance.EnemyUnits.Remove(gameObject);
        Destroy(gameObject);
        //TurnManager.Instance.UpDateEnemys();
        print("Destroy");
        TurnManager.Instance.ResetTile();
    }
}