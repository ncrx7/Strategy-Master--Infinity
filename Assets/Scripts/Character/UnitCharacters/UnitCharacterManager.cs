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
    public CharacterOwnerType CharacterOwnerType;
    public CharacterClassType CharacterClassType;
    [SerializeField] private UnitClass CurrentClassData;
    
    [Header("STATE SECTION")]
    private IUnitCharacterState _currentState;

    [Header("REFERENCE SECTION")]
    [SerializeField] UnitCharacterLocomotionManager _unitCharacterLocomotionManager;
    [SerializeField] Transform _modelTransform;

    public override void Start()
    {
        base.Start();

        ChangeState(new IWalkingState());
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
