using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;
    [SerializeField] Image _healthImage;

    private void OnEnable()
    {
        EventSystem.UpdateRemainingTimeUI += SetRemainingTimeValueText;
        EventSystem.SetMaxHealth += HandleSetMaxHealth;
        EventSystem.UpdateHealthBar += HandleUpdateHealth;
    }

    private void OnDisable()
    {
        EventSystem.UpdateRemainingTimeUI -= SetRemainingTimeValueText;
        EventSystem.SetMaxHealth -= HandleSetMaxHealth;
        EventSystem.UpdateHealthBar -= HandleUpdateHealth;
    }

    private void SetRemainingTimeValueText()
    {
        _remainingTimeValueText.text = TimeManager.Instance.GetRemainingTimeForArena().ToString("0");
    
    }

    private void HandleSetMaxHealth()
    {
        _healthImage.fillAmount = 1f;
    }

    private void HandleUpdateHealth(int maxHealth, int currentHealth)
    {
        _healthImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
