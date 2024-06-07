using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    // SHOULD COME FROM PLAYER STATS
    #region fields
    private Vector3 _currentTouchDirection;
    private Vector3 _previousTouchDirection;
    private Vector3 _currentTouchCharacterLocalDirection;
    private Vector3 _previousTouchCharacterLocalDirection;
    private TransformData characterTransformOnDragStarted;
    public static Action OnDragStarted;
    #endregion


    #region mb callbacks
    public override void Start()
    {
        base.Start();
        OnDragStarted += HandleOnDragStarted;
    }


    public override void Update()
    {
        base.Update();

        HandleMovement();
    }
    #endregion

    #region locomotion methods
    private void HandleMovement()
    {
        if (PlayerInputManager.Instance.DragStarted)
        {
            SetRotation();
            MoveForward();
        }
    }

    private Quaternion CalculateRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        Vector3 targetEulerAngles = targetRotation.eulerAngles;
        targetEulerAngles.x = 0;
        targetRotation = Quaternion.Euler(targetEulerAngles);
        return targetRotation;
    }
    private Vector3 CalculateDirection()
    {
        //characterTransformOnDragStarted = new TransformData(transform.position, transform.rotation, transform.localScale); // call once when drag started
        _currentTouchDirection = (PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp).normalized;
        _currentTouchDirection.z = _currentTouchDirection.y;
        _currentTouchDirection.y = 0;


        _currentTouchCharacterLocalDirection = Camera.main.transform.TransformDirection(_currentTouchDirection); //characterTransformOnDragStarted
        //Debug.Log("_currentTouchCharacterLocalDirection : " + _currentTouchCharacterLocalDirection);
        if (_currentTouchDirection != _previousTouchDirection)
        {
            //_currentTouchCharacterLocalDirection = transform.TransformDirection(_currentTouchDirection);
            _previousTouchDirection = _currentTouchDirection;
            _previousTouchCharacterLocalDirection = _currentTouchCharacterLocalDirection;
            //Debug.Log("if state");
            return _currentTouchCharacterLocalDirection;
        }
        else
        {
            return _previousTouchCharacterLocalDirection;
        }
    }

    private void SetRotation()
    {
        // Rotasyonu hesapla ve karakterin rotasyonunu buna göre güncelle
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), GetRotationSpeed() * Time.deltaTime);
    }

    private void MoveForward()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * _movementSpeed);
        GetCharacterManager().characterController.Move(transform.forward * GetMovementSpeed() * Time.deltaTime);
    }

    private void HandleOnDragStarted()
    {
        characterTransformOnDragStarted = new TransformData(Camera.main.transform.position, Camera.main.transform.rotation, transform.localScale); //TO MOVE CAMERA FORWARD
    }
    #endregion
}


[System.Serializable]
public class TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    // Kaydedilen transform bilgilerini kullanarak dünya yönüne dönüştür
    public Vector3 TransformDirection(Vector3 direction)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(Position, Rotation, Scale);
        return matrix.MultiplyVector(direction);
    }
}
