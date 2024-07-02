using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    private enum EnemyState { IDLE, CHASING, ATTACKING } //Patrolling++
    private EnemyState _currentEnemyState;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _idleToChasingToleranceDistance;
    [SerializeField] private float _chasingToAttackingToleranceDistance;
    float _distanceHolder = 0f;

    public override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        base.Update();
        Debug.Log("current enemy state : " + _currentEnemyState);
        _distanceHolder = Vector3.Distance( _player.transform.position, transform.position);

        switch (_currentEnemyState)
        {
            case EnemyState.IDLE:
                EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);
                if( _distanceHolder < _idleToChasingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.CHASING;
                }
                break;
            case EnemyState.CHASING:
                EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0.5f, 0, false);
                EventSystem.MoveEnemyToTarget(_player.transform);

                if(_distanceHolder < _chasingToAttackingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.ATTACKING;
                }
                else if(_distanceHolder > _idleToChasingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.IDLE;
                }
                break;
            case EnemyState.ATTACKING:
                
                if(_distanceHolder > _chasingToAttackingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.CHASING;
                }
                break;
            default:
                break;
        }
    }
}
