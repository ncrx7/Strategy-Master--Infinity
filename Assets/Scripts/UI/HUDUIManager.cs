using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;

    private void OnEnable()
    {
        EventSystem.UpdateRemainingTimeUI += SetRemainingTimeValueText;
    }

    private void OnDisable()
    {
        EventSystem.UpdateRemainingTimeUI -= SetRemainingTimeValueText;
    }

    private void SetRemainingTimeValueText()
    {
        _remainingTimeValueText.text = TimeManager.Instance.GetRemainingTimeForArena().ToString("0");
    
    }
}
