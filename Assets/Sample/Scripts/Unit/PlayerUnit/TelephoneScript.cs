using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneScript : PlayerUnit
{
    [SerializeField] GameObject healObject;
    [SerializeField] GameObject healObjectModel;

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
    public void CheckMouseAttack1()
    {
        print("Attack1");
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        
        if(Physics.Raycast(r, out h))
        {
            if(h.collider.tag == "Tile")
            {
                Tile target = h.collider.GetComponent<Tile>();
                Vector3 position = new Vector3(target.transform.position.x, 1.5f, target.transform.position.z);
                healObjectModel.SetActive(true);
                healObjectModel.transform.position = position;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            healObjectModel.SetActive(false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile target = hit.collider.GetComponent<Tile>();
                    if (CanAttack)
                    {
                        //spawnObject
                        Vector3 position = new Vector3(target.transform.position.x, 1.5f, target.transform.position.z);
                        SpawnObject(position);

                        currentSkill1CD = Skill1CD;
                        objectsInColliderskill1.Clear();
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
    }
    public void CheckMouseAttack2()
    {

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    PlayerUnit player = hit.collider.GetComponent<PlayerUnit>();
                    if (CanAttack )
                    {

                        player.SpacialCommand = true;
                        player.IsPowerUp();
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
    }

    private void SpawnObject(Vector3 position)
    {
        Instantiate(healObject,position, Quaternion.identity);
    }
}
