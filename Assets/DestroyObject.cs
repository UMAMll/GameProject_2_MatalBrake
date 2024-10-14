using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject Object;

    public void Destroy_Object()
    {
        Destroy(Object);
    }
}
