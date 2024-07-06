using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    //[SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] NavmeshManager _navmeshManager;
    [SerializeField] private float _enemyMovementSpeed;


    public void HandleMoveEnemyToTarget(Transform targetTransform)
    {
        ActivateEnemySpeed();
        //Debug.Log("target ref : " + targetTransform.position);
        _navmeshManager.GetNavMeshAgent().SetDestination(targetTransform.position);
    }

    public void ActivateEnemySpeed()
    {
        _navmeshManager.GetNavMeshAgent().speed = _enemyMovementSpeed;
    }

    public void DisableEnemySpeed()
    {
        _navmeshManager.GetNavMeshAgent().speed = 0f;
    }

    public NavmeshManager GetNavmeshManagerReference()
    {
        return _navmeshManager;
    }
}
