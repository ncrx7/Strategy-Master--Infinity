using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemyManager : CharacterManager
{
    public EnemyStats enemyStats;
    public PlayerStatManager playerStatManager;

    public override void Start()
    {
        CreateEnemyStat();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref enemyStats.hp, (int)playerStatManager.GetPlayerFixedStatValue(StatType.PF));
            //Debug.Log("new enemy hp : " + _enemyStats._hp);
            //_healthText.text = _boxHealth.ToString();

            if (CheckEnemyHealth())
            {
                Debug.Log("enemy died");
            }
        }
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
}
