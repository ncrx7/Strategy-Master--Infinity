using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterStat : CharacterStats
{
    public int ap;
    public int dex;

    public UnitCharacterStat(float hp, int ad, int ap, int dex)
    {
        this.hp = hp;
        this.ad = ad;
        this.ap = ap;
        this.dex = dex;
    }

    public void SetStatValue(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.HP:
                hp = (float)value;
                break;
            case StatType.PF:
                ad = (int)value;
                break;
            case StatType.INT:
                ap = (int)value;
                break;
            case StatType.DEX:
                dex = (int)value;
                break;
            default:
                break;
        }
    }

    public float GetStatValue(StatType statType)
    {
        switch (statType)
        {
            case StatType.HP:
                return hp;
            case StatType.PF:
                return ad;
            case StatType.INT:
                return ap;
            case StatType.DEX:
                return dex;
            default:
                return 0f;
        }
    }
}
