using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADStatObject : MonoBehaviour, ICollectable
{
    private float ADIncreasePoint = 15f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.PF, playerStatManager.GetPlayerStatValue(StatType.PF) + ADIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.PF, playerStatManager.GetPlayerStatValue(StatType.PF));
        //Debug.Log("new ad : " + playerStatManager.GetPlayerStatValue(StatType.PF));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
