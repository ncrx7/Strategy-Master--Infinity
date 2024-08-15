using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBaseManager : MonoBehaviour
{
    [SerializeField] private ArenaUIManager _arenaUIManager;
    public BaseType baseType;

    public float CurrentBaseHealth {get; private set;}
    private float _maximumHealth;
    
    private void Start()
    {
        SetMaximumHealth();
    }

    private void SetMaximumHealth()
    {
        _maximumHealth = 1500 * PlayerStatusManager.Instance.GetPlayerStatObjectReference().level;

        CurrentBaseHealth = _maximumHealth;
        EventSystem.SetSliderBarValue?.Invoke(BarType.HEALTH_BAR, CurrentBaseHealth, _maximumHealth, baseType);
    }
    
}

public enum BaseType
{
    PLAYER_BASE,
    OPPOSING_BASE
}
