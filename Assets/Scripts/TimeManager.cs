using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] private float _remainingTimeToFightArena = 10;
    [SerializeField] private int _phaseTransitionTime;
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
        EventSystem.OnPlayerDefeat += StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase += StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase += HandlePhaseTransitionTime;
    }

    private void OnDisable()
    {
        EventSystem.OnPlayerDefeat -= StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase -= StopTimer;
        EventSystem.OnTimeOutForEvolutionPhase -= HandlePhaseTransitionTime;
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
             HandleOnTimeOutForEvolutionPhase();
        }

        //SetRemainingTimeValueText();
        EventSystem.UpdateRemainingTimeUI?.Invoke();
    }

    private async void HandleOnTimeOutForEvolutionPhase()
    {
        EventSystem.OnTimeOutForEvolutionPhase?.Invoke();
        await UniTask.Delay(_phaseTransitionTime * 1000);
        SceneControlManager.Instance.LoadTheLevelScene(3);
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

    private async void HandlePhaseTransitionTime()
    {
        int currentRemainingTime = _phaseTransitionTime;
        EventSystem.UpdatePhaseTransitionTimeUI?.Invoke(currentRemainingTime);
        
        while (currentRemainingTime > 0)
        {
            --currentRemainingTime ;
            EventSystem.UpdatePhaseTransitionTimeUI?.Invoke(currentRemainingTime);
            await UniTask.Delay(1000);
        }
    }

    public float GetRemainingTimeForArena() => _remainingTimeToFightArena;
   

    private void StopTimer()
    {
        _stoppedTime = true;
    }
}
