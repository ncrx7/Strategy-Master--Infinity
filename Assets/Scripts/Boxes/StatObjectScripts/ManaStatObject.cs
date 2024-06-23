using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStatObject : MonoBehaviour, ICollectable
{
    private float ManaIncreasePoint = 25f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.MANA, playerStatManager.GetPlayerStatValue(StatType.MANA) + ManaIncreasePoint);
        Debug.Log("new mana : " + playerStatManager.GetPlayerStatValue(StatType.MANA));
    }

    public void PlaySoundEffect()
    {
        
    }
}
