using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitCharacterManager : CharacterManager
{
    [Header("DATA SECTION")]
    [SerializeField] private UnitClass[] _allClassData;
    [SerializeField] private SkillStrategy[] _allClassSkills;

    [Header("TYPE SECTION")]
    public CharacterOwnerType characterOwnerType;
    public CharacterClassType characterClassType;
    [SerializeField] private UnitClass CurrentClassData;
    
    [Header("STATE SECTION")]
    private IUnitCharacterState _currentState;

    [Header("REFERENCE SECTION")]
    [SerializeField] UnitCharacterLocomotionManager _unitCharacterLocomotionManager;
    public Transform modelTransform;

    public override void Start()
    {
        base.Start();

        ChangeState(new UnitCharacterWalkingState());
    }

    public override void Update()
    {
        base.Update();

        _currentState.UpdateState(this);
    }

    public SkillStrategy[] GetAllCharacterSkills()
    {
        return _allClassSkills;
    }

    public void ChangeState(IUnitCharacterState newState)
    {
        if(_currentState != null && _currentState.GetType() == newState.GetType()) 
        {
            return;
        }

        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public UnitCharacterLocomotionManager GetUnitCharacterLocomotionManager()
    {
        return _unitCharacterLocomotionManager;
    }

    public UnitClass[] GetAllClassData()
    {
        return _allClassData;
    }

    public void SetCurrentClassData(UnitClass unitClass)
    {
        CurrentClassData = unitClass;
    }

    public void SetCurrentOwnerType(CharacterOwnerType characterOwnerType)
    {
        this.characterOwnerType = characterOwnerType;
    }

    public void SetCurrentClassType(CharacterClassType characterClassType)
    {
        this.characterClassType = characterClassType;
    }
}

public enum CharacterOwnerType
{
    PLAYER_UNIT,
    ENEMY_UNIT
}

public enum CharacterClassType
{
    MEELE_FIGHTER,
    RIFLE,
    MAGE,
    HEALER
}
