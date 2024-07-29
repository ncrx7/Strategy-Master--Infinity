using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyArenChasingState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
//        Debug.Log("Entering Enemy Chasing State");
        //enemyManager.GetArenaEnemyLocomotionManager().StartAdjustSpeedCoroutine();
        enemyManager.GetArenaEnemyLocomotionManager().Speed = 4f;
        //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(0, 0, 1), Vector3.up);

        Vector3 targetEulerAngles = new Vector3(0, 180, 0);
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        //enemyManager.transform.rotation = targetRotation;
        enemyManager.transform.DORotateQuaternion(targetRotation, 1f);
        //TODO: ENABLE NAVMESH OF ENEMY
    }

    public void ExitState(EnemyManager enemyManager)
    {
        //Debug.Log("Exiting Enemy Chasing State");
        enemyManager.GetArenaEnemyLocomotionManager().Speed = 0f;
        //TODO: DISABLE NAVMESH OF ENEMY
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        enemyManager.GetEnemyAnimatonManagerReference().SetAnimatorValue(AnimatorParameterType.FLOAT, "moveAmount", 0.5f, 0, false);
        enemyManager.GetArenaEnemyLocomotionManager().MoveForward();
       // Debug.Log("enemy chasing arena state");
        

        if (enemyManager.GetDistanceHolder() < enemyManager.GetChasingToAttackingToleranceDistance())
        {
            enemyManager.ChangeState(new EnemyAttackingState());

        }

    }
}
