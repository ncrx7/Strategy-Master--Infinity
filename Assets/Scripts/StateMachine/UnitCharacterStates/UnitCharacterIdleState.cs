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

        StateChangeControl(unitCharacterManager);
    }

    private void StateChangeControl(UnitCharacterManager unitCharacterManager)
    {
        if (unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance == -1 || unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance > 5)
        {
            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
        }

        else if (unitCharacterManager.characterClassType == CharacterClassType.RIFLE || unitCharacterManager.characterClassType == CharacterClassType.MAGE)
        {
            UnitDistanceManager forwardUnitCharacterDistanceManager = unitCharacterManager.GetUnitDistanceManager().
            FriendForwardUnitCharacter.GetComponent<UnitCharacterManager>(). //ForwardUnitCharacter null check
            GetUnitDistanceManager();
            if (forwardUnitCharacterDistanceManager.OpposingUnitDistance != -1 && forwardUnitCharacterDistanceManager.OpposingUnitDistance < 18)
            {
                unitCharacterManager.ChangeState(new UnitCharacterAttackState());
            }
        }

        else if (unitCharacterManager.characterClassType == CharacterClassType.HEALER)
        {
            UnitDistanceManager forwardUnitCharacterDistanceManager = unitCharacterManager.GetUnitDistanceManager().FriendForwardUnitCharacter.GetComponent<UnitCharacterManager>().
            GetUnitDistanceManager();

            if (forwardUnitCharacterDistanceManager.FriendForwardUnitCharacter == null)
            {
                if (forwardUnitCharacterDistanceManager.OpposingUnitDistance != -1 && forwardUnitCharacterDistanceManager.OpposingUnitDistance < 18)
                {
                    unitCharacterManager.ChangeState(new UnitCharacterAttackState());
                }
            }
            else if (forwardUnitCharacterDistanceManager.FriendForwardUnitCharacter != null)
            {
                UnitDistanceManager secondForwardUnitCharacterDistanceManager = forwardUnitCharacterDistanceManager.FriendForwardUnitCharacter.
                GetComponent<UnitCharacterManager>().
                GetUnitDistanceManager();

                if (secondForwardUnitCharacterDistanceManager.OpposingUnitDistance != -1 && secondForwardUnitCharacterDistanceManager.OpposingUnitDistance < 18)
                {
                    unitCharacterManager.ChangeState(new UnitCharacterAttackState());
                }
            }
        }
    }
}
