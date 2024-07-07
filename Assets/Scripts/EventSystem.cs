using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem 
{
    //public static Action <CharacterAnimatorType, string> PlayAnimation;
    //public static Action<CharacterAnimatorType, AnimatorParameterType, string, float, int, bool> UpdateAnimatorParameter;
    public static Action<SoundType> PlaySoundClip;
    public static Action OnTimeOutForEvolutionPhase;

    #region UIAction
    public static Action UpdateRemainingTimeUI;
    public static Action<StatUIType, float> UpdateStatUIText;
    public static Action SetMaxHealth;
    public static Action<int, int> UpdateHealthBar;
    #endregion


    #region ENEMY EVENTS
    public static Action OnEnemyStatsInitialized;
    //public static Action<Transform> MoveEnemyToTarget;
    //public static Action StopTheEnemy;
    #endregion
}
