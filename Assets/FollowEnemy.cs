using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    public Transform parentTransform;

    private void Start()
    {
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        if(cam != null){ cam.Priority = 0; }
    }
    void Update()
    {
        if (cam != null)
        {
            if (TurnManager.Instance.EnemyTurn)
            {
                cam.Priority = 10;

                parentTransform = TurnManager.Instance.TurnEnemy();

            }
            else if (TurnManager.Instance.PlayerTurn)
            {
                cam.Priority = 0;
            }

        }

        if(parentTransform != null)
        {

            transform.position = parentTransform.position;
            transform.rotation = transform.rotation;
            transform.localScale = transform.localScale;
        }
    }
}
