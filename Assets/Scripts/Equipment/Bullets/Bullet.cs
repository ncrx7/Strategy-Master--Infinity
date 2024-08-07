using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _damage;
    private float _lifeTimeCounter;
    private void OnEnable()
    {
        _lifeTimeCounter = 0f;
        Debug.Log("bullet on enable");
    }

    public virtual void Update()
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
            BulletPoolManager.Instance.ReturnBullet(this as EquilibriumBullet);
        }
    }
}
