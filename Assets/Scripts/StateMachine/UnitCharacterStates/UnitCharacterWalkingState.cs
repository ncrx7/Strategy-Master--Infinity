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
        Debug.Log("Unit Walking update state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().MoveForward();
        
        if(unitCharacterManager.GetUnityCharacterAnimationManager() != null)
        {
            unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0.5f);
            Debug.Log("0.5 worked");
        }

        //STATE CHANGES
        if(unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance < 5 && unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance != -1)
        {
            unitCharacterManager.ChangeState(new UnitCharacterIdleState());
        }
        else if(unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance < 8 && unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance != -1)
        {
            unitCharacterManager.ChangeState(new UnitCharacterAttackState());
        }
    }
}
