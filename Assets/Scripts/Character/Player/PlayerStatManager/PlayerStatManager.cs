using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    PlayerStats _playerStats;

    private void Awake()
    {
        //IF NEW GAME CREATED
        CreateNewPlayerStat();
        //IF EXISTING GAME
        //PopulatePlayerStat();

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

    public void UpdatePlayerStat(StatType statType, float value)
    {
        _playerStats.SetStatValue(statType, value);
    }

    public float GetPlayerStatValue(StatType statType)
    {
        return _playerStats.GetStatValue(statType);
    }
}
