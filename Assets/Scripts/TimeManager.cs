using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] private float _remainingTimeToFightArena = 10;
    [SerializeField] TextMeshProUGUI _remainingTimeValueText;
    private bool _timeOut = false;
    private bool _stoppedTime = false;

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

    private void OnEnable()
    {
        EventSystem.OnPlayerDied += StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase += StopTimer;
    }

    private void OnDisable()
    {
        EventSystem.OnPlayerDied -= StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase -= StopTimer;
    }

    private void Start()
    {
        Application.targetFrameRate = 360;
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        if(_stoppedTime)
            return;

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

    private void StopTimer()
    {
        _stoppedTime = true;
    }
}
