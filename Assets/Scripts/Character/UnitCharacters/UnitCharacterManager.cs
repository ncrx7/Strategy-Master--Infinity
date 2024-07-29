using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitCharacterManager : CharacterManager
{
    public  CharacterOwnerType CharacterOwnerType;
    public CharacterClassType CharacterClassType;
    [SerializeField] private SkillStrategy[] _allClassSkills;
    private IEnemyState _currentState;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        //_currentState.UpdateState(this);
    }

    public SkillStrategy[] GetAllCharacterSkills()
    {
        return _allClassSkills;
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
