using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class APStatObject : MonoBehaviour, ICollectable
{
    private float APIncreasePoint = 30f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.INT, playerStatManager.GetPlayerFixedStatValue(StatType.INT) + APIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.INT, playerStatManager.GetPlayerFixedStatValue(StatType.INT).ToString());
        //Debug.Log("new ap : " + playerStatManager.GetPlayerStatValue(StatType.INT));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
