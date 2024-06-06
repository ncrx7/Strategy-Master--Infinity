using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _chaseSpeed = 5;
    [SerializeField] float _leftAndRightLookAngle;
    [SerializeField] float _leftAndRightRotationSpeed = 220;
    private float _cameraXDirection;


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, _chaseSpeed * Time.deltaTime);
        //HandleCameraRotation();
        hndcmr();
    }

    private void HandleCameraRotation()
    {
        CalculateDirection();
        Debug.Log("x dir" + _cameraXDirection);
        _leftAndRightLookAngle += _cameraXDirection * _leftAndRightRotationSpeed * Time.deltaTime;

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;
        //Rotate this game object left and right
        cameraRotation.y = _leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

    }

    private void CalculateDirection()
    {
        Vector3 dir = PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp;
        dir.Normalize();
        _cameraXDirection = dir.x;

        if (!PlayerInputManager.Instance.DragStarted)
        {
            _cameraXDirection = 0f;
        }
    }

    private Vector3 CalculateDir()
    {
        Vector3 dir = (PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp).normalized;
        dir.z = dir.y;
        dir.y = 0;
        //Debug.Log(dir);
        return dir;
    }
    private void hndcmr()
    {
        if (PlayerInputManager.Instance.DragStarted)
        {
            Quaternion targetRotation = Quaternion.LookRotation(CalculateDir(), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        }
    }
}
