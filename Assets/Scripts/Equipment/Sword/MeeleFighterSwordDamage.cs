using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleFighterSwordDamage : MonoBehaviour, IDamage
{
    [SerializeField] private float _swordBaseDamage;
    [SerializeField] private Collider _swordCollider;
    public void DealDamage(ref float healthVariable, int playerPF)
    {
        Debug.Log("dealed damage from the sword");
        healthVariable -= _swordBaseDamage + playerPF;
    }

    public void PlayParticleVfx(GameObject box)
    {
        throw new System.NotImplementedException();
    }

    public void EnableCollider()
    {
        _swordCollider.enabled = true;
    }
    public void DisableCollider()
    {
        _swordCollider.enabled = false;
    }
}
