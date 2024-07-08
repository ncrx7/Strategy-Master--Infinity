using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDefeatState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
        //Debug.Log("Entering Enemy Idle State");
        enemyManager.GetEnemyLocomotionManagerReference().GetNavmeshManagerReference().DisableNavmeshAgent();
        //enemyManager.isVictory = true;
        enemyManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isDefeated", boolValue: true);
    }

    public void ExitState(EnemyManager enemyManager)
    {
        //Debug.Log("Exiting Enemy Idle State");
    }

    public void UpdateState(EnemyManager enemyManager)
    {

    }
}
