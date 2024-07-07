using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    #region REFERENCE FIELDS
    [Header("REFERENCE FIELDS")]
    [SerializeField] private EnemyLocomotionManager _enemyLocomotionManager;
    [SerializeField] private GameObject _player;
    private IEnemyState currentState;
    public EnemyStats enemyStats;
    #endregion

    #region DISTANCE FIELD
    [Header("DISTANCE FIELD")]
    [SerializeField] private float _idleToChasingToleranceDistance;
    [SerializeField] private float _chasingToAttackingToleranceDistance;
    float _distanceHolder = 0f;
    #endregion

    #region ATTACK FIELDS 
    [Header("ATTACK FIELDS")]
    [SerializeField] private EnemyAttackColliderManager _enemyAttackCollider;
    [SerializeField] private float _enemyAttackSpeed;
    private Coroutine _attackCoroutine;
    private float _enemyTimePerAttack;
    #endregion

    public override void Start()
    {
        //base.Start();
        ChangeState(new EnemyIdleState());
        _enemyTimePerAttack = 1 / _enemyAttackSpeed;
        CreateEnemyStat();
        EventSystem.OnEnemyStatsInitialized?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref enemyStats._hp);
            //Debug.Log("new enemy hp : " + _enemyStats._hp);
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

        SetDistanceBetweenEnemyAndPlayer();
        currentState.UpdateState(this);
    }

    private void SetDistanceBetweenEnemyAndPlayer()
    {
        //_distanceHolder = Vector3.Distance( _player.transform.position, transform.position);
        _distanceHolder = (transform.position - _player.transform.position).sqrMagnitude;
    }

    private void CreateEnemyStat()
    {
        enemyStats = new EnemyStats(200, 10); //TODO: MOVE ENEMY STAT MANAGER
    }

    private bool CheckEnemyHealth()
    {
        if (enemyStats._hp <= 0)
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

    public void HandleEnemyAttackStart()
    {

        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(HandleEnemyAttackCoroutine());
        }
    }

    public void HandleEnemyAttackStop()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

    }

    private IEnumerator HandleEnemyAttackCoroutine()
    {
        while (true)
        {
            characterAnimationManager.HandlePlayAnimation("Zombie_Attack");
            yield return new WaitForSeconds(_enemyTimePerAttack);
        }
    }

    public void OpenDamageCollider()
    {
        _enemyAttackCollider.EnableEnemyDamageCollider();
    }

    public void CloseDamageCollider()
    {
        _enemyAttackCollider.DisableEnemyDamageCollider();
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public CharacterAnimationManager GetEnemyAnimatonManagerReference()
    {
        return characterAnimationManager;
    }

    public EnemyLocomotionManager GetEnemyLocomotionManagerReference()
    {
        return _enemyLocomotionManager;
    }

    public Transform GetPlayerTransform()
    {
        return _player.transform;
    }

    public float GetDistanceHolder()
    {
        return _distanceHolder;
    }

    public float GetIdleToChasingToleranceDistance()
    {
        return _idleToChasingToleranceDistance;
    }

    public float GetChasingToAttackingToleranceDistance()
    {
        return _chasingToAttackingToleranceDistance;
    }
}
