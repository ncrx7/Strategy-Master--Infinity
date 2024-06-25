using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEXStatObject : MonoBehaviour, ICollectable
{
    private float DEXIncreasePoint = 25f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.DEX, playerStatManager.GetPlayerStatValue(StatType.DEX) + DEXIncreasePoint);
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
        Debug.Log("new dex : " + playerStatManager.GetPlayerStatValue(StatType.DEX));
    }

    public void PlaySoundEffect()
    {

    }
}
