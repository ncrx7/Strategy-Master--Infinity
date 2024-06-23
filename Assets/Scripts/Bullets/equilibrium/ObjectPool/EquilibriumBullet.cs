using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquilibriumBullet : Bullet, IDamage
{
    private float _lifeTimeCounter;

    public void DealDamage(BoxManager boxManager)
    {
        BulletPoolManager.Instance.ReturnBullet(this);
        boxManager.ReduceHealth(_damage);
    }

    public void PlayParticleVfx(GameObject box)
    {
        
    }

    private void OnEnable()
    {
        _lifeTimeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _lifeTimeCounter += Time.deltaTime;

        if (_lifeTimeCounter >= _lifeTime)
        {
            BulletPoolManager.Instance.ReturnBullet(this);
        }
    }
}
