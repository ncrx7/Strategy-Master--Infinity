using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    PlayerStats _currentPlayerStatsOnCurrentLevel;
    PlayerStats _initialStatsOnCurrentLevel;
    [SerializeField] private PlayerManager _playerManager;

    [Header("Current Stats")]
    [SerializeField] private int _currentPlayerHealth;
    public float DamageReduceRate { get; private set;}

    private void Awake()
    {
        //IF NEW GAME CREATED
        InitPlayerStats();
        _currentPlayerHealth = (int)GetPlayerFixedStatValue(StatType.HP);
        CalculateDamageReduceRate();
        //IF EXISTING GAME
        //PopulatePlayerStat();

    }

    private void OnEnable()
    {
        EventSystem.OnTimeOutForEvolutionPhase += PlayerStatusManager.Instance.UpdatePlayerStatsFile;
        EventSystem.OnPlayerDefeat += () => PlayerStatusManager.Instance.SetPlayerStatObjectReference(_initialStatsOnCurrentLevel);
    }

    private void OnDisable()
    {
        EventSystem.OnTimeOutForEvolutionPhase -= PlayerStatusManager.Instance.UpdatePlayerStatsFile;
        EventSystem.OnPlayerDefeat -= () => PlayerStatusManager.Instance.SetPlayerStatObjectReference(_initialStatsOnCurrentLevel);
    }

    private void Start()
    {
        EventSystem.SetMaxHealthUI?.Invoke();
        EventSystem.UpdateHealthBarUI?.Invoke((int)GetPlayerFixedStatValue(StatType.HP), _currentPlayerHealth);
    }

    private void InitPlayerStats()
    {
        //DEFAULT PLAYER STAT VALUE
        _currentPlayerStatsOnCurrentLevel = PlayerStatusManager.Instance.GetPlayerStatObjectReference();
        _initialStatsOnCurrentLevel = _currentPlayerStatsOnCurrentLevel.Clone();
        Debug.Log("player stat hp : " + GetPlayerFixedStatValue(StatType.HP));
        Debug.Log("player stat ad : " + GetPlayerFixedStatValue(StatType.PF));
        Debug.Log("player stat level : " + GetPlayerFixedStatValue(StatType.LEVEL));
    }

    private void PopulatePlayerStat()
    {
        //DATA COME FROM DATABSE AND POPULATE STAT CLASS
    }

    public void UpdateFixedPlayerStat(StatType statType, float value)
    {
        _currentPlayerStatsOnCurrentLevel.SetStatValue(statType, value);

        if(statType == StatType.DEX)
        {
            UpdateDamageReduceRate();
        }
        Debug.Log("new hp from playerstats: " + _currentPlayerStatsOnCurrentLevel.GetStatValue(StatType.HP));
        Debug.Log("initial hp from playerstats: " + _initialStatsOnCurrentLevel.GetStatValue(StatType.HP));
    }

    public float GetPlayerFixedStatValue(StatType statType)
    {
        return _currentPlayerStatsOnCurrentLevel.GetStatValue(statType);
    }
    
    private void UpdateDamageReduceRate()
    {
        CalculateDamageReduceRate();
        Debug.Log("dmg reduce rate : " + DamageReduceRate);
    }

    private void CalculateDamageReduceRate()
    {
        DamageReduceRate = (int)(GetPlayerFixedStatValue(StatType.DEX) / (GetPlayerFixedStatValue(StatType.DEX) + 150) * 100);
    }

    public void SetCurrentPlayerHealth(int newHealth)
    {
        _currentPlayerHealth = newHealth;
        
        if(CheckPlayerHealth())
        {
            //TODO: ACTIVATE DEAD UI
            _playerManager.isDead = true;
            _currentPlayerHealth = 0;
            EventSystem.OnPlayerDefeat?.Invoke();
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
