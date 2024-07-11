using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectable : MonoBehaviour, ICollectable
{
    private float MoneyIncreasePoint = 20f;

    public void Collect(PlayerStatManager playerStatManager)
    {
        playerStatManager.UpdateFixedPlayerStat(StatType.MONEY_COLLECTED, playerStatManager.GetPlayerFixedStatValue(StatType.MONEY_COLLECTED) + MoneyIncreasePoint);
        Debug.Log("new money: " + playerStatManager.GetPlayerFixedStatValue(StatType.MONEY_COLLECTED));
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MONEY_COLLECTED, playerStatManager.GetPlayerFixedStatValue(StatType.MONEY_COLLECTED).ToString());
    }

    public void PlaySoundEffect()
    {
        EventSystem.PlaySoundClip?.Invoke(SoundType.MONEY_COLLECT);
    }
}
