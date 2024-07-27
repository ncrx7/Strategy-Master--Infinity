using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : CharacterManager
{
    #region REFERENCE FIELDS
    [Header("REFERENCE FIELDS")]
    [SerializeField] public EnemyLocationType _currentEnemyLocationType;
    [SerializeField] private EnemyLocomotionManager _enemyLocomotionManager;
    [SerializeField] private ArenaEnemyLocomotionManager _arenaEnemyLocomotionManager;
    [SerializeField] public GameObject _player;
    [SerializeField] private PlayerStatManager _playerStatManager;
    [SerializeField] private GameObject _moneyObject;
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
    public bool isDefeated { get; set; }
    public bool isVictory { get; set; }
    #endregion

    private void Awake()
    {
        EventSystem.OnEnemyEnabledOnScene?.Invoke(this, EventSystem.OnEnemyEnabledOnSceneCallback);
    }
    public override void Start()
    {
        //base.Start();
        ChooseInitialState();

        _enemyTimePerAttack = 1 / _enemyAttackSpeed;

        CreateEnemyStat();

        EventSystem.OnEnemyStatsInitialized?.Invoke();

        if (_player == null)
            EventSystem.OnEnemyEnabledOnScene?.Invoke(this, EventSystem.OnEnemyEnabledOnSceneCallback);

    }

    private void OnEnable()
    {
        EventSystem.OnPlayerDefeat += SwitchToVictoryStateOnPlayerDead;
        EventSystem.OnTimeOutForEvolutionPhase += SwitchToDefeatStateOnTimeOut;
        EventSystem.OnPlayerEnabledOnScene += InitializePlayerManagerOnPlayerEnabled;
/*         EventSystem.OnEnemyEnabledOnSceneCallback += (EnemyManager enemyManager) =>
        {
            Debug.Log("stat manager from callback but not if :" + enemyManager.name + "-" + this.name);
            Destroy(enemyManager.gameObject);
            if (this == enemyManager)
            {
                Debug.Log("Equal");
                //return;
            } 

            _playerStatManager = _player.GetComponent<PlayerStatManager>();
            Debug.Log("stat manager from callback : " + _playerStatManager);
        }; */
    }

    private void OnDisable()
    {
        EventSystem.OnPlayerDefeat -= SwitchToVictoryStateOnPlayerDead;
        EventSystem.OnPlayerEnabledOnScene -= InitializePlayerManagerOnPlayerEnabled;
        EventSystem.OnTimeOutForEvolutionPhase -= SwitchToDefeatStateOnTimeOut;
/*         EventSystem.OnEnemyEnabledOnSceneCallback -= (EnemyManager enemyManager) =>
        {
            if (this != enemyManager) return;

            _playerStatManager = _player.GetComponent<PlayerStatManager>();
            Debug.Log("stat manager from callback : " + _playerStatManager);
        }; */
    }

    public void InitializePlayerManagerOnPlayerEnabled(PlayerManager playerManager)
    {
        _player = playerManager.gameObject;
    }

    public void InitPlayerStat(PlayerStatManager playerStatManager)
    {
        _playerStatManager = playerStatManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref enemyStats.hp, (int)_playerStatManager.GetPlayerFixedStatValue(StatType.PF));
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
        if (enemyStats.hp <= 0)
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
        if (_currentEnemyLocationType == EnemyLocationType.STRATEGY)
        {
            _moneyObject.transform.parent = null;
            _moneyObject.SetActive(true);
        }
        else if (_currentEnemyLocationType == EnemyLocationType.ARENA)
        {
            //DO SOMETHING
        }

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
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            Quaternion targetQuaternion = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = targetQuaternion;
            transform.DORotateQuaternion(targetQuaternion, 0.4f);
            Debug.Log("target qt : " + targetQuaternion.eulerAngles);
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

    private void ChooseInitialState()
    {
        switch (_currentEnemyLocationType)
        {
            case EnemyLocationType.STRATEGY:
                ChangeState(new EnemyIdleState());
                break;
            case EnemyLocationType.ARENA:
                ChangeState(new EnemyArenChasingState());
                break;
            default:
                break;
        }
    }

    public void SwitchToVictoryStateOnPlayerDead()
    {
        ChangeState(new EnemyVictoryState());
    }

    public void SwitchToDefeatStateOnTimeOut()
    {
        ChangeState(new EnemyDefeatState());
    }

    public CharacterAnimationManager GetEnemyAnimatonManagerReference()
    {
        return characterAnimationManager;
    }

    public EnemyLocomotionManager GetEnemyLocomotionManagerReference()
    {
        return _enemyLocomotionManager;
    }

    public ArenaEnemyLocomotionManager GetArenaEnemyLocomotionManager()
    {
        return _arenaEnemyLocomotionManager;
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

public enum EnemyLocationType
{
    STRATEGY,
    ARENA
}
