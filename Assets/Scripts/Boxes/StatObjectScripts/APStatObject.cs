using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APStatObject : MonoBehaviour, ICollectable
{
    private float APIncreasePoint = 30f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.AP, playerStatManager.GetPlayerStatValue(StatType.AP) + APIncreasePoint);
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
        Debug.Log("new ap : " + playerStatManager.GetPlayerStatValue(StatType.AP));
    }

    public void PlaySoundEffect()
    {

    }
}
