using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemyPoolManager : MonoBehaviour
{
    public static ArenaEnemyPoolManager Instance;
    [SerializeField] private GameObject _arenaEnemyPrefab;
    [SerializeField] private int _initialPoolSize = 20;
    private ObjectPoolManager<EnemyManager> _arenaEnemyPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _arenaEnemyPool = new ObjectPoolManager<EnemyManager>(_arenaEnemyPrefab.GetComponent<EnemyManager>(), _initialPoolSize, transform);
    }

    public EnemyManager GetBullet()
    {
        return _arenaEnemyPool.GetObject();
    }

    public void ReturnBullet(EnemyManager enemyManager)
    {
        _arenaEnemyPool.ReturnObject(enemyManager);
    }
}
