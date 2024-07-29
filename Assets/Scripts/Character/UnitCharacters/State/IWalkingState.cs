using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWalkingState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("enter state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("exit state unity character");
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("update state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().MoveForward();
    }
}
