using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    private enum EnemyState { IDLE, CHASING, ATTACKING } //Patrolling++
    private EnemyState _currentEnemyState;
    private EnemyStats _enemyStats;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _idleToChasingToleranceDistance;
    [SerializeField] private float _chasingToAttackingToleranceDistance;
    [SerializeField] private float _enemyAttackSpeed;
    private float _enemyTimePerAttack;
    private float _attackTimer = 5f;
    float _distanceHolder = 0f;

    public override void Start()
    {
        //base.Start();
        _enemyTimePerAttack = 1 / _enemyAttackSpeed;
        Debug.Log("time per attack : " + _enemyTimePerAttack);
        CreateEnemyStat();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref _enemyStats._hp);
            Debug.Log("new enemy hp : " + _enemyStats._hp);
            //_healthText.text = _boxHealth.ToString();

            if (CheckEnemyHealth())
            {
                OnEnemyHealtRunnedOut();
            }
        }
    }

    public override void Update()
    {
        //base.Update();
        Debug.Log("current enemy state : " + _currentEnemyState);
        SetDistanceBetweenEnemyAndPlayer();
        HandleStateMachine();
    }

    private void SetDistanceBetweenEnemyAndPlayer()
    {
        //_distanceHolder = Vector3.Distance( _player.transform.position, transform.position);
        _distanceHolder = (transform.position - _player.transform.position).sqrMagnitude;
    }

    private void HandleStateMachine()
    {
        switch (_currentEnemyState)
        {
            case EnemyState.IDLE:
                EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);
                if (_distanceHolder < _idleToChasingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.CHASING;
                }
                break;
            case EnemyState.CHASING:
                EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0.5f, 0, false);
                EventSystem.MoveEnemyToTarget(_player.transform);

                if (_distanceHolder < _chasingToAttackingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.ATTACKING;
                    EventSystem.StopTheEnemy?.Invoke();
                    
                }
                else if (_distanceHolder > _idleToChasingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.IDLE;
                    EventSystem.StopTheEnemy?.Invoke();
                }
                break;
            case EnemyState.ATTACKING:
                EventSystem.UpdateAnimatorParameter(CharacterAnimatorType.ENEMY_ANIMATOR, AnimatorParameterType.FLOAT, "moveAmount", 0f, 0, false);
                HandleEnemyAttack();
                if (_distanceHolder > _chasingToAttackingToleranceDistance)
                {
                    _currentEnemyState = EnemyState.CHASING;
                }
                break;
            default:
                break;
        }
    }

    private void CreateEnemyStat()
    {
        _enemyStats = new EnemyStats(200, 10);
    }

    private bool CheckEnemyHealth()
    {
        if (_enemyStats._hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnEnemyHealtRunnedOut()
    {
        Destroy(gameObject);
    }

    private void HandleEnemyAttack()
    {
        _attackTimer += Time.deltaTime * 1;
        Debug.Log("attack timer : " + _attackTimer);
        if (_attackTimer > _enemyTimePerAttack)
        {
            EventSystem.PlayAnimation?.Invoke(CharacterAnimatorType.ENEMY_ANIMATOR, "Zombie_Attack");
            _attackTimer = 0;
            Debug.Log("attack!!!!!!");
        }
    }
}
