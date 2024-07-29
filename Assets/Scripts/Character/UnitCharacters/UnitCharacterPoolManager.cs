using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterPoolManager : MonoBehaviour
{
     public static UnitCharacterPoolManager Instance;
    [SerializeField] private GameObject _arenaEnemyPrefab;
    [SerializeField] Dictionary<CharacterClassType, GameObject> _unitCharacterPrefabs = new Dictionary<CharacterClassType, GameObject>();
    [SerializeField] private int _initialPoolSize = 7;
    private ObjectPoolManager<UnitCharacterManager> _unitCharacterPool;

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
        _unitCharacterPool = new ObjectPoolManager<UnitCharacterManager>(_arenaEnemyPrefab.GetComponent<UnitCharacterManager>(), _initialPoolSize, transform); //should be located on awake
    }

    public UnitCharacterManager GetEnemy()
    {
        return _unitCharacterPool.GetObject();
    }

    public void ReturnEnemy(UnitCharacterManager unityCharacterManager)
    {
        _unitCharacterPool.ReturnObject(unityCharacterManager);
    }
}
