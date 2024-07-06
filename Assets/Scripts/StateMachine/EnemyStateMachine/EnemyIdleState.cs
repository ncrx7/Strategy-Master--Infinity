using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
        //Debug.Log("Entering Enemy Idle State");
    }

    public void ExitState(EnemyManager enemyManager)
    {
        //Debug.Log("Exiting Enemy Idle State");
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        enemyManager.GetEnemyAnimatonManagerReference().SetAnimatorValue(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);

        if (enemyManager.GetDistanceHolder() < enemyManager.GetIdleToChasingToleranceDistance())
        {
            //_currentEnemyState = EnemyState.CHASING;
            enemyManager.ChangeState(new EnemyChasingState());
        }
    }
}
