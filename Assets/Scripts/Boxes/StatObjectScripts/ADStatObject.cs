using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADStatObject : MonoBehaviour, ICollectable
{
    private float ADIncreasePoint = 15f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.AD, playerStatManager.GetPlayerStatValue(StatType.AD) + ADIncreasePoint);
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
        Debug.Log("new ad : " + playerStatManager.GetPlayerStatValue(StatType.AD));
    }

    public void PlaySoundEffect()
    {
        
    }
}
