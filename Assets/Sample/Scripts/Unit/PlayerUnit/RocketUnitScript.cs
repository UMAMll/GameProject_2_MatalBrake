using UnityEngine;

public class RocketUnitScript : PlayerUnit
{
    
    private void Start()
    {
        GameObject Sound = GameObject.FindGameObjectWithTag("WalkSound");
        WalkSound = Sound.GetComponent<SoundManager>();
        GameObject es = GameObject.FindGameObjectWithTag("EffectSound");
        EffectSound = es.GetComponent<SoundManager>();

        IsRocket = true;
        idleGameobj.SetActive(true);
        moreGameobj.SetActive(false);
        actionCanves.SetActive(false);
        Init();
        CanAttack = false;
        walkButton.onClick.AddListener(OnClickWalkButton);
        skill1Button.onClick.AddListener(OnClickSkill1Button);
        skill2Button.onClick.AddListener(OnClickSkill2Button);
    }
    private void Update()
    {
        if (!IsMyturn)
        {
            actionCanves.SetActive(false);
        }
        if (!TurnManager.Instance.IsStartGame)
        {
            HPCanvas.SetActive(false);
            return;
        }

        if (TurnManager.Instance.IsStartGame)
        {
            CheckTurnUnit();
            HPCanvas.SetActive(true);
        }

        if (TurnManager.Instance.PlayerTurn)
        {
            TurnManager.Instance.Endturnobject.SetActive(true);
            if (IsMyturn)
            {
                if (!IsShowselect)
                {
                    string unitname = Unitname;
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
            if(moving)
            {
                idleGameobj.SetActive(true);
                moreGameobj.SetActive(false);
                IdleWalkanim.SetBool("Walk",true);
            }
            if(!moving)
            {
                IdleWalkanim.SetBool("Walk",false);
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
            moreGameobj.SetActive(true);
            idleGameobj.SetActive(false);
            actionCanves.SetActive(false);
            Moreanim.SetTrigger("Die");
            TurnManager.Instance.playerunit.Remove(gameObject);
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            objectsInColliderskill1.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            objectsInColliderskill1.Remove(other.gameObject);
        }
    }
    public void CheckMouseAttack1()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (CanAttack)
                    {
                        if (GunflashEffect != null)
                        {
                            GunflashEffect.Play();
                        }
                        moreGameobj.SetActive(true);
                        idleGameobj.SetActive(false);
                        Moreanim.SetTrigger("Attack1");
                        //correct Enemy
                        foreach (var target in TurnManager.Instance.EnemyUnits)
                        {
                            EnemyUnit enemy = target.GetComponent<EnemyUnit>();
                            if (enemy != null)
                            {
                                
                                if (enemy.attackable)
                                {
                                    enemy.currentHp -= skill1Damage;
                                    enemy.IsBoomHit();
                                }
                            }
                        }
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
                            if (t != null && CanAttack)
                            {
                                t.Reset();
                                t.CheckTile();

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
                if (hit.collider.tag == "Barrier")
                {
                    if (GunflashEffect != null)
                    {
                        GunflashEffect.Play();
                    }
                    if(EffectSound != null)
                    {
                        EffectSound.ExplosionSound();
                    }
                    Barrier barrier = hit.collider.GetComponent<Barrier>();
                    if (CanAttack && barrier.InRangeAttack)
                    {
                        barrier.Boom.Play();
                        barrier.IsAttack();
                        transform.LookAt(barrier.gameObject.transform.position);
                        moreGameobj.SetActive(true);
                        idleGameobj.SetActive(false);
                        Moreanim.SetTrigger("Attack1");
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
                            if (t != null && CanAttack)
                            {
                                t.Reset();
                                t.CheckTile();
                            }
                        }
                        SpacialCommand = false;
                        CanAttack = false;
                        TurnManager.Instance.ReMoveAttackableEnemy();
                        TurnManager.Instance.ReMoveAttackableBarrier();
                    }
                }
                if (hit.collider.tag == "Enemy")
                {
                    if (GunflashEffect != null)
                    {
                        GunflashEffect.Play();
                    }
                    EnemyUnit enemy = hit.collider.GetComponent<EnemyUnit>();
                    if (CanAttack && enemy.attackable)
                    {
                        enemy.currentHp -= skill2Damage;
                        enemy.IsBoomHit();
                        transform.LookAt(enemy.gameObject.transform.position);
                        moreGameobj.SetActive(true);
                        idleGameobj.SetActive(false);
                        Moreanim.SetTrigger("Attack1");
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
                            if (t != null && CanAttack)
                            {
                                t.Reset();
                                t.CheckTile();

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
}
