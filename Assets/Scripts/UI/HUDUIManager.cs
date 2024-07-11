using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] Image _healthImage;

    private void OnEnable()
    {
        EventSystem.UpdateRemainingTimeUI += SetRemainingTimeValueText;
        EventSystem.SetMaxHealthUI += HandleSetMaxHealth;
        EventSystem.UpdateHealthBarUI += HandleUpdateHealthUI;
    }

    private void OnDisable()
    {
        EventSystem.UpdateRemainingTimeUI -= SetRemainingTimeValueText;
        EventSystem.SetMaxHealthUI -= HandleSetMaxHealth;
        EventSystem.UpdateHealthBarUI -= HandleUpdateHealthUI;
    }

    private void SetRemainingTimeValueText()
    {
        _remainingTimeValueText.text = TimeManager.Instance.GetRemainingTimeForArena().ToString("0");
    
    }

    private void HandleSetMaxHealth()
    {
        _healthImage.fillAmount = 1f;
    }

    private void HandleUpdateHealthUI(int maxHealth, int currentHealth)
    {
        _healthImage.fillAmount = (float)currentHealth / maxHealth;
        _healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void RestartButton()
    {
        SceneControlManager.Instance.LoadTheLevelScene(1);
    }

    public void MainMenuButton()
    {
        SceneControlManager.Instance.LoadTheLevelScene(0);
    }
}
