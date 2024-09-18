using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillbordScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _Camera;

    private void Start()
    {
        if(_camera == null)
        {
            _camera = FindObjectOfType<Camera>();
            _Camera = _camera.GetComponent<Transform>();
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _Camera.forward);
    }
}
