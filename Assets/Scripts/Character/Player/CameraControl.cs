using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _chaseSpeed = 5;
    //[SerializeField] float _leftAndRightRotationSpeed = 220;

    //COLLISION FIELDS
    [Header("Collision Fields")]
    private float _mainCameraYPosition;
    private float _targetCameraYPosition;
    private float _targetCameraZPosition;
    private float _mainCameraZPosition;
    public Camera CameraObject;
    [SerializeField] Transform _cameraPivotTransform;
    [SerializeField] float _cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask _collideWithLayers;
    private Vector3 _cameraObjectPosition;

    private void Start()
    {
        _mainCameraYPosition = CameraObject.transform.localPosition.y;
        _mainCameraZPosition = CameraObject.transform.localPosition.z;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, _chaseSpeed * Time.deltaTime);
        HandleCameraRotation();
        HandleCameraCollision();
    }

    /*     private Vector3 CalculateDirection()
        {
            Vector3 dir = (PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp).normalized;
            dir.z = dir.y;
            dir.y = 0;
            //Debug.Log(dir);
            return dir;
        } */
    private void HandleCameraRotation()
    {
        if (PlayerInputManager.Instance.DragStarted)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, 1 * Time.deltaTime);
        }
    }

    private void HandleCameraCollision()
    {
        _targetCameraYPosition = _mainCameraYPosition;
        _targetCameraZPosition = _mainCameraZPosition;
        RaycastHit hit;
        Vector3 direction = -_cameraPivotTransform.transform.up;
        direction.Normalize();

        if (Physics.SphereCast(_cameraPivotTransform.position, _cameraCollisionRadius, direction, out hit, 2, _collideWithLayers))
        {
            _targetCameraYPosition = - 3.3f;
            _targetCameraZPosition = -2.5f;
        }

        _cameraObjectPosition.y = Mathf.Lerp(CameraObject.transform.localPosition.y, _targetCameraYPosition, 0.05f);
        _cameraObjectPosition.z = Mathf.Lerp(CameraObject.transform.localPosition.z, _targetCameraZPosition, 0.05f);;
        CameraObject.transform.localPosition = _cameraObjectPosition;
    }
}