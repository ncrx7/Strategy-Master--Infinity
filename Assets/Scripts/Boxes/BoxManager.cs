using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private BoxType _boxType;
    #region STATS
    [SerializeField] private float _boxHealth;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] private Transform _playerTransform;
    #endregion

    private void Start()
    {
        _healthText.text = _boxHealth.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("ontrigger enter");
        if(other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            Debug.Log("ontrigger enter inside IDAMAGE");
            bullet.DealDamage(this);
            _healthText.text = _boxHealth.ToString();

            if(CheckBoxHealth())
            {
                OnBoxHealtRunnedOut();
            }
        }
    }

    private void FixedUpdate()
    {
        _healthText.transform.LookAt(_playerTransform);
        _healthText.transform.Rotate(0, 180, 0);
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
