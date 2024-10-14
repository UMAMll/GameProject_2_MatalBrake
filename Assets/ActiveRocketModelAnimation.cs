using UnityEngine;

public class ActiveRocketModelAnimation : MonoBehaviour
{
    public GameObject Walk_Idle, More;
    public bool CanIdle, Can_t_Idle;
    public Animator anim;
    private void Start()
    {
        if(anim != null)
        {
            if(CanIdle)
            {
                anim.SetTrigger("Idle");
            }
            else
            {
                return;
            }
        }
    }
    public void ActiveWalk_IdleModel()
    {
        
        //not ative
        More.SetActive(false);

        // active
        Walk_Idle.SetActive(true);
    }
    public void ActiveMoreModel() 
    {
        //not ative
        Walk_Idle.SetActive(false);

        // active
        More.SetActive(true);
    }
    
}
