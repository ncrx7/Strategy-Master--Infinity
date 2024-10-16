using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterAttackState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("enter attack state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
        //unitCharacterManager.GetUnitCharacterSkillManager().HandleStartAttacking(0);
        unitCharacterManager.GetUnitCharacterSkillManager().IsCoroutineRunning = false;
        unitCharacterManager.GetUnitCharacterHealthBarController().SwitchHealthBarVisibility();
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("exit attack state unity character");
        unitCharacterManager.GetUnitCharacterSkillManager().HandleStopAttacking();
        unitCharacterManager.GetUnitCharacterHealthBarController().SwitchHealthBarVisibility();
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("Unit Attacking update state unity character");
        unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0f);

        HandleAttack(unitCharacterManager);

        StateChangeControl(unitCharacterManager);
    }

    private void HandleAttack(UnitCharacterManager unitCharacterManager)
    {

        if (!unitCharacterManager.GetUnitCharacterSkillManager().IsCoroutineRunning)
        {
            if (unitCharacterManager.characterClassType == CharacterClassType.HEALER && unitCharacterManager.GetUnitDistanceManager().FriendForwardUnitCharacter == null)
            {
                unitCharacterManager.GetUnitCharacterSkillManager().HandleStartAttacking(1);
            }
            else
            {
                unitCharacterManager.GetUnitCharacterSkillManager().HandleStartAttacking(0);
            }
        }
    }

    private void StateChangeControl(UnitCharacterManager unitCharacterManager)
    {
        if (unitCharacterManager.characterClassType == CharacterClassType.RIFLE ||
        unitCharacterManager.characterClassType == CharacterClassType.MAGE || unitCharacterManager.characterClassType == CharacterClassType.HEALER) //Range classes
        {
            if (unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance > 5 || unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance > 18)
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
            else if (unitCharacterManager.GetUnitDistanceManager().FriendForwardUnitCharacter == null && unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance == -1
            && unitCharacterManager.GetUnitDistanceManager().BaseDistance > 18)
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
        }
        else if (unitCharacterManager.characterClassType == CharacterClassType.MEELE_FIGHTER) // no range classes
        {
            if ( (unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance > 18 || unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance == -1)
            && (unitCharacterManager.GetUnitDistanceManager().BaseDistance > 18 || unitCharacterManager.GetUnitDistanceManager().BaseDistance == -1) ) 
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
        }
    }
}
