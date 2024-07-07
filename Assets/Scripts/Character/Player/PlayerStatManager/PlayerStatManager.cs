using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    PlayerStats _playerStats;
    [SerializeField] private PlayerManager _playerManager;

    [Header("Current Stats")]
    [SerializeField] private int _currentPlayerHealth;

    private void Awake()
    {
        //IF NEW GAME CREATED
        CreateNewPlayerStat();
        _currentPlayerHealth = (int)GetPlayerFixedStatValue(StatType.HP);
        //IF EXISTING GAME
        //PopulatePlayerStat();

    }

    private void Start()
    {
        EventSystem.SetMaxHealthUI?.Invoke();
        EventSystem.UpdateHealthBarUI?.Invoke((int)GetPlayerFixedStatValue(StatType.HP), _currentPlayerHealth);
    }

    private void CreateNewPlayerStat()
    {
        //DEFAULT PLAYER STAT VALUE
        _playerStats = new PlayerStats(30, 10, 10, 15, 5);
    }

    private void PopulatePlayerStat()
    {
        //DATA COME FROM DATABSE AND POPULATE STAT CLASS
    }

    public void UpdateFixedPlayerStat(StatType statType, float value)
    {
        _playerStats.SetStatValue(statType, value);
    }

    public float GetPlayerFixedStatValue(StatType statType)
    {
        return _playerStats.GetStatValue(statType);
    }

    public void SetCurrentPlayerHealth(int newHealth)
    {
        _currentPlayerHealth = newHealth;
        
        if(CheckPlayerHealth())
        {
            //TODO: ACTIVATE DEAD UI
            _playerManager.characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isDead", boolValue: true);
            _playerManager.isDead = true;
            _currentPlayerHealth = 0;
            EventSystem.OnPlayerDied?.Invoke();
        }

        EventSystem.UpdateHealthBarUI?.Invoke((int)GetPlayerFixedStatValue(StatType.HP), _currentPlayerHealth);
        //Debug.Log("PLAYER HEALTH : " + newHealth);
    }

    public int GetCurrentPlayerHealth()
    {
        return _currentPlayerHealth;
    }

    private bool CheckPlayerHealth()
    {
        if (_currentPlayerHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
