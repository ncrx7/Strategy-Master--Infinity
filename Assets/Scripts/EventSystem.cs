using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem 
{
    //public static Action <CharacterAnimatorType, string> PlayAnimation;
    //public static Action<CharacterAnimatorType, AnimatorParameterType, string, float, int, bool> UpdateAnimatorParameter;
    public static Action<PlayerManager> OnPlayerEnabledOnScene;
    public static Action<SoundType> PlaySoundClip;
    public static Action OnTimeOutForEvolutionPhase;
    public static Action OnPlayerDefeat;
    public static Action StopTheFire;

    #region UIAction
    public static Action UpdateRemainingTimeUI;
    public static Action<StatUIType, string> UpdateStatUIText;
    public static Action SetMaxHealthUI;
    public static Action<int, int> UpdateHealthBarUI;
    #endregion


    #region ENEMY EVENTS
    public static Action<EnemyManager, Action<EnemyManager>> OnEnemyEnabledOnScene;
    public static Action<EnemyManager> OnEnemyEnabledOnSceneCallback;
    public static Action OnEnemyStatsInitialized;
    //public static Action<Transform> MoveEnemyToTarget;
    //public static Action StopTheEnemy;
    #endregion

    #region UNIT CHARACTER EVENTS
    //public static Action<Animator> OnUnitModelInstantiated;
    #endregion
}
