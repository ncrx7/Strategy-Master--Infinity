using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerManager : CharacterManager
{
    [SerializeField] private PlayerStatManager _playerStatManager;
    public bool isDead { get; set; }
    public bool isFallen { get; set; }
    public bool isVictory { get; set; }

    private void OnEnable()
    {
        EventSystem.OnTimeOutForEvolutionPhase += HandlePlayPlayerVictoryAnimation;
        EventSystem.OnTimeOutForEvolutionPhase += IncreaseCharacterPoint;
        EventSystem.OnTimeOutForEvolutionPhase += () => EventSystem.PlaySoundClip?.Invoke(SoundType.VICTORY);
        EventSystem.OnTimeOutForEvolutionPhase += IncreaseLevel;
        EventSystem.OnPlayerDefeat += HandleDefeatAnimation;
        EventSystem.OnPlayerDefeat += () => EventSystem.PlaySoundClip?.Invoke(SoundType.DEFEAT);
        EventSystem.OnEnemyEnabledOnScene += (EnemyManager enemyManager, Action<EnemyManager> callback) =>
        {
            enemyManager.InitializePlayerManagerOnPlayerEnabled(this);
            callback?.Invoke(enemyManager);
        };

        EventSystem.OnEnemyEnabledOnSceneCallback += (EnemyManager enemyManager) =>
        {
            enemyManager.InitPlayerStat(_playerStatManager);
        };
    }

    private void OnDisable()
    {
        EventSystem.OnTimeOutForEvolutionPhase -= HandlePlayPlayerVictoryAnimation;
        EventSystem.OnTimeOutForEvolutionPhase -= IncreaseCharacterPoint;
        EventSystem.OnTimeOutForEvolutionPhase -= () => EventSystem.PlaySoundClip?.Invoke(SoundType.VICTORY);
        EventSystem.OnPlayerDefeat -= () => EventSystem.PlaySoundClip?.Invoke(SoundType.DEFEAT);
        EventSystem.OnPlayerDefeat -= HandleDefeatAnimation;
        EventSystem.OnTimeOutForEvolutionPhase -= IncreaseLevel;
        EventSystem.OnEnemyEnabledOnScene -= (EnemyManager enemyManager, Action<EnemyManager> callback) =>
        {
            enemyManager.InitializePlayerManagerOnPlayerEnabled(this);
            callback?.Invoke(enemyManager);
        };

        EventSystem.OnEnemyEnabledOnSceneCallback -= (EnemyManager enemyManager) =>
        {
            enemyManager.InitPlayerStat(_playerStatManager);
        };
    }

    public override void Start()
    {
        base.Start();
        //string id = Guid.NewGuid().ToString();
        
        //EventSystem.OnPlayerEnabledOnScene?.Invoke(this);
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

    private void HandleDefeatAnimation()
    {
        if (isDead)
        {
            characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isDead", boolValue: true);
        }
        else if (isFallen)
        {
            characterAnimationManager.SetAnimatorValue(AnimatorParameterType.BOOL, "isFallen", boolValue: true);
        }
    }

    private void IncreaseCharacterPoint()
    {
        _playerStatManager.UpdateFixedPlayerStat(StatType.CHARACTER_POINT, _playerStatManager.GetPlayerFixedStatValue(StatType.CHARACTER_POINT) + 5);
        EventSystem.UpdateStatUIText?.Invoke(StatUIType.CHARACTER_POINT, _playerStatManager.GetPlayerFixedStatValue(StatType.CHARACTER_POINT).ToString());
    }

    private void IncreaseLevel()
    {
        _playerStatManager.UpdateFixedPlayerStat(StatType.LEVEL, _playerStatManager.GetPlayerFixedStatValue(StatType.LEVEL) + 1);
    }
}
