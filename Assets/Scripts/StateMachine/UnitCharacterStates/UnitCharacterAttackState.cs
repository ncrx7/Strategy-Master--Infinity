using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterAttackState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("enter attack state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
        unitCharacterManager.GetUnitCharacterSkillManager().HandleStartAttacking(0);
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("exit attack state unity character");
        unitCharacterManager.GetUnitCharacterSkillManager().HandleStopAttacking();
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("Unit Attacking update state unity character");
        unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0f);

        //ATTACK FUNCTIONS
        if(unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance > 8 || unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance == -1)
        {
            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
        }
    }
}
