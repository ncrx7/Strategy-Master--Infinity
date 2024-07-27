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
        //enemyManager.GetEnemyLocomotionManagerReference().GetNavmeshManagerReference().DisableNavmeshAgent();
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        //EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);
        enemyManager.GetEnemyAnimatonManagerReference().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);

        if (enemyManager.GetDistanceHolder() > enemyManager.GetChasingToAttackingToleranceDistance())
        {
            if (enemyManager._currentEnemyLocationType == EnemyLocationType.STRATEGY)
            {
                enemyManager.ChangeState(new EnemyChasingState());
            }
            else if (enemyManager._currentEnemyLocationType == EnemyLocationType.ARENA)
            {
                enemyManager.ChangeState(new EnemyArenChasingState());
            }
        }
    }
}
