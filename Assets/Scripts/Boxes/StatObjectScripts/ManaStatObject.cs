using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStatObject : MonoBehaviour, ICollectable
{
    private float ManaIncreasePoint = 25f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.MP, playerStatManager.GetPlayerFixedStatValue(StatType.MP) + ManaIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MP, playerStatManager.GetPlayerFixedStatValue(StatType.MP).ToString());
        //Debug.Log("new mana : " + playerStatManager.GetPlayerStatValue(StatType.MP));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
