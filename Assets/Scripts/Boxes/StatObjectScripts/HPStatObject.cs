using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPStatObject : MonoBehaviour, ICollectable
{
    private float HealhtIncreasePoint = 50f;
    
    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.HP, playerStatManager.GetPlayerFixedStatValue(StatType.HP) + HealhtIncreasePoint);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.HP, playerStatManager.GetPlayerFixedStatValue(StatType.HP).ToString());
        EventSystem.UpdateHealthBarUI((int)playerStatManager.GetPlayerFixedStatValue(StatType.HP), playerStatManager.GetCurrentPlayerHealth());
        //Debug.Log("new hp : " + playerStatManager.GetPlayerStatValue(StatType.HP));
        Debug.Log("main scene status manager hp: " + PlayerStatusManager.Instance.GetPlayerStatObjectReference().GetStatValue(StatType.HP));
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.STATCOLLECT);
    }
}
