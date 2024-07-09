using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquilibriumBullet : Bullet, IDamage
{
    private float _lifeTimeCounter;

    public void DealDamage(ref float healthVariable, int playerPF)
    {
        BulletPoolManager.Instance.ReturnBullet(this);
        healthVariable -= _damage + playerPF; //TODO: ADD PLAYER PF STAT TO BULLET DAMAGE HERE
        //boxManager.ReduceHealth(_damage);
    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    private void OnEnable()
    {
        _lifeTimeCounter = 0f;
    }


    void Update()
    {
        MoveBullet();
        CheckLifeTime();
    }

    void MoveBullet()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    void CheckLifeTime()
    {
        _lifeTimeCounter += Time.deltaTime;
        if (_lifeTimeCounter >= _lifeTime)
        {
            BulletPoolManager.Instance.ReturnBullet(this);
        }
    }
}
