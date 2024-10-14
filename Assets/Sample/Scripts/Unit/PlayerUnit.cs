using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : TacticSystem
{

    public GameObject actionCanves;
    public Button walkButton;
    public Button skill1Button;
    public Button skill2Button;

    public Image[] Heart;
    public Sprite fullhealth;
    public Sprite emptyhealth;

    public bool CanAttack;
    public bool SkyAttack;
    public int isAttack;

    public bool InRange;

    public int sphereCollider;

    public bool SpacialCommand;

    protected void OnClickWalkButton()
    {
        print("Walk");
        RemoveSelectableTile();
        if(Skill1Colider != null)
        {
            Skill1Colider.SetActive(false);
            objectsInColliderskill1.Clear();
        }
        if (Skill2Colider != null)
        {
            Skill2Colider.SetActive(false);
            objectsInColliderskill2.Clear();
        }
        TurnManager.Instance.ReMoveAttackableEnemy();
        TurnManager.Instance.ReMoveAttackableBarrier();
        CanAttack = false;
        isAttack = 0;
        if (!moving)
        {
            FindSelectableTilesWalk();
        }
        
        walkButton.interactable = false;
        CanAttack = false;
    }

    /*void Update()
    {
        if (!TurnManager.Instance.IsStartGame)
        {
            return;
        }
        if (TurnManager.Instance.PlayerTurn)
        {
            TurnManager.Instance.Endturnobject.SetActive(true);
            if (IsMyturn)
            {
                if(!IsShowSelect)
                {
                    string unitname = Unitname + " (Selected)";
                    UIManager.Instance.SetProfilePanel(unitname, ProfileImg, HpPoint, currentHp, currentstatus, statusUnit);
                    IsShowSelect = true;
                }
                
            }
            if (WalkStack == 0)
            {
                walkButton.interactable = false;
            }
            if (WalkStack == 0 && currentSkill1CD == Skill1CD && currentSkill2CD == Skill2CD)
            {
                IsMyturn = false;
            }
            if (!IsMyturn)
            {
                IsShowSelect = false;
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
        if(currentHp <= 0)
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
        if(currentSkill1CD == Skill1CD)
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
    }*/
    public IEnumerator WaitForDead()
    {
        Destroy(gameObject);
        print("Unit is Dead");
        yield return new WaitForSeconds(0);
        //Destroy(gameObject);

    }
    private void OnMouseUp()
    {
        
        if (!TurnManager.Instance.IsStartGame && !TurnManager.Instance.HaveLeader)
        {
            IsLeader = true;
            TurnManager.Instance.HaveLeader = true;
            UIManager.Instance.SetLeader(name);
            return;
        }
        if (!TurnManager.Instance.IsStartGame)
            return;
        TurnManager.Instance.AddCommandError();
        if (TurnManager.Instance.PlayerTurn)
        {
            print("Player Has Click");
            actionCanves.SetActive(true);
            IsMyturn = true;
        }

    }

    private void OnMouseDown()
    {
        if (!TurnManager.Instance.IsStartGame && TurnManager.Instance.HaveLeader)
        { 
            IsLeader = false;
            TurnManager.Instance.HaveLeader = false;
            return; 
        }
        if (TurnManager.Instance.PlayerTurn)
        {
            TurnManager.Instance.RemoveThisTurn();
            TurnManager.Instance.ResetTile();
        }
    }
    public void CheckMouse()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Tile") 
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if(t.selectable)
                    {
                        Debug.Log("move");
                        MovetoTile(t);
                        if (CMError)
                        {
                            if (SpacialCommand)
                            {
                                TurnManager.Instance.currentCMOpoint -= 0;
                            }
                            else
                            {
                                TurnManager.Instance.currentCMOpoint -= (CMOtoWalk * 2);

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
                                TurnManager.Instance.currentCMOpoint -= CMOtoWalk;

                            }
                        }
                        SpacialCommand = false;
                    }
                }
            }
        }
    }
    protected void OnClickSkill1Button()
    {
        RemoveSelectableTile();
        if (Skill1Colider != null)
        {
            Skill1Colider.SetActive(true);
            objectsInColliderskill1.Clear();
        }
        if (Skill2Colider != null)
        {
            Skill2Colider.SetActive(false);
            objectsInColliderskill2.Clear();
        }
        TurnManager.Instance.ReMoveAttackableEnemy();
        TurnManager.Instance.ReMoveAttackableBarrier();
        isAttack = 1;
        CanAttack = true;
        float sphereRadius = attackArea1;
        Vector3 sphereCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);


        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found on tile.");
                EnemyUnit enemy = hitCollider.GetComponent<EnemyUnit>();
                if (SkyAttack)
                {
                    enemy.attackable = true;
                }
                else if (!SkyAttack)
                {
                    if (enemy != null && !enemy.Inbarier)
                    {
                        print("CollectEnemy");
                        enemy.attackable = true;
                    }

                    else
                    {
                        enemy = hitCollider.GetComponent<EnemyUnit>();
                    }
                }

            }
            if (hitCollider.CompareTag("Barrier"))
            {
                Barrier barrier = hitCollider.GetComponent<Barrier>();
                barrier.InRangeAttack = true;
            }
            if (hitCollider.tag == "Tile")
            {
                Tile t = hitCollider.GetComponent<Tile>();
                if (t != null && !t.attackable)
                {
                    t.Reset();
                    Debug.Log("Skill");
                    t.attackselectable = true;
                }
            }

            //player
            if (hitCollider.tag == "Player")
            {
                PlayerUnit t = hitCollider.GetComponent<PlayerUnit>();
                if (t != null && !t.attackable)
                {
                    t.InRange = true;
                }
            }


        }
        walkButton.interactable = false;
    }
    protected void OnClickSkill2Button()
    {
        RemoveSelectableTile();
        if (Skill1Colider != null)
        {
            Skill1Colider.SetActive(false);
            objectsInColliderskill1.Clear();
        }

        if (Skill2Colider != null)
        {
            Skill2Colider.SetActive(true);
            objectsInColliderskill2.Clear();
        }
        TurnManager.Instance.ReMoveAttackableEnemy();
        TurnManager.Instance.ReMoveAttackableBarrier();
        CanAttack = true;
        isAttack = 2;
        float sphereRadius = attackArea2;
        Vector3 sphereCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);


        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found on tile.");
                EnemyUnit enemy = hitCollider.GetComponent<EnemyUnit>();
                if (SkyAttack)
                {
                    enemy.attackable = true;
                }
                else if (!SkyAttack)
                {
                    if (enemy != null && !enemy.Inbarier)
                    {
                        print("CollectEnemy");
                        enemy.attackable = true;
                    }

                    else
                    {
                        enemy = hitCollider.GetComponent<EnemyUnit>();
                    }
                }
                

            }
            if (hitCollider.CompareTag("Barrier"))
            {
                Barrier barrier = hitCollider.GetComponent<Barrier>();
                barrier.InRangeAttack = true;
            }
            if (hitCollider.tag == "Tile")
            {
                Tile t = hitCollider.GetComponent<Tile>();
                if (t != null && !t.attackable)
                {
                    t.Reset();
                    Debug.Log("Skill");

                    t.attackselectable = true;
                }
            }


        }
        walkButton.interactable = false;
    }

    /*public void CheckMouseAttack1()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Tile t;
            if (Physics.Raycast(ray, out hit))
            { 
                if( hit.collider.tag == "Barrier")
                {
                    Barrier barrier = hit.collider.GetComponent<Barrier>();
                    if(CanAttack && barrier.InRangeAttack)
                    {
                        
                        barrier.IsAttack();
                        Debug.Log("Attack");
                        currentSkill1CD = Skill1CD;
                        if (CMError)
                        {
                            TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill1 * 2);
                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill1;
                        }
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
                        CanAttack = false;
                        TurnManager.Instance.ReMoveAttackableEnemy();
                        TurnManager.Instance.ReMoveAttackableBarrier();
                    }
                }
                if (hit.collider.tag == "Enemy")
                {
                    EnemyUnit enemy = hit.collider.GetComponent<EnemyUnit>();
                    if (CanAttack && enemy.attackable)
                    {
                        enemy.currentHp -= 1;
                        Debug.Log("Attack");
                        currentSkill1CD = Skill1CD;
                        if (CMError)
                        {
                            TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill1 * 2);
                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill1;
                        }
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
                if (hit.collider.tag == "Barrier")
                {
                    
                    Barrier barrier = hit.collider.GetComponent<Barrier>();
                    if (CanAttack && barrier.InRangeAttack)
                    {
                        
                        barrier.IsAttack();
                        currentSkill2CD = Skill2CD;
                        if (CMError)
                        {
                            TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill2 * 2);

                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill2;

                        }
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
                        CanAttack = false;
                        TurnManager.Instance.ReMoveAttackableEnemy();
                        TurnManager.Instance.ReMoveAttackableBarrier();

                    }
                }
                if (hit.collider.tag == "Enemy")
                {
                    EnemyUnit enemy = hit.collider.GetComponent<EnemyUnit>();
                    if (CanAttack && enemy.attackable)
                    {
                        enemy.currentHp -= 2;
                        Debug.Log("Attack2");
                        currentSkill2CD = Skill2CD;
                        if (CMError)
                        {
                            TurnManager.Instance.currentCMOpoint -= (CMOtoUseSkill2 * 2 );

                        }
                        else
                        {
                            TurnManager.Instance.currentCMOpoint -= CMOtoUseSkill2;

                        }
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
                        CanAttack = false;
                        TurnManager.Instance.ReMoveAttackableEnemy();
                        TurnManager.Instance.ReMoveAttackableBarrier();

                    }
                }
            }
            

        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!IsLeader)
            {
                CMError = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!IsLeader)
            {
                CMError = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!IsLeader)
            {
                CMError = true;
            }
        }
    }
}
