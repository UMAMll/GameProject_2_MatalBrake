using UnityEngine;

public class RanDomSpacialAnimation : MonoBehaviour
{
    public Animator animator;

    public GameObject Charector;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void RandomforSpacialAnimation()
    {
        int num;
        num = UnityEngine.Random.Range(0, 5);
        print(num);
        if (num == 1)
        {
            animator.SetTrigger("IdleSp");
        }
    }
    public void stoppost()
    {
        if (animator != null)
        {
            animator.SetBool("Post", false);
        }
    }
    public void DestroyUnitDie()
    {
        TurnManager.Instance.playerunit.Remove(Charector);
        print("Die");
        Destroy(Charector);
    }
}
