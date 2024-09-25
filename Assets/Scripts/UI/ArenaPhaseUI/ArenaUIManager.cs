using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    [Header("Progress Bars")]
    [SerializeField] private Slider _spawnProgressBar;
    [Header("UI Menus")]
    [SerializeField] private GameObject _unitCharacterSelectionUI;

    [Header("Images")]
    [SerializeField] private Image _playerManaBarSlider;
    [SerializeField] private Image _playerHealthBarSlider;
    [SerializeField] private Image _enemyHealthBarSlider;

    private void OnEnable()
    {
        EventSystem.SetSliderBarValue += HandleSetSliderBarValue;
        EventSystem.ChangeBarVisibility += HandleChangingBarVisibility;
    }

    private void OnDisable()
    {
        EventSystem.SetSliderBarValue -= HandleSetSliderBarValue;
        EventSystem.ChangeBarVisibility -= HandleChangingBarVisibility;
    }

    public void HandleSetSliderBarValue(BarType barType, float newValue, float maxValue, BaseType baseType)
    {
        if (barType == BarType.HEALTH_BAR)
        {
            switch (baseType)
            {
                case BaseType.OPPOSING_BASE:
                    _enemyHealthBarSlider.SetSliderBarValue(newValue, maxValue);
                    break;
                case BaseType.PLAYER_BASE:
                    _playerHealthBarSlider.SetSliderBarValue(newValue, maxValue);
                    break;
                default:
                    break;
            }
        }
        else if (barType == BarType.MANA_BAR)
        {
            _playerManaBarSlider.SetSliderBarValue(newValue, maxValue);
        }
        else if (barType == BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR)
        {
            _spawnProgressBar.value = newValue;
        }
    }

    private void HandleChangingBarVisibility(BarType barType, bool state)
    {
        switch (barType)
        {
            case BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR:
                _spawnProgressBar.gameObject.SetActive(state);
                break;
        }
    }

    public void SwitchUnitCharacterSelectionUIVisibility()
    {
        _unitCharacterSelectionUI.SetActive(!_unitCharacterSelectionUI.activeSelf);
    }

    public void SelectUnitCharacter(int index)
    {
        CharacterClassType characterClassType;

        switch (index)
        {
            case 0:
                characterClassType = CharacterClassType.MEELE_FIGHTER;
                break;
            case 1:
                characterClassType = CharacterClassType.MAGE;
                break;
            case 2:
                characterClassType = CharacterClassType.RIFLE;
                break;
            case 3:
                characterClassType = CharacterClassType.HEALER;
                break;
            default:
                characterClassType = CharacterClassType.MEELE_FIGHTER;
                break;
        }
        EventSystem.CreateUnitCharacter?.Invoke(index, characterClassType, EventSystem.SpReduce);
    }

}

public enum BarType
{
    HEALTH_BAR,
    MANA_BAR,
    UNITCHARACTER_SPAWN_PROGRESS_BAR
}
