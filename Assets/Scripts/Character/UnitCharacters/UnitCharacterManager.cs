using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitCharacterManager : CharacterManager
{
    public CharacterOwnerType CharacterOwnerType;
    public CharacterClassType CharacterClassType;
    [SerializeField] private SkillStrategy[] _allClassSkills;
    private IUnitCharacterState _currentState;
    [SerializeField] UnitCharacterLocomotionManager _unitCharacterLocomotionManager;

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
    MAGE
}
