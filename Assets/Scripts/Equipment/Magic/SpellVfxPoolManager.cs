using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellVfxPoolManager : MonoBehaviour
{
    public static SpellVfxPoolManager Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject fireStormVfxPrefab;
    [SerializeField] private GameObject healAreaVfxPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private ObjectPoolManager<Spell> _fireStormVfxPool;
    private ObjectPoolManager<Spell> _healAreaVfxPool;

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
        _fireStormVfxPool = new ObjectPoolManager<Spell>(fireStormVfxPrefab.GetComponent<Spell>(), initialPoolSize, transform);
        _healAreaVfxPool = new ObjectPoolManager<Spell>(healAreaVfxPrefab.GetComponent<Spell>(), initialPoolSize, transform);
    }

    public Spell GetSpell(SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.SPELL_MAGE_FIRESTORM:
                return _fireStormVfxPool.GetObject();
            case SpellType.SPELL_HEALER_HEALAREA:
                return _healAreaVfxPool.GetObject();
            default:
                Debug.LogWarning("Undefined spell type!!!");
                return null;
        }
    }

    public void ReturnSpell(Spell spell, SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.SPELL_MAGE_FIRESTORM:
                _fireStormVfxPool.ReturnObject(spell);
                break;
            case SpellType.SPELL_HEALER_HEALAREA:
                _healAreaVfxPool.ReturnObject(spell);
                break;
            default:
                Debug.LogWarning("Undefined spell type!!!");
                break;
        }
    }
}

public enum SpellType
{
    SPELL_MAGE_FIRESTORM,
    SPELL_HEALER_HEALAREA
}