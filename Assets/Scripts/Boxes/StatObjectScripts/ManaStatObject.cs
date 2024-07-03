using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStatObject : MonoBehaviour, ICollectable
{
    private float ManaIncreasePoint = 25f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.MP, playerStatManager.GetPlayerStatValue(StatType.MP) + ManaIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MP, playerStatManager.GetPlayerStatValue(StatType.MP));
        //Debug.Log("new mana : " + playerStatManager.GetPlayerStatValue(StatType.MP));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
