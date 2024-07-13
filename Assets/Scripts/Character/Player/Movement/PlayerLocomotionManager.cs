using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    // SHOULD COME FROM PLAYER STATS
    //[SerializeField] private CharacterAnimationManager _characterAnimationManager;
    [SerializeField] private PlayerManager _playerManager;

    #region fields
    [SerializeField] private LocomotionMode _currentLocomotionMode;
    private Vector3 _currentTouchDirection;
    private Vector3 _previousTouchDirection;
    private Vector3 _currentTouchCharacterLocalDirection;
    private Vector3 _previousTouchCharacterLocalDirection;
    private TransformData characterTransformOnDragStarted;
    public static Action OnDragStarted;

    [Header("Arena field")]
    [SerializeField] private float _maxXPos;
    [SerializeField] private float _minXPos;
    #endregion


    #region mb callbacks
    public override void Start()
    {
        base.Start();
        OnDragStarted += HandleOnDragStarted;

        EventSystem.OnPlayerDefeat += DisableInstance;
        EventSystem.OnTimeOutForEvolutionPhase += DisableInstance;
    }

    private void OnDisable()
    {
        EventSystem.OnPlayerDefeat -= DisableInstance;
        EventSystem.OnTimeOutForEvolutionPhase -= DisableInstance;
    }

    public override void Update()
    {
        base.Update();

        /*         if (_playerManager.isDead || _playerManager.isVictory)
                    return; */

        HandleMovement();
        //Debug.Log("locomotion update");
    }
    #endregion

    #region locomotion methods
    private void HandleMovement()
    {
        if (PlayerInputManager.Instance.DragStarted)
        {
            switch (_currentLocomotionMode)
            {
                case LocomotionMode.STRATEGY_PHASE:
                    //EventSystem.UpdateAnimatorParameter?.Invoke(CharacterAnimatorType.PLAYER_ANIMATOR, AnimatorParameterType.FLOAT, "vertical", 1f, 0, false);
                    _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.FLOAT, "vertical", 1f, 0, false);
                    SetRotation();
                    MoveForward();
                    break;
                case LocomotionMode.ARENA_PHASE:
                    if (_currentTouchDirection.x < 0)
                    {
                        _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.FLOAT, "horizontal", -1f, 0, false);
                    }
                    else if (_currentTouchDirection.x > 0)
                    {
                        _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.FLOAT, "horizontal", 1f, 0, false);
                    }
                    HandleMoveHorizontal();
                    break;
                default:
                    break;
            }
        }
        else
        {
            //EventSystem.UpdateAnimatorParameter?.Invoke(CharacterAnimatorType.PLAYER_ANIMATOR, AnimatorParameterType.FLOAT, "vertical", 0f, 0, false);
            _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.FLOAT, "vertical", 0f, 0, false);
            _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.FLOAT, "horizontal", 0f, 0, false);
        }
    }

    private void HandleMoveHorizontal()
    {
        _currentTouchDirection = (PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp).normalized;
        _currentTouchDirection.y = 0;
        _currentTouchDirection.z = 0;

        Vector3 movement = _currentTouchDirection * GetMovementSpeed() * Time.deltaTime;

        Vector3 nextPosition = transform.position + movement;

        if (nextPosition.x < _minXPos)
        {
            movement.x = _minXPos - transform.position.x;
        }
        else if (nextPosition.x > _maxXPos)
        {
            movement.x = _maxXPos - transform.position.x;
        }

        GetCharacterManager().characterController.Move(movement);
        Debug.Log("movement: " + movement);

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
        if (this == null)
            return;

        characterTransformOnDragStarted = new TransformData(Camera.main.transform.position, Camera.main.transform.rotation, transform.localScale); //TO MOVE CAMERA FORWARD
    }
    #endregion

    private void DisableInstance()
    {
        this.enabled = false;
    }
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

public enum LocomotionMode
{
    STRATEGY_PHASE,
    ARENA_PHASE
}
