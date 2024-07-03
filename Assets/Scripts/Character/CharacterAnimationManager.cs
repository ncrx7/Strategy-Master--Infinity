using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] Animator _animator;
    [SerializeField] private CharacterAnimatorType _currentCharacterAnimatorType;

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

    public void SetAnimatorValue(CharacterAnimatorType characterAnimatorType, AnimatorParameterType type, string animatorParameterName, float floatValue = 0, int intValue = 0, bool boolValue = false)
    {
        if (_currentCharacterAnimatorType != characterAnimatorType)
            return;

        switch (type)
        {
            case AnimatorParameterType.FLOAT:
                _animator.SetFloat(animatorParameterName, floatValue, 0.2f, Time.deltaTime);
                Debug.Log("parameter name: " + animatorParameterName);
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

    public void HandlePlayAnimation(CharacterAnimatorType characterAnimatorType, string animationName)
    {
        if (_currentCharacterAnimatorType != characterAnimatorType)
            return;

        _animator.CrossFade(animationName, 0.5f);
    }
}

public enum AnimatorParameterType
{
    FLOAT,
    BOOL,
    INT
}

public enum CharacterAnimatorType
{
    ENEMY_ANIMATOR,
    PLAYER_ANIMATOR
}
