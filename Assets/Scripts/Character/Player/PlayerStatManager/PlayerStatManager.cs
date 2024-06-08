using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = new PlayerStats(30, 10, 10, 15, 5);
    }
}
