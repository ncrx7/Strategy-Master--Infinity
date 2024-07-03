using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] private float _enemyMovementSpeed;

/*     public  void Start()
    {
        
    } */

    // Update is called once per frame
/*     public  void Update()
    {
        
    } */

/*     private void OnEnable()
    {
        EventSystem.MoveEnemyToTarget += HandleMoveEnemyToTarget;
        EventSystem.StopTheEnemy += DisableEnemySpeed;
    }

    private void OnDisable()
    {
        EventSystem.MoveEnemyToTarget -= HandleMoveEnemyToTarget;
        EventSystem.StopTheEnemy -= DisableEnemySpeed;
    } */

    public void HandleMoveEnemyToTarget(Transform targetTransform)
    {
        ActivateEnemySpeed();
        //Debug.Log("target ref : " + targetTransform.position);
        _navMeshAgent.SetDestination(targetTransform.position);
    }

    public void ActivateEnemySpeed()
    {
        _navMeshAgent.speed = _enemyMovementSpeed;
    }

    public void DisableEnemySpeed()
    {
        _navMeshAgent.speed = 0f;
    }
}
