using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _chaseSpeed = 5;


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, _chaseSpeed * Time.deltaTime);
    }
}
