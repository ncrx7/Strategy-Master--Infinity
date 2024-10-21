using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UnitCharacterLocomotionManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    [SerializeField] private float _unitSpeed;
    [SerializeField] Transform _playerUnitSpawnPosition;
    [SerializeField] Transform _enemyUnitSpawnPosition;

    [SerializeField] private Vector3 _moveDirection;
    public static Action<UnitCharacterLocomotionManager> OnUnitCharacterSpawned;

    #region MB CALLBACKS
    private void Awake()
    {
        OnUnitCharacterSpawned?.Invoke(this);
    }



    private async void OnEnable()
    {
        await SetMoveDirection();
        await SetRotation();

        //transform.position = new Vector3(0, 0, 0); // StartPosition by owner type - enemy or player?
        if (_unitCharacterManager.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
        {
            transform.position = _playerUnitSpawnPosition.position;
        }
        else
        {
            transform.position = _enemyUnitSpawnPosition.position;
        }

        _unitCharacterManager.characterController.enabled = true;
    }

    private void OnDisable()
    {
        _unitCharacterManager.characterController.enabled = false;
    }
    #endregion

    private UniTask SetMoveDirection()
    {
        if (_unitCharacterManager.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
        {
            _moveDirection = new Vector3(0, 0, 1);
        }
        else if (_unitCharacterManager.characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
        {
            _moveDirection = new Vector3(0, 0, -1);
        }
        else
        {
            Debug.LogWarning("CHARACTER HAS UNDEFINED OWNER TYPE");
        }

        return UniTask.CompletedTask;
    }

    public void MoveForward()
    {
        _unitCharacterManager.characterController.Move(_moveDirection * _unitSpeed * Time.deltaTime);
    }

/*     public void SetRotation()
    {
        //StartCoroutine(SetRotationDelayed());
    } */

    public void SetStartPositions(Transform playerBase, Transform enemyBase)
    {
        _playerUnitSpawnPosition = playerBase;
        _enemyUnitSpawnPosition = enemyBase;
    }

    private UniTask SetRotation()
    {
        //yield return new WaitForSeconds(0.5f);
        // await UniTask.Delay(500);
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
        transform.rotation = targetRotation;
        return UniTask.CompletedTask;
    }
}
