using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraControl : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _cameraLerp;

    private void LateUpdate()
    {
        HandleFollowTarget();
    }

    private void HandleFollowTarget()
    {
        float newXPos = Mathf.Lerp(transform.position.x, _targetTransform.transform.position.x, _cameraLerp);
        Vector3 newPos = new Vector3(newXPos, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
}
