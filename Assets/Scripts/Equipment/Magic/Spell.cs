using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [Header("fields")]
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
            SpellVfxPoolManager.Instance.ReturnSpell(this);
        }
    }
}
