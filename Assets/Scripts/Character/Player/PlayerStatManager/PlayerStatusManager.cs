using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance { get; private set; }
    PlayerStats _playerStats;
    int _playerLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _playerStats = new PlayerStats(10, 10, 10, 15, 5);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerStats GetPlayerStatObjectReference()
    {
        return _playerStats;
    }
}
