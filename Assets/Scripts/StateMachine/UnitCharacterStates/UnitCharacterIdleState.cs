using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterIdleState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("enter idle state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("exit idle state unity character");
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("Unit Idle update state unity character");
        if (unitCharacterManager.GetUnityCharacterAnimationManager() != null)
        {
            unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0f);
        }
        //IDLE FUNCTIONS

        if (unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance == -1 || unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance > 5)
        {
            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
        }
    }
}
