using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackColliderManager : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Collider _enemyDamageCollider;
    private int _enemyDamage;

    private void Awake()
    {
        //_damageCollider = GetComponent<Collider>();
        _enemyDamageCollider.gameObject.SetActive(true);
        _enemyDamageCollider.isTrigger = true;
        _enemyDamageCollider.enabled = false;
    }

    private void Start()
    {
        EventSystem.OnEnemyStatsInitialized += () => 
        {
            _enemyDamage = (int)_enemyManager.enemyStats.GetStatValue(StatType.PF);
            //Debug.Log("new enemy damage : " + _enemyDamage);
        };
        //_enemyDamage = 10;
    }

    public void EnableEnemyDamageCollider()
    {
        _enemyDamageCollider.enabled = true;
    }
    public void DisableEnemyDamageCollider()
    {
        _enemyDamageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<PlayerStatManager>(out PlayerStatManager playerStatManager))
        {
            Debug.Log("enemy damage  : " + _enemyDamage);
            Debug.Log("reduce rate: " + playerStatManager.DamageReduceRate / 100);
            int damage = (int)(_enemyDamage - (_enemyDamage * (playerStatManager.DamageReduceRate / 100))); //can develop algorithm
            Debug.Log("damage : " + damage);
            playerStatManager.SetCurrentPlayerHealth(playerStatManager.GetCurrentPlayerHealth() - damage);
        }
    }
}
