using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : CharacterLocomotionManager
{
    [SerializeField] NavMeshAgent _navMeshAgent;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnEnable()
    {
        EventSystem.MoveEnemyToTarget += HandleMoveEnemyToTarget;
    }

    private void OnDisable()
    {
        EventSystem.MoveEnemyToTarget -= HandleMoveEnemyToTarget;
    }

    public void HandleMoveEnemyToTarget(Transform targetTransform)
    {
        ActivateEnemySpeed();
        Debug.Log("target ref : " + targetTransform.position);
        _navMeshAgent.SetDestination(targetTransform.position);
    }

    public void ActivateEnemySpeed()
    {
        _navMeshAgent.speed = GetMovementSpeed();
    }

    public void DisableEnemySpeed()
    {
        _navMeshAgent.speed = 0f;
    }
}
