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

    private void HandleUpdatingStatUIText(StatUIType statUIType, float value)
    {
        foreach (StatUIElement statUIElement in _statUIElements)
        {
            if(statUIElement.statUIType == statUIType)
            {
                statUIElement.textMesh.text = value.ToString();
            }
        }
    }

    private void SetInitialStatTexts()
    {
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.INT, _playerStatManager.GetPlayerFixedStatValue(StatType.INT));
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.PF, _playerStatManager.GetPlayerFixedStatValue(StatType.PF));
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.HP, _playerStatManager.GetPlayerFixedStatValue(StatType.HP));
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.MP, _playerStatManager.GetPlayerFixedStatValue(StatType.MP));
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.DEX, _playerStatManager.GetPlayerFixedStatValue(StatType.DEX));
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
    MP
}
