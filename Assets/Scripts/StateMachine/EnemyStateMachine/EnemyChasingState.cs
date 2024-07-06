using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
        //Debug.Log("Entering Enemy Chasing State");
        enemyManager.GetEnemyLocomotionManagerReference().GetNavmeshManagerReference().ActivateNavmeshAgent();
        //TODO: ENABLE NAVMESH OF ENEMY
    }

    public void ExitState(EnemyManager enemyManager)
    {
        //Debug.Log("Exiting Enemy Chasing State");
        enemyManager.GetEnemyLocomotionManagerReference().GetNavmeshManagerReference().DisableNavmeshAgent();
        //TODO: DISABLE NAVMESH OF ENEMY
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        //EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0.5f, 0, false);
        enemyManager.GetEnemyAnimatonManagerReference().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0.5f, 0, false);
        enemyManager.GetEnemyLocomotionManagerReference().HandleMoveEnemyToTarget(enemyManager.GetPlayerTransform());
        //EventSystem.MoveEnemyToTarget?.Invoke(_player.transform);

        if (enemyManager.GetDistanceHolder() < enemyManager.GetChasingToAttackingToleranceDistance())
        {
            enemyManager.ChangeState(new EnemyAttackingState());
            enemyManager.GetEnemyLocomotionManagerReference().DisableEnemySpeed();
            //EventSystem.StopTheEnemy?.Invoke();

        }
        else if (enemyManager.GetDistanceHolder() > enemyManager.GetIdleToChasingToleranceDistance())
        {
            enemyManager.ChangeState(new EnemyIdleState());
            enemyManager.GetEnemyLocomotionManagerReference().DisableEnemySpeed();
            //EventSystem.StopTheEnemy?.Invoke();
        }
    }
}
