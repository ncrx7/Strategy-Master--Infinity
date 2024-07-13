using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] protected float _currentBaseHealth;
    [SerializeField] protected TextMeshPro _baseHealthText;

    public virtual void Start()
    {
        InitializeBaseHealth();
        UpdateBaseHealthText(_currentBaseHealth);
    }

    private void InitializeBaseHealth()
    {
        _currentBaseHealth = 1000; //TODO: INIT WITH LEVEL
    }

    private int CalculateHealth()
    {
        return -1;
    }

    protected void UpdateBaseHealthText(float newHealth)
    {
        _baseHealthText.text = newHealth.ToString();
    }

    protected bool CheckBaseHealth()
    {
        if (_currentBaseHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
