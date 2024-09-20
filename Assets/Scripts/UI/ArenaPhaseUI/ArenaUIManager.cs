using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    [Header("UI Menus")]
    [SerializeField] private GameObject _unitCharacterSelectionUI;

    [Header("Images")]
    [SerializeField] private Image _playerManaBarSlider;
    [SerializeField] private Image _playerHealthBarSlider;
    [SerializeField] private Image _enemyHealthBarSlider;

    private void OnEnable()
    {
        EventSystem.SetSliderBarValue += HandleSetSliderBarValue;
    }

    private void OnDisable()
    {
        EventSystem.SetSliderBarValue -= HandleSetSliderBarValue;
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
    }

    public void SwitchUnitCharacterSelectionUIVisibility()
    {
        _unitCharacterSelectionUI.SetActive(!_unitCharacterSelectionUI.activeSelf);
    }

    public void SelectUnitCharacter(int index)
    {
        EventSystem.CreateUnitCharacter?.Invoke(index);
        Debug.Log("clickec select unit character for creating");
    }
}

public enum BarType
{
    HEALTH_BAR,
    MANA_BAR
}
