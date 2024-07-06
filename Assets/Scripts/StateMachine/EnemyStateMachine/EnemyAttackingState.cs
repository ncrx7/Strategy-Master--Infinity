using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
        //Debug.Log("Entering Enemy Attacking State");
        enemyManager.HandleEnemyAttackStart();
    }

    public void ExitState(EnemyManager enemyManager)
    {
        //Debug.Log("Exiting Enemy Attacking State");
        enemyManager.HandleEnemyAttackStop();
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        //EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);
        enemyManager.GetEnemyAnimatonManagerReference().SetAnimatorValue(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);

        if (enemyManager.GetDistanceHolder() > enemyManager.GetChasingToAttackingToleranceDistance())
        {
            enemyManager.ChangeState(new EnemyChasingState());
        }
    }
}
