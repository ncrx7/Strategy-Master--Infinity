using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacerManager : MonoBehaviour
{
    [SerializeField] Transform[] _startTransforms;
    [SerializeField] int _spawnInterval;
    Coroutine _spawnCoroutine;
    EnemyManager _enemyManager;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemyDelayed());
    }

    IEnumerator SpawnEnemyDelayed()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            _enemyManager = ArenaEnemyPoolManager.Instance.GetEnemy();
            _enemyManager.transform.position = _startTransforms[Random.Range(0,3)].transform.position;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
