using System.Collections.Generic;
using UnityEngine;

public class TacticSystem : MonoBehaviour
{
    public SoundManager WalkSound;
    public SoundManager EffectSound;

    public GameObject HPCanvas;

    List<Tile> Selectabletiles = new List<Tile>();
    public GameObject[] tiles;
    
    Stack<Tile> path = new Stack<Tile>();
    Tile currenttile;


    public bool moving = false;
    public bool attacking = false;

    public string Unitname;
    public Sprite ProfileImg;
    public string statusUnit;
    public string Unittype,UnittypeSkill2;
    public string unittargettype,unittargettypeskill2;

    public int movearea;
    public int LeaderArea;
    public float movespeed;
    public int HpPoint;
    public int currentHp;
    public int currentstatus;

    public float attackArea1;
    public float attackArea2;

    Rigidbody rb;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfheight = 0;

    public int WalkStack;
    public int currentWalkstack;
    public int skill1Damage;
    public int skill2Damage;
    public int Skill1CD;
    public int Skill2CD;
    public int currentSkill1CD;
    public int currentSkill2CD;
    public int CMOtoWalk;
    public int CMOtoUseSkill1;
    public int CMOtoUseSkill2;

    public bool IsMyturn;
    public bool IsLeader;
    public bool CMError;

    public bool attackable;
    public bool Inbarier;

    public bool IsShowselect;
    public GameObject Skill1Colider;
    public List<GameObject> objectsInColliderskill1 = new List<GameObject>();
    public GameObject Skill2Colider;
    public List<GameObject> objectsInColliderskill2 = new List<GameObject>();

    public Tile actualTargetTile;

    [Header("Effect")]
    public ParticleSystem HitEffect;
    public ParticleSystem BoomEffect, HealEffect, PowerUpEffect, MyTurnEffect;
    bool isplay;

    [Header("Animation")]
    public Animator animator;

    [Header("RocketAnimation")]
    public bool IsRocket;
    public Animator IdleWalkanim, Moreanim;
    public GameObject idleGameobj, moreGameobj;
    protected void Init()
    {
        
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfheight = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        currentWalkstack = WalkStack;

    }

    
    public void IsHit()
    {
        HitEffect.Play();
        if(animator != null)
        {
            animator.SetTrigger("Hurt");

        }
        if (animator == null && IsRocket)
        {
            moreGameobj.SetActive(true);
            idleGameobj.SetActive(false);
            Moreanim.SetTrigger("Hurt");
        }
        if(EffectSound != null)
        {
            EffectSound.HurtSound();
        }
    }
    public void CheckTurnUnit()
    {
        
        if (IsMyturn)
        {
            if (!isplay)
            {
                MyTurnEffect.gameObject.SetActive(true);
                MyTurnEffect.Play();
                isplay = true;
            }

        }
        if (!IsMyturn)
        {
            if(isplay)
            {
                MyTurnEffect.Stop();
                MyTurnEffect.gameObject.SetActive(false);
                isplay = false;
            }
            
        }
    }

    public void IsBoomHit()
    {
        BoomEffect.Play();
        if(EffectSound != null)
        {
            EffectSound.ExplosionSound();
        }
        if(animator != null)
        {
            animator.SetTrigger("Hurt");

        }
        if (animator == null && IsRocket)
        {
            moreGameobj.SetActive(true);
            idleGameobj.SetActive(false);
            Moreanim.SetTrigger("Hurt");
        }
    }
    public void IsHeal()
    {
        HealEffect.Play();
        if (EffectSound != null)
        {
            EffectSound.PowerUpSound();
        }
    }
    public void IsPowerUp()
    {
        PowerUpEffect.Play();
        if (EffectSound != null)
        {
            EffectSound.PowerUpSound();
        }
    }

    public void GetCurrentTile()
    {
        currenttile = GetTargetTile(gameObject);
        if (currenttile == null)
        {
            currenttile = GetTargetTile(gameObject);
        }
        else
        {
            currenttile.current = true;
        }

    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }
    
    public void ComputeAdjecencyLists(Tile target)
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.Findneighbors(0, target);
        }
    }
    
    public void FindSelectableTilesWalk()
    {
        ComputeAdjecencyLists(null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currenttile);
        if (currenttile == null)
        {
            currenttile = GetTargetTile(gameObject);
        }
        else
        {
            currenttile.visited = true;
        }
        
        while (process.Count > 0)
        {

            Tile t = process.Dequeue();

            Selectabletiles.Add(t);
            if(t != null)
            {
                if (TurnManager.Instance.PlayerTurn)
                {
                    t.selectable = true;
                }
                if (t.distance < movearea)
                {
                    foreach (Tile tile in t.adjacencyList)
                    {
                        if (!tile.visited)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = 1 + t.distance;
                            process.Enqueue(tile);
                        }
                    }
                }
                if (TurnManager.Instance.PlayerTurn)
                {
                    t.CheckTile();

                }

            }
            
        }
    }
    
    public void MovetoTile(Tile tile)
    {
        path.Clear();

        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
        tile.CheckTile();

    }

    public void Move()
    {
        if(path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfheight + t.GetComponent<Collider>().bounds.extents.y;
            
            if(Vector3.Distance(transform.position, target) > 0.05f)
            {
                CaculateHeading(target);
                SetHorizontalVelocity();
                transform.LookAt(target);
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                if(animator != null)
                {
                    animator.SetBool("Walk", true);
                }
                if (WalkSound != null)
                {
                    WalkSound.PlayWalkSound();
                }

                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTile();
            //TurnManager.Instance.currentCMOpoint--;
            currentWalkstack--;
            moving = false;
            if(animator != null)
            {
                animator.SetBool("Walk", false);
            }
            if(WalkSound != null)
            {
                WalkSound.StopSoundLoop();
            }
        }

    }

    protected void RemoveSelectableTile()
    {
        print("ReMove");
        if(currenttile != null)
        {
            currenttile.current = false;
            currenttile = null;
        }

        foreach(Tile tile in Selectabletiles)
        {
            if (tile != null)
            {
                tile.Reset();
                tile.CheckTile();

            }

        }

        Selectabletiles.Clear();
        foreach (GameObject t in tiles)
        {
            if (t != null)
            {
                t.GetComponent<Tile>().Reset();
                t.GetComponent<Tile>().CheckTile();

            }
        }

    }

    void CaculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }
    void SetHorizontalVelocity()
    {
        velocity = heading * movespeed;
    }

    private void OnMouseEnter()
    {
        if(!TurnManager.Instance.IsStartGame)
        {
            return;
        }
        print("MouseEnter");
        if (SearchMode.Instance.searchmode)
        {
            SearchMode.Instance.uipanel.gameObject.SetActive(true);
            if (Skill1CD != 0)
            {
                SearchMode.Instance.UISet1Skill(ProfileImg, Unitname, movearea.ToString(), Unittype, skill1Damage.ToString(), attackArea1.ToString(), unittargettype, Skill1CD.ToString());
            }
            else if (Skill2CD == 0)
            {
                SearchMode.Instance.UISet1Skill(ProfileImg, Unitname, movearea.ToString(), Unittype, "-", "-", "-", "-");

            }
            if (Skill2CD != 0)
            {
                SearchMode.Instance.UISet2Skill(UnittypeSkill2, skill2Damage.ToString(), attackArea2.ToString(), unittargettypeskill2, Skill2CD.ToString());
            }
            else if(Skill2CD == 0)
            {
                SearchMode.Instance.UISet2Skill(UnittypeSkill2, "-", "-", "-", "-");

            }
        }
    }

    //Enemy
    /*protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if(t.f < lowest.f)
            {
                print("222");
                lowest = t;
                
            }
            
        }
        list.Remove(lowest);

        print("lowest = " + lowest);
        return lowest;
    }
    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();
        print("EndTile function");
        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }
        print("EndTile function 2");
        if (tempPath.Count >= movearea)
        {
            print("T Parent = " + t.parent);
            return t.parent;
        }

        print("EndTile 22");
        Tile endTile = null;
        for (int i = 0; i <= movearea; i++)
        {
            endTile = tempPath.Pop();
        }
        print("EndTile 222");

        print("EndTile = " + endTile);
        
        return endTile;
    }
    
    protected void FindPath(Tile target)
    {
        ComputeAdjecencyLists(target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currenttile);

        //currenttile.parent = ??
        currenttile.h = Vector3.Distance(currenttile.transform.position, target.transform.position);
        currenttile.f = currenttile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);
            
            actualTargetTile = FindEndTile(target);
            
            print("Tile = " + t);
            closedList.Add(t);
            print("Target" + target);
            if (t == target)
            {
                print("1");
                actualTargetTile = FindEndTile(t);
                MovetoTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {

                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform .position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                    
                }


            }
            
        }
    }*/
    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }
    protected Tile FindHighestF(List<Tile> list)
    {
        Tile highest = list[0];

        foreach (Tile t in list)
        {
            if (t.f > highest.f)
            {
                highest = t;
            }
        }
        list.Remove(highest);

        return highest;
    }
    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= movearea)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= movearea; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }
    protected void FindPathWithHighest(Tile target)
    {
        ComputeAdjecencyLists(target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currenttile);
        //currentTile.parent = ??
        currenttile.h = Vector3.Distance(currenttile.transform.position, target.transform.position);
        currenttile.f = currenttile.h;

        while (openList.Count > 0)
        {
            Tile t = FindHighestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MovetoTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }


    }
    protected void FindPath(Tile target)
    {
        ComputeAdjecencyLists(target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currenttile);
        //currentTile.parent = ??
        
        currenttile.h = Vector3.Distance(currenttile.transform.position, target.transform.position);
        currenttile.f = currenttile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MovetoTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }


    }

    // Show UI
    private void OnMouseOver()
    {
        if (!TurnManager.Instance.IsStartGame)
        {
            return;
        }
        print("MouseEnter");
        if (SearchMode.Instance.searchmode)
        {
            SearchMode.Instance.uipanel.gameObject.SetActive(true);
            SearchMode.Instance.UISet1Skill(ProfileImg, Unitname, movearea.ToString(), Unittype, skill1Damage.ToString(), attackArea1.ToString(), unittargettype, Skill1CD.ToString());
            if (Skill2CD != 0)
            {
                SearchMode.Instance.UISet2Skill(UnittypeSkill2, skill2Damage.ToString(), attackArea2.ToString(), unittargettypeskill2, Skill2CD.ToString());
            }
            else if (Skill2CD == 0)
            {
                SearchMode.Instance.UISet2Skill(UnittypeSkill2, "-", "-", "-", "-");

            }
        }
    }

    private void OnMouseExit()
    {
        if (!TurnManager.Instance.IsStartGame)
        {
            return;
        }
        print("MouseExit");
        SearchMode.Instance.uipanel.gameObject.SetActive(false);

    }

    
}
