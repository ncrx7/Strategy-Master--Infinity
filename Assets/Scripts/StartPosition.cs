using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField] Transform enemyBaseTransform;
    [SerializeField] Transform playerBaseTransform;
    private void OnEnable()
    {
        UnitCharacterLocomotionManager.OnUnitCharacterSpawned += SetStartPos;
    }

    private void OnDisable()
    {
        UnitCharacterLocomotionManager.OnUnitCharacterSpawned -= SetStartPos;
    }

    private void SetStartPos(UnitCharacterLocomotionManager unitCharacterLocomotionManager)
    {
        unitCharacterLocomotionManager.SetStartPositions(playerBaseTransform, enemyBaseTransform);
    }
}
