using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] Animator _animator;

/*     private void OnEnable()
    {
        EventSystem.UpdateAnimatorParameter += SetAnimatorValue;
        EventSystem.PlayAnimation += HandlePlayAnimation;
    }

    private void OnDisable()
    {
        EventSystem.UpdateAnimatorParameter -= SetAnimatorValue;
        EventSystem.PlayAnimation -= HandlePlayAnimation;
    } */

    public void SetAnimatorValue(AnimatorParameterType type, string animatorParameterName, float floatValue = 0, int intValue = 0, bool boolValue = false)
    {
        switch (type)
        {
            case AnimatorParameterType.FLOAT:
                _animator.SetFloat(animatorParameterName, floatValue, 0.2f, Time.deltaTime);
                break;
            case AnimatorParameterType.INT:
                _animator.SetInteger(animatorParameterName, intValue);
                break;
            case AnimatorParameterType.BOOL:
                _animator.SetBool(animatorParameterName, boolValue);
                break;
            default:
                break;
        }
    }

    public void HandlePlayAnimation(string animationName)
    {
        _animator.CrossFade(animationName, 0.5f);
    }
}

public enum AnimatorParameterType
{
    FLOAT,
    BOOL,
    INT
}
