using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : CharacterManager
{
    [SerializeField] private PlayerStatManager _playerStatManager;
    public bool isDead { get; set;}
    public bool isVictory { get; set;}

    private void OnEnable()
    {
        EventSystem.OnTimeOutForEvolutionPhase += HandlePlayPlayerVictoryAnimation;
        EventSystem.OnTimeOutForEvolutionPhase += IncreaseCharacterPoint;
        EventSystem.OnTimeOutForEvolutionPhase += () => EventSystem.PlaySoundClip?.Invoke(SoundType.VICTORY);
        EventSystem.OnPlayerDied += () => EventSystem.PlaySoundClip?.Invoke(SoundType.DEFEAT);
    }

    private void OnDisable()
    {
        EventSystem.OnTimeOutForEvolutionPhase -= HandlePlayPlayerVictoryAnimation;
        EventSystem.OnTimeOutForEvolutionPhase -= IncreaseCharacterPoint;
        EventSystem.OnTimeOutForEvolutionPhase -= () => EventSystem.PlaySoundClip?.Invoke(SoundType.VICTORY);
        EventSystem.OnPlayerDied -= () => EventSystem.PlaySoundClip?.Invoke(SoundType.DEFEAT);
    }

    public override void Start()
    {
        base.Start();

        EventSystem.OnPlayerEnabledOnScene?.Invoke(this);
    }

    // Update is called once per frame
/*     public override void Update()
    {
        base.Update();
    }  */

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable collectableObject))
        {
            collectableObject.Collect(_playerStatManager);
            Destroy(((MonoBehaviour)collectableObject).gameObject);
            collectableObject.PlaySoundEffect();
        }
    }

    private void HandlePlayPlayerDeadAnimation()
    {
        characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isDead", boolValue: true);
        isDead = true;
    }

    private void HandlePlayPlayerVictoryAnimation()
    {
        characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isVictory", boolValue: true);
        isVictory = true;
    }

    private void IncreaseCharacterPoint()
    {
        _playerStatManager.UpdateFixedPlayerStat(StatType.CHARACTER_POINT, _playerStatManager.GetPlayerFixedStatValue(StatType.CHARACTER_POINT) + 1);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.CHARACTER_POINT, _playerStatManager.GetPlayerFixedStatValue(StatType.CHARACTER_POINT));
    }
}
