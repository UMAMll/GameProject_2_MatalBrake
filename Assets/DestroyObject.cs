using UnityEngine;
using UnityEngine.UIElements;

public class DestroyObject : MonoBehaviour
{
    public GameObject Object;

    public void Destroy_Object()
    {
        Destroy(Object);
    }

    public void CanMove()
    {

        Healbot bot = Object.GetComponent<Healbot>();
        if (bot != null)
        {
            bot.currentWalkstack = 1;
            bot.currentSkill2CD = bot.Skill2CD;
        }

        Summonbot sumbot = Object.GetComponent<Summonbot>();
        if (sumbot != null)
        {
            print("not");
        }
        if (sumbot != null)
        {
            int randomObject = Random.Range(0, sumbot.summonObject.Length);
            int randompos = Random.Range(0, sumbot.summonTargetpos.Length);
            print("Summon");
            Instantiate(sumbot.summonObject[randomObject], sumbot.summonTargetpos[randompos].transform.position, Quaternion.identity);
            sumbot.currentWalkstack = 1;
            sumbot.currentSkill2CD = sumbot.Skill2CD;

        }


    }
}
