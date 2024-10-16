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
        //IF THERE IS NO FRIEND UNIT |OR| FRIEND UNIT AWAY FROM 5MT IN FRONT OF THE UNIT, SWITCH TO WALKING STATE
        if (unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance == -1 || unitCharacterManager.GetUnitDistanceManager().FriendUnitDistance > 5)
        {
            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
        }
        //IF THE UNIT IS RIFLE OR MAGE
        else if (unitCharacterManager.characterClassType == CharacterClassType.RIFLE || unitCharacterManager.characterClassType == CharacterClassType.MAGE)
        {
            UnitDistanceManager forwardUnitCharacterDistanceManager = unitCharacterManager.GetUnitDistanceManager().
            FriendForwardUnitCharacter.GetComponent<UnitCharacterManager>(). //ForwardUnitCharacter null check
            GetUnitDistanceManager();
            //IF THERE IS OPPOSING UNIT AND LESS THAN 18MT IN FRONT OF FRIEND UNIT |OR| THERE IS A OPPOSING BASE IN FRONT OF FRIEND UNIT, SWTICH TO ATTACK STATE
            if ( (forwardUnitCharacterDistanceManager.OpposingUnitDistance != -1 && forwardUnitCharacterDistanceManager.OpposingUnitDistance < 18) 
            || (forwardUnitCharacterDistanceManager.BaseDistance < 18 && forwardUnitCharacterDistanceManager.BaseDistance != -1) )
            {
                unitCharacterManager.ChangeState(new UnitCharacterAttackState());
            }
        }
        //IF THE UNIT IS HEALER 
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
