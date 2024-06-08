using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : CharacterStats
{
    //include _hp and _ad from base class
    private int _ap;
    private int _dex;
    private float _mana;

    public PlayerStats(float hp, int ad, int ap, int dex, float mana)
    {
        this._hp = hp;
        this._ad = ad;
        this._ap = ap;
        this._dex = dex;
        this._mana = mana;
    }

    public void SetStatValue(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.HP:
                _hp = (float)value;
                break;
            case StatType.AD:
                _ad = (int)value;
                break;
            case StatType.AP:
                _ap = (int)value;
                break;
            case StatType.DEX:
                _dex = (int)value;
                break;
            case StatType.MANA:
                _mana = (float)value;
                break;
            default:
                break;
        }
    }
}

public enum StatType
{
    HP,
    AD,
    AP,
    DEX,
    MANA
}
