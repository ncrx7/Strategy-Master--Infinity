using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatManager _playerStatManager;
    [SerializeField] private List<StatUIElement> _statUIElements;

    private void Start()
    {
        SetInitialStatTexts();
    }

    private void OnEnable()
    {
        EventSystem.UpdateStatUIText += HandleUpdatingStatUIText;
    }

    private void OnDisable()
    {
        EventSystem.UpdateStatUIText -= HandleUpdatingStatUIText;
    }

    private void HandleUpdatingStatUIText(StatUIType statUIType, string value)
    {
        foreach (StatUIElement statUIElement in _statUIElements)
        {
            if(statUIElement.statUIType == statUIType)
            {
                statUIElement.textMesh.text = value;
                Debug.Log("type : " + statUIElement.statUIType + " value" + value.ToString());
                Debug.Log("--------------------------------");
            }
        }
    }

    private void SetInitialStatTexts()
    {
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.INT, _playerStatManager.GetPlayerFixedStatValue(StatType.INT).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.PF, _playerStatManager.GetPlayerFixedStatValue(StatType.PF).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.HP, _playerStatManager.GetPlayerFixedStatValue(StatType.HP).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MP, _playerStatManager.GetPlayerFixedStatValue(StatType.MP).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.DEX, _playerStatManager.GetPlayerFixedStatValue(StatType.DEX).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MONEY_COLLECTED, _playerStatManager.GetPlayerFixedStatValue(StatType.MONEY_COLLECTED).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.CHARACTER_POINT, _playerStatManager.GetPlayerFixedStatValue(StatType.CHARACTER_POINT).ToString());
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.LEVEL, "Level " + _playerStatManager.GetPlayerFixedStatValue(StatType.LEVEL).ToString());
    }
}

[Serializable]
public class StatUIElement
{
    public TextMeshProUGUI textMesh;
    public StatUIType statUIType;

}

public enum StatUIType
{
    INT,
    PF,
    DEX,
    HP,
    MP,
    MONEY_COLLECTED,
    CHARACTER_POINT,
    LEVEL
}
