using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private BoxType _boxType;
    #region STATS
    [SerializeField] private float _boxHealth;
    #endregion

    private void OnTriggerEnter(Collider other) {
        Debug.Log("ontrigger enter");
        if(other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            Debug.Log("ontrigger enter inside IDAMAGE");
            bullet.DealDamage(this);

            if(CheckBoxHealth())
            {
                OnBoxHealtRunnedOut();
            }
        }
    }

    private bool CheckBoxHealth()
    {
        if(_boxHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnBoxHealtRunnedOut()
    {
        Destroy(gameObject);
        //PLAY DESTROY VFX
        //INSTANTIATE STAT OBJECT
    }

    public void ReduceHealth(float damage)
    {
        _boxHealth -= damage;
    }
}

public enum BoxType
{
    HP_BOX,
    MANA_BOX,
    AD_BOX,
    AP_BOX,
    DEX_BOX
}
