using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterDeadState : IUnitCharacterState
{
    float timer = 0;
    float returnTimerLimit = 2;
    public void EnterState(UnitCharacterManager unitCharacterManager)
    {
        //Debug.Log("enter dead state");
        unitCharacterManager.GetUnityCharacterAnimationManager().SetAnimatorValue(AnimatorParameterType.BOOL, "isDead", boolValue:true);
        timer = 0;
    }

    public void ExitState(UnitCharacterManager unitCharacterManager)
    {
        
    }

    public void UpdateState(UnitCharacterManager unitCharacterManager)
    {
        //if isDead true, switch to walking state
        //Debug.Log("update dead state");
        timer += Time.deltaTime;

        if(timer >= returnTimerLimit)
        {
            unitCharacterManager.ChangeState(new UnitCharacterWalkingState());
            UnitCharacterPoolManager.Instance.ReturnUnitCharacter(unitCharacterManager);
        }

    }
}
