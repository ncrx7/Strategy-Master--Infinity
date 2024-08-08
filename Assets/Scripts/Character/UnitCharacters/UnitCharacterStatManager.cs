using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterStatManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    private UnitCharacterStat _unitCharacterStat;
    private PlayerStats _playerStats;
    public float currentHealth; //should be private if ref value is not necessary
    public float MaxHealth { get { return _unitCharacterStat.hp; } }

    [SerializeField]
    private Dictionary<CharacterClassType, int> _unitClassCoefficient = new Dictionary<CharacterClassType, int>()
    {
        {CharacterClassType.MEELE_FIGHTER, 1},
        {CharacterClassType.RIFLE, 1},
        {CharacterClassType.HEALER, 1},
        {CharacterClassType.MAGE, 1},
    };

    private void OnEnable()
    {
        //EventSystem.OnUnitCharacterTagged += InitUnitCharacterStat;
    }

    private void OnDisable()
    {
        //EventSystem.OnUnitCharacterTagged -= InitUnitCharacterStat;
        SetCurrentHealth(MaxHealth);
        _unitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (currentHealth, _unitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());
    }

    public void InitUnitCharacterStat()
    {
        _playerStats = PlayerStatusManager.Instance.GetPlayerStatObjectReference();
        int coefficient = _unitClassCoefficient[_unitCharacterManager.characterClassType];
        switch (_unitCharacterManager.characterOwnerType)
        {
            case CharacterOwnerType.PLAYER_UNIT:
                _unitCharacterStat = new UnitCharacterStat(_playerStats.hp * coefficient, _playerStats.ad * coefficient, _playerStats.ap / coefficient, _playerStats.dex * coefficient);
                Debug.Log("player stat init");
                currentHealth = _unitCharacterStat.hp;
                _unitCharacterManager.GetUnitCharacterHealthBarController().SetMaxValueSliderImage();
                break;
            case CharacterOwnerType.ENEMY_UNIT:
                int level = _playerStats.level;
                _unitCharacterStat = new UnitCharacterStat(200 * level, 10 * level, 50 * level, 50 * level);
                Debug.Log("enemy stat init");
                currentHealth = _unitCharacterStat.hp;
                _unitCharacterManager.GetUnitCharacterHealthBarController().SetMaxValueSliderImage();
                break;
            default:
                Debug.LogWarning("Undefined owner type..!!");
                break;
        }
    }

/*     public void UpdateFixedPlayerStat(StatType statType, float value)
    {
        _unitCharacterStat.SetStatValue(statType, value);

        if (statType == StatType.DEX)
        {
            //UpdateDamageReduceRate();
        }
        Debug.Log("new hp from playerstats: " + _unitCharacterStat.GetStatValue(StatType.HP));
        
    } */

    public float GetUnitCharacterFixedStatValue(StatType statType)
    {
        return _unitCharacterStat.GetStatValue(statType);
    }

    public void SetCurrentHealth(float newValue)
    {
        currentHealth = newValue;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return _unitCharacterStat.hp;
    }

    public bool CheckHealth()
    {
        if(currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
