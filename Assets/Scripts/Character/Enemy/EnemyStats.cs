using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyStats(float hp, int ad)
    {
        base.hp = hp;
        base.ad = ad;
    }

    public float GetStatValue(StatType statType)
    {
        switch (statType)
        {
            case StatType.HP:
                return hp;
            case StatType.PF:
                return ad;
            default:
                return 0f;
        }
    }
}
