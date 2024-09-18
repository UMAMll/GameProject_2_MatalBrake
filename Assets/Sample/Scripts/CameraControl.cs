using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Camera MainCamera;
    public CinemachineVirtualCamera[] VirtualCameraTurnLeft;
    public CinemachineVirtualCamera[] VirtualCameraTurnRight;
    public int virtualnow = 0;
    int i;
    public float speed;
    public float ZoomSpeed;
    public float maxZoom;
    public float minZoom;
    void Start()
    {
        if (MainCamera == null)
        {
            MainCamera = GetComponentInChildren<Camera>();
        }
        VirtualCameraTurnRight[0].Priority = 1;
        
    }
    void Update()
    {
        if(MainCamera== null)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

       
        if (VirtualCameraTurnLeft[1].Priority == 1 || VirtualCameraTurnRight[3].Priority == 1 || VirtualCameraTurnLeft[2].Priority == 1 || VirtualCameraTurnRight[2].Priority == 1)
        {
            Vector3 movementspacial = new Vector3(-moveHorizontal, moveVertical, 0);
            transform.Translate(movementspacial * speed * Time.deltaTime);
        }
        else
        {
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            transform.Translate(movement * speed * Time.deltaTime);

        }

        if (scrollInput != 0)
        {
            foreach (CinemachineVirtualCamera t in VirtualCameraTurnLeft)
            {
                t.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= scrollInput * ZoomSpeed;
                if (t.m_Lens.OrthographicSize >= maxZoom)
                {
                    t.m_Lens.OrthographicSize = maxZoom;
                }
                if (t.m_Lens.OrthographicSize <= minZoom)
                {
                    t.m_Lens.OrthographicSize = minZoom;
                }

            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i -= 1;
            int j = 0;
            if (i <= 0)
            {
                j = -i % (VirtualCameraTurnLeft.Length);
            }
            else
            {
                j = i % (VirtualCameraTurnLeft.Length);
            }
            int k = (j + 1) % VirtualCameraTurnLeft.Length;
            int h = (j + 2) % VirtualCameraTurnLeft.Length;
            int w = (j + 3) % VirtualCameraTurnLeft.Length;
            VirtualCameraTurnLeft[j].Priority = 1;
            VirtualCameraTurnLeft[k].Priority = 0;
            VirtualCameraTurnLeft[h].Priority = 0;
            VirtualCameraTurnLeft[w].Priority = 0;
            print("i" + i.ToString() + "j" + j.ToString() + "k" + k.ToString() + "h" + h.ToString() + "w" + w.ToString());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            i++;
            int j = 0;
            if (i <= 0)
            {
                j = -i % (VirtualCameraTurnRight.Length);
            }
            else
            {
                j = i % (VirtualCameraTurnRight.Length);
            }
            
            int k = (j + 1) % VirtualCameraTurnRight.Length;
            int h = (j + 2) % VirtualCameraTurnRight.Length;
            int w = (j + 3) % VirtualCameraTurnRight.Length;
            VirtualCameraTurnRight[j].Priority = 1;
            VirtualCameraTurnRight[k].Priority = 0;
            VirtualCameraTurnRight[h].Priority = 0;
            VirtualCameraTurnRight[w].Priority = 0;
            print("i" + i.ToString() + "j" + j.ToString() + "k" + k.ToString() + "h" + h.ToString() + "w" + w.ToString());
            
        }
    }
}
