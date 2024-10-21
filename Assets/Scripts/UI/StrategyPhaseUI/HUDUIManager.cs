using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;
    [SerializeField] TextMeshProUGUI _transitionRemainingTimeValueText;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] Image _healthImage;
    [SerializeField] GameObject _defeatUI;

    private void OnEnable()
    {
        EventSystem.UpdateRemainingTimeUI += SetRemainingTimeValueText;
        EventSystem.UpdatePhaseTransitionTimeUI += HandlePhaseTransitionTimeUI;
        EventSystem.SetMaxHealthUI += HandleSetMaxHealth;
        EventSystem.UpdateHealthBarUI += HandleUpdateHealthUI;
        EventSystem.OnPlayerDefeat += ActivateDefeatUI;
    }

    private void OnDisable()
    {
        EventSystem.UpdateRemainingTimeUI -= SetRemainingTimeValueText;
        EventSystem.UpdatePhaseTransitionTimeUI -= HandlePhaseTransitionTimeUI;
        EventSystem.SetMaxHealthUI -= HandleSetMaxHealth;
        EventSystem.UpdateHealthBarUI -= HandleUpdateHealthUI;
        EventSystem.OnPlayerDefeat -= ActivateDefeatUI;
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

    private void ActivateDefeatUI()
    {
        _defeatUI.SetActive(true);
    }

    private void HandlePhaseTransitionTimeUI(int time)
    {
        _transitionRemainingTimeValueText.text = time.ToString();
        _transitionRemainingTimeValueText.gameObject.SetActive(true);
        
        _transitionRemainingTimeValueText.transform.localScale = Vector3.one * 2; 
        _transitionRemainingTimeValueText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad);
    }
}
