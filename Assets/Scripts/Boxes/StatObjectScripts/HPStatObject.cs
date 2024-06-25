using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPStatObject : MonoBehaviour, ICollectable
{
    private float HealhtIncreasePoint = 50f;
    
    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdatePlayerStat(StatType.HP, playerStatManager.GetPlayerStatValue(StatType.HP) + HealhtIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.HP, playerStatManager.GetPlayerStatValue(StatType.HP));
        Debug.Log("new hp : " + playerStatManager.GetPlayerStatValue(StatType.HP));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
