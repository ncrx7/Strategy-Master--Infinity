using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquilibriumBullet : Bullet, IDamage
{

    public void DealDamage(ref float healthVariable, int playerPF)
    {
        BulletPoolManager.Instance.ReturnBullet(this);
        healthVariable -= _damage + playerPF; //TODO: ADD PLAYER PF STAT TO BULLET DAMAGE HERE
        //boxManager.ReduceHealth(_damage);
    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    public override void Update()
    {
        base.Update();
    }
}
