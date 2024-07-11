using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEXStatObject : MonoBehaviour, ICollectable
{
    private float DEXIncreasePoint = 25f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.DEX, playerStatManager.GetPlayerFixedStatValue(StatType.DEX) + DEXIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.DEX, playerStatManager.GetPlayerFixedStatValue(StatType.DEX).ToString());
        //Debug.Log("new dex : " + playerStatManager.GetPlayerStatValue(StatType.DEX));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
