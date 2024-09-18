using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    public int healPower;
    public int MaxCuldown;
    public int CuldownTurn;
    public int HealStack;
    public GameObject HealModel;
    Collider[] col;
    Tile t;
    void Start()
    {
        col = gameObject.GetComponents<Collider>();
    }

    void Update()
    {
        if ((CuldownTurn <= 0))
        {
            CuldownTurn = 0;
        }
        {
            
        }
        if (CuldownTurn == 0)
        {
            HealModel.GetComponent<Renderer>().material.color = Color.yellow;
            foreach (Collider h in col)
            {
                h.enabled = true;
            }
        }
        else
        {
            HealModel.GetComponent<Renderer>().material.color = Color.white;
            foreach (Collider h in col)
            {
                h.enabled = false;
            }
        }
        if (HealStack <= 0)
        {
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerUnit player = other.GetComponent<PlayerUnit>();

            if (player != null)
            {

                player.currentHp += healPower;
                HealStack -= 1;
                foreach (Collider h in col)
                {
                    h.enabled = false;
                }
                CuldownTurn = MaxCuldown;

            }
            
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            t = other.GetComponent<Tile>();
            if (t != null)
            {
                t.healPosition = true;
            }
        }
    }
}
