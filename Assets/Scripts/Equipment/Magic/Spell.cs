using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private SpellType _spellType;
    [SerializeField] protected UnitCharacterManager _unitCharacterManager;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected float _baseDamage;
    [SerializeField] protected float _lifeTime;
    protected float _lifeTimeCounter;

    public virtual void Update()
    {
        HandleLifeTime();
    }

    void HandleLifeTime()
    {
        _lifeTimeCounter += Time.deltaTime;
        if (_lifeTimeCounter >= _lifeTime)
        {
            SpellVfxPoolManager.Instance.ReturnSpell(this, _spellType);
        }
    }

    public void SetUnitCharacterManager(UnitCharacterManager unitCharacterManager)
    {
        _unitCharacterManager = unitCharacterManager;
    }
}
