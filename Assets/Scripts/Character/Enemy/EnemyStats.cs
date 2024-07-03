using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyStats(float hp, int ad)
    {
        _hp = hp;
        _ad = ad;
    }

    public float GetStatValue(StatType statType)
    {
        switch (statType)
        {
            case StatType.HP:
                return _hp;
            case StatType.PF:
                return _ad;
            default:
                return 0f;
        }
    }
}
