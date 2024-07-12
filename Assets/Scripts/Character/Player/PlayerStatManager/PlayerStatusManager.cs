using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance { get; private set; }
    PlayerStats _playerStats;
    PlayerDataWriterAndReader _playerDataWriterAndReader;
    int _playerLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _playerDataWriterAndReader = new PlayerDataWriterAndReader(Application.persistentDataPath, "Player_Stat_Data");
            //_playerStats = new PlayerStats(10, 10, 10, 15, 5);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePlayerStatsFile();
/*         Debug.Log("play hp stats object from status manager : " + _playerStats.GetStatValue(StatType.HP));
        Debug.Log("play pf stats object from status manager : " + _playerStats.GetStatValue(StatType.PF));
         Debug.Log("play level stats object from status manager : " + _playerStats.GetStatValue(StatType.LEVEL)); */
    }

    public void InitializePlayerStatsFile()
    {
        _playerStats = _playerDataWriterAndReader.InitializePlayerStatsFile();
    }

    public void UpdatePlayerStatsFile()
    {
        _playerDataWriterAndReader.UpdatePlayerStatsFile(_playerStats);
    }

    public PlayerStats CreateNewPlayerStatObject()
    {
        PlayerStats playerStats = new PlayerStats(60, 10, 10, 15, 5, 0, 0, 1);
        return playerStats;
    }

    public PlayerStats GetPlayerStatObjectReference()
    {
        return _playerStats;
    }

    public void SetPlayerStatObjectReference(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }
}
