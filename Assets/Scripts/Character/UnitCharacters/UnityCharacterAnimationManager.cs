using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCharacterAnimationManager : CharacterAnimationManager
{
    private void Start()
    {
        SetAnimator(GetComponentInChildren<Animator>());
    }

/*     private void OnEnable()
    {
        EventSystem.OnUnitModelInstantiated += SetAnimatorOnModelInit;
    }

    private void OnDisable()
    {
        EventSystem.OnUnitModelInstantiated -= SetAnimatorOnModelInit;
    }

    private void SetAnimatorOnModelInit(Animator animator)
    {
        Debug.Log($"this is the event animator {animator}");
        SetAnimator(animator);
    } */
}
