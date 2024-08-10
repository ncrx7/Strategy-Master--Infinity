using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellVfxPoolManager : MonoBehaviour
{
    public static SpellVfxPoolManager Instance;

    [SerializeField] private GameObject spellVfxPrefab;
    [SerializeField] private int initialPoolSize = 20;

    private ObjectPoolManager<Spell> spellVfxPool;

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
        spellVfxPool = new ObjectPoolManager<Spell>(spellVfxPrefab.GetComponent<Spell>(), initialPoolSize, transform);
    }

    public Spell GetSpell()
    {
        return spellVfxPool.GetObject();
    }

    public void ReturnSpell(Spell spell)
    {
        spellVfxPool.ReturnObject(spell);
    }
}
