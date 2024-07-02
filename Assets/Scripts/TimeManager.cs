using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] private float _remainingTimeToFightArena = 30;
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;
    private bool _timeOut = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        RemainingTimeCounter();
    }

    private void RemainingTimeCounter()
    {
        if (!CheckTimeout())
        {
            _remainingTimeToFightArena = _remainingTimeToFightArena - (1 * Time.deltaTime);
        }
        else
        {
            EventSystem.OnTimeOutForEvolutionPhase?.Invoke();
        }

        //SetRemainingTimeValueText();
        EventSystem.UpdateRemainingTimeUI?.Invoke();
    }



    private bool CheckTimeout()
    {
        if (_remainingTimeToFightArena <= 0)
        {
            _timeOut = true;
        }
        else
        {
            _timeOut = false;
        }

        return _timeOut;
    }

    public float GetRemainingTimeForArena()
    {
        return _remainingTimeToFightArena;
    }
}