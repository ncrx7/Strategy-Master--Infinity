using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem 
{
    public static Action<AnimatorParameterType, string, float, int, bool> UpdateAnimatorParameter;
    public static Action<SoundType> PlaySoundClip;
    public static Action OnTimeOutForEvolutionPhase;

    #region UIAction
    public static Action UpdateRemainingTimeUI;
    #endregion
}
