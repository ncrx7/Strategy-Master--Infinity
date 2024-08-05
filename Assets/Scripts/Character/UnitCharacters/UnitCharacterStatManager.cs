using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacterStatManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    private UnitCharacterStat _unitCharacterStat;
    private PlayerStats _playerStats;
    public float currentHealth; //should be private if ref value is not necessary
    public float MaxHealth { get { return _unitCharacterStat.hp;} }

    private void Update()
    {
        Debug.Log("unit character health: " + currentHealth);
    }

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
                _unitCharacterStat = new UnitCharacterStat(200 * _playerStats.level, 75 * _playerStats.level, 50 * _playerStats.ap, 50 * _playerStats.level);
                Debug.Log("enemy stat init");
                currentHealth = _unitCharacterStat.hp;
                _unitCharacterManager.GetUnitCharacterHealthBarController().SetMaxValueSliderImage();
                break;
            default:
                Debug.LogWarning("Undefined owner type..!!");
                break;
        }
    }

    public float GetMaxHealth()
    {
        return _unitCharacterStat.hp;
    }
}
