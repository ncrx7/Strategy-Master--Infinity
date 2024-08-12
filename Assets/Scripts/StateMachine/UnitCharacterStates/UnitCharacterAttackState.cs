using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterAttackState : IUnitCharacterState
{
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("enter attack state unity character");
        unitCharacterManager.GetUnitCharacterLocomotionManager().SetRotation();
        unitCharacterManager.GetUnitCharacterSkillManager().HandleStartAttacking(0);
        unitCharacterManager.GetUnitCharacterHealthBarController().SwitchHealthBarVisibility();
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("exit attack state unity character");
        unitCharacterManager.GetUnitCharacterSkillManager().HandleStopAttacking();
        unitCharacterManager.GetUnitCharacterHealthBarController().SwitchHealthBarVisibility();
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("Unit Attacking update state unity character");
        unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0f);

        //ATTACK FUNCTIONS

        if (unitCharacterManager.characterClassType == CharacterClassType.RIFLE ||
        unitCharacterManager.characterClassType == CharacterClassType.MAGE || unitCharacterManager.characterClassType == CharacterClassType.HEALER) //Range classes
        {
            /*             if (unitCharacterManager.GetUnitDistanceManager().ForwardUnitCharacter == null)
                        {
                            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
                        } */

            if (unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance > 5 || unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance > 18)
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
            else if (unitCharacterManager.GetUnitDistanceManager().FriendForwardUnitCharacter == null && unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance == -1)
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
        }
        else if (unitCharacterManager.characterClassType == CharacterClassType.MEELE_FIGHTER) // no range classes
        {
            Debug.Log("not rifle and magic ");
            if (unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance > 18 || unitCharacterManager.GetUnitDistanceManager().OpposingUnitDistance == -1)
            {
                unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            }
        }

    }
}
