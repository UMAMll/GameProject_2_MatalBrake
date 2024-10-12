using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;

public class SompornScript : PlayerUnit
{
    public bool IsBuff;
    private void Start()
    {
        actionCanves.SetActive(false);
        Init();
        CanAttack = false;
        walkButton.onClick.AddListener(OnClickWalkButton);
        skill1Button.onClick.AddListener(OnClickSkill1Button);
        skill2Button.onClick.AddListener(OnClickSkill2Button);
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

        if (TurnManager.Instance.PlayerTurn)
        {
            TurnManager.Instance.Endturnobject.SetActive(true);
            if (IsMyturn)
            {
                if (!IsShowselect)
                {
                    string unitname = Unitname + " (Selected)";
                    UIManager.Instance.SetProfilePanel(unitname, ProfileImg, HpPoint, currentHp, currentstatus, statusUnit);
                    IsShowselect = true;
                }

                if (!moving)
                {
                    CheckMouse();
                    if (currentWalkstack != 0)
                    {
                        walkButton.interactable = true;
                    }
                    if (!attacking && isAttack == 1 && CanAttack)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            Vector3 targetPosition = hit.point;
                            targetPosition.y = transform.position.y;
                            transform.LookAt(targetPosition);
                        }
                        CheckMouseAttack1();
                    }
                    if (!attacking && isAttack == 2 && CanAttack)
                    {
                        
                        CheckMouseAttack2();
                    }
                }
                else
                {
                    if (currentWalkstack != 0)
                    {
                        walkButton.interactable = false;
                    }
                    Move();
                }

            }
            if (currentWalkstack == 0)
            {
                walkButton.interactable = false;
            }
            if (currentWalkstack == 0 && currentSkill1CD == Skill1CD && currentSkill2CD == Skill2CD)
            {
                IsMyturn = false;
            }
            if (!IsMyturn)
            {
                IsShowselect = false;
                actionCanves.SetActive(false);
            }
            if (CMError)
            {
                if (!IsLeader)
                {
                    currentstatus = 1;
                }
                else if (IsLeader)
                {
                    currentstatus = 2;
                }
                statusUnit = "CMError";
            }
            else if (!CMError)
            {
                if (!IsLeader)
                {
                    currentstatus = 0;
                }
                else if (IsLeader)
                {
                    currentstatus = 1;
                }
            }

            if (IsLeader)
            {
                currentstatus = 1;
                statusUnit = "Leader";
                CMError = false;
                float sphereRadius = LeaderArea;
                Vector3 sphereCenter = transform.position;
                Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);

                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Player"))
                    {
                        PlayerUnit player = hitCollider.GetComponent<PlayerUnit>();
                        if (player != null)
                        {

                            player.CMError = false;
                        }
                    }
                }
            }
        }
        //not playerturn
        else
        {
            TurnManager.Instance.Endturnobject.SetActive(false);
            actionCanves.SetActive(false);
        }
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
        if (currentSkill1CD == Skill1CD)
        {
            skill1Button.interactable = false;
        }
        else if (currentSkill1CD <= 0)
        {
            currentSkill1CD = 0;
            skill1Button.interactable = true;
        }
        if (currentSkill2CD == Skill2CD)
        {
            skill2Button.interactable = false;
        }
        else if (currentSkill2CD <= 0)
        {
            currentSkill2CD = 0;
            skill2Button.interactable = true;
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Barrier"))
        {
            objectsInColliderskill1.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Barrier"))
        {
            objectsInColliderskill1.Remove(other.gameObject);
        }
    }
    public void CheckMouseAttack1()
    {
        Skill1Colider.SetActive(true);
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

           
            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                
                if (CanAttack)
                {
                    print("ObjectCount = " +objectsInColliderskill1.Count);
                    if (objectsInColliderskill1.Count == 0)
                    {
                        return;
                    }
                    foreach (var target in objectsInColliderskill1)
                    {
                        print("AttackEnemy");
                        if (target.CompareTag("Barrier"))
                        {
                            Barrier barrier = target.GetComponent<Barrier>();
                            barrier.IsAttack();
                        }
                        if (target.CompareTag("Enemy"))
                        {
                            EnemyUnit unit = target.GetComponent<EnemyUnit>();
                            if (IsBuff)
                            {
                                unit.currentHp -= 4;
                                unit.IsHit();

                            }
                            else
                            {
                                unit.currentHp -= 2;
                                unit.IsHit();
                            }
                            print("Attack" + unit.name + " " + unit.currentHp);

                        }

                    }
                    IsBuff = false;
                    currentSkill1CD = Skill1CD;
                    objectsInColliderskill1.Clear();
                    Skill1Colider.SetActive(false);
                    if (CMError)
                    {
                        if (SpacialCommand)
                        {
                            TurnManager.Instance.currentCMOpoint -= 0;
                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill1 * 2);

                        }
                    }
                    else
                    {
                        if (SpacialCommand)
                        {
                            TurnManager.Instance.currentCMOpoint -= 0;
                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill1;

                        }
                    }
                    foreach (GameObject tile in tiles)
                    {
                        t = tile.GetComponent<Tile>();
                        print(t);
                        if (t != null && CanAttack)
                        {
                            t.Reset();

                        }
                    }
                    SpacialCommand = false;
                    CanAttack = false;
                    TurnManager.Instance.ReMoveAttackableEnemy();
                    TurnManager.Instance.ReMoveAttackableBarrier();
                }



            }

        }
    }
    public void CheckMouseAttack2()
    {
        IsBuff = true;
        IsPowerUp();
        currentSkill2CD = Skill2CD;

        if (CMError)
        {
            if (SpacialCommand)
            {
                TurnManager.Instance.currentCMOpoint -= 0;
            }
            else
            {
                TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill2 * 2);

            }
        }
        else
        {
            if (SpacialCommand)
            {
                TurnManager.Instance.currentCMOpoint -= 0;
            }
            else
            {
                TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill2;

            }
        }
        Tile t;
        foreach (GameObject tile in tiles)
        {
            t = tile.GetComponent<Tile>();
            print(t);
            if (t != null && CanAttack)
            {
                print(t.name + "Reset");
                t.Reset();

            }
        }
        SpacialCommand = false;
        CanAttack = false;
        TurnManager.Instance.ReMoveAttackableEnemy();
        TurnManager.Instance.ReMoveAttackableBarrier();
    }
}
