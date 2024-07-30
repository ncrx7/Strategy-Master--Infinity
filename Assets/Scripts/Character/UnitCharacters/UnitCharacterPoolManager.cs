using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private async void Start()
    {
        await CreatePool();
        await SetUnitPrefabInit();
        //_unitCharacterPool = new ObjectPoolManager<UnitCharacterManager>(_arenaEnemyPrefab.GetComponent<UnitCharacterManager>(), _initialPoolSize, transform); //should be located on awake
    }

    public UnitCharacterManager GetEnemy()
    {
        return _unitCharacterPool.GetObject();
    }

    public void ReturnEnemy(UnitCharacterManager unityCharacterManager)
    {
        _unitCharacterPool.ReturnObject(unityCharacterManager);
    }

    private async Task CreatePool()
    {
        _unitCharacterPool = new ObjectPoolManager<UnitCharacterManager>(_arenaEnemyPrefab.GetComponent<UnitCharacterManager>(), _initialPoolSize, transform); //should be located on awake
        await Task.Delay(500);
    }

    private async Task SetUnitPrefabInit()
    {
        await Task.Delay(500);
        for (int i = 0; i < _initialPoolSize; i++)
        {
            UnitCharacterManager unit = _unitCharacterPool.GetObject();
            GameObject unitObject = unit.gameObject;
            UnitClass unitClass = unit.GetAllClassData()[i % unit.GetAllClassData().Length];
            unit.SetCurrentClassData(unitClass);
            unit.gameObject.SetActive(false);
        }
    }
}
