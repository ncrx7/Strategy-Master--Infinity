using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCharacterHealthBarController : MonoBehaviour
{
    [SerializeField] private Image _healthBarSlideImage;
    [SerializeField] private GameObject _healthBar;

    public void SetMaxValueSliderImage()
    {
        _healthBarSlideImage.fillAmount = 1;
    }

    public void SetCurrentValueSliderImage(float newHealth, float maxHealth)
    {
        float healthRate = newHealth / maxHealth;
        _healthBarSlideImage.fillAmount = healthRate;
    }

    public void SwitchHealthBarVisibility()
    {
        _healthBar.SetActive(!_healthBar.activeSelf);
        Debug.Log("switchhealth bar vis. processed");
    }
}
