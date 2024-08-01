using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterWalkingState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("enter walking state unity character");
        unitCharacterManager.characterController.enabled = true;
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("exit walking state unity character");
        unitCharacterManager.characterController.enabled = false;
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("update state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().MoveForward();
    }
}
