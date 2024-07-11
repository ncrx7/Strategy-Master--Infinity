using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADStatObject : MonoBehaviour, ICollectable
{
    private float ADIncreasePoint = 15f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.PF, playerStatManager.GetPlayerFixedStatValue(StatType.PF) + ADIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.PF, playerStatManager.GetPlayerFixedStatValue(StatType.PF).ToString());
        //Debug.Log("new ad : " + playerStatManager.GetPlayerStatValue(StatType.PF));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
