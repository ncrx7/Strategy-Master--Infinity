using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterLocomotionManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    [SerializeField] private float _unitSpeed;

    private Vector3 _moveDirection;

    #region MB CALLBACKS
    private void Awake()
    {
        SetMoveDirection();
    }
    #endregion

    private void SetMoveDirection()
    {
        if (_unitCharacterManager.CharacterOwnerType == CharacterOwnerType.PLAYER_UNIT)
        {
            _moveDirection = new Vector3(0, 0, 1);
        }
        else if (_unitCharacterManager.CharacterOwnerType == CharacterOwnerType.ENEMY_UNIT)
        {
            _moveDirection = new Vector3(0, 0, -1);
        }
        else
        {
            Debug.LogWarning("CHARACTER HAS UNDEFINED OWNER TYPE");
        }
    }

    public void MoveForward()
    {
        _unitCharacterManager.characterController.Move(_moveDirection * _unitSpeed * Time.deltaTime);
    }

    public void SetRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
        transform.rotation = targetRotation;
    }
}
