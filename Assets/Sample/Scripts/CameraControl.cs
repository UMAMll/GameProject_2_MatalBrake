using Cinemachine;
using UnityEngine;

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

    public Vector2 minpos;
    public Vector2 maxpos;

    public Transform currenttransfrom;
    void Start()
    {
        if (MainCamera == null)
        {
            MainCamera = GetComponentInChildren<Camera>();
        }
        VirtualCameraTurnRight[0].Priority = 1;
        BlockCameraPos camnow = VirtualCameraTurnLeft[0].GetComponent<BlockCameraPos>();
        maxpos = camnow.maxposition;
        minpos = camnow.minposition;

    }
    void Update()
    {
        if (MainCamera == null)
            return;
        if (TurnManager.Instance.EnemyTurn)
        {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");


        if (VirtualCameraTurnLeft[1].Priority == 1 || VirtualCameraTurnRight[3].Priority == 1 || VirtualCameraTurnLeft[2].Priority == 1 || VirtualCameraTurnRight[2].Priority == 1)
        {
            Vector3 movementspacial = new Vector3(-moveHorizontal, moveVertical, 0);
            transform.Translate(movementspacial * speed * Time.unscaledDeltaTime);
            
            if(transform.position.x >= maxpos.x)
            {
                transform.position = new Vector3(maxpos.x,transform.position.y,transform.position.z);
            }
            if(transform.position.x <= minpos.x)
            {
                transform.position = new Vector3(minpos.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y >= maxpos.y)
            {
                transform.position = new Vector3(transform.position.x, maxpos.y, transform.position.z);
            }
            if (transform.position.y <= minpos.y)
            {
                transform.position = new Vector3(transform.position.x, minpos.y, transform.position.z);
            }
        }
        else
        {
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            transform.Translate(movement * speed * Time.unscaledDeltaTime);
            
            if (transform.position.x >= maxpos.x)
            {
                transform.position = new Vector3(maxpos.x, transform.position.y, transform.position.z);
            }
            if (transform.position.x <= minpos.x)
            {
                transform.position = new Vector3(minpos.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y >= maxpos.y)
            {
                transform.position = new Vector3(transform.position.x, maxpos.y, transform.position.z);
            }
            if (transform.position.y <= minpos.y)
            {
                transform.position = new Vector3(transform.position.x, minpos.y, transform.position.z);
            }
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
            BlockCameraPos camnow = VirtualCameraTurnLeft[j].GetComponent<BlockCameraPos>();
            maxpos = camnow.maxposition;
            minpos = camnow.minposition;
            VirtualCameraTurnLeft[k].Priority = 0;
            VirtualCameraTurnLeft[h].Priority = 0;
            VirtualCameraTurnLeft[w].Priority = 0;
            print("i" + i.ToString() + "j" + j.ToString() + "k" + k.ToString() + "h" + h.ToString() + "w" + w.ToString());
        }
        if (Input.GetKeyDown(KeyCode.E))
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
            BlockCameraPos camnow = VirtualCameraTurnRight[j].GetComponent<BlockCameraPos>();
            maxpos = camnow.maxposition;
            minpos = camnow.minposition;
            VirtualCameraTurnRight[k].Priority = 0;
            VirtualCameraTurnRight[h].Priority = 0;
            VirtualCameraTurnRight[w].Priority = 0;
            print("i" + i.ToString() + "j" + j.ToString() + "k" + k.ToString() + "h" + h.ToString() + "w" + w.ToString());

        }

        for (int i = 0; i < VirtualCameraTurnLeft.Length; i++)
        {

            if (VirtualCameraTurnLeft[i].Priority == 1)
            {

                if (TurnManager.Instance.EnemyTurn)
                {
                    //VirtualCameraTurnLeft[i].LookAt = TurnManager.Instance.TurnEnemy();
                    VirtualCameraTurnLeft[i].Follow = TurnManager.Instance.TurnEnemy();

                }
                else if (TurnManager.Instance.PlayerTurn)
                {
                    // VirtualCameraTurnLeft[i].LookAt = TurnManager.Instance.TurnEnemy();
                    VirtualCameraTurnLeft[i].Follow = TurnManager.Instance.TurnEnemy();
                }


            }

        }
        //CheckVisibleObjects();
    }
    void CheckVisibleObjects()
    {
        float viewDistance = 60;
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewDistance);
        foreach (var collider in hitColliders)
        {

            if (collider.CompareTag("BlockCam"))
            {

                Debug.Log("Visible Object:" + collider.gameObject.name);
            }
        }
    }

}