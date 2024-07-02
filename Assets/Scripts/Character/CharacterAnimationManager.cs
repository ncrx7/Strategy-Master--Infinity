using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] Animator _animator;
    [SerializeField] private CharacterAnimatorType _currentCharacterAnimatorType;
    
    private void OnEnable()
    {
        EventSystem.UpdateAnimatorParameter += SetAnimatorValue;
    }

    private void OnDisable()
    {
        EventSystem.UpdateAnimatorParameter -= SetAnimatorValue;
    }

    private void SetAnimatorValue(CharacterAnimatorType characterAnimatorType, AnimatorParameterType type, string animatorParameterName, float floatValue = 0, int intValue = 0, bool boolValue = false)
    {
        if (_currentCharacterAnimatorType != characterAnimatorType)
            return;
 
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

    private void PlayAnimation()
    {
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
