using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : CharacterStats
{
    //include _hp and _ad from base class
    public int ap;
    public int dex;
    public float mana;

    public PlayerStats(float hp, int ad, int ap, int dex, float mana)
    {
        this.hp = hp;
        this.ad = ad;
        this.ap = ap;
        this.dex = dex;
        this.mana = mana;
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
            case StatType.MP:
                mana = (float)value;
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
            case StatType.MP:
                return mana;
            default:
                return 0f;
        }
    }
}

public enum StatType
{
    HP,
    PF,
    INT,
    DEX,
    MP
}
