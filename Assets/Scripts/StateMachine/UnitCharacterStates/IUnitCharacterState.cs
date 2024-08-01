using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitCharacterState
{
    void EnterState(UnitCharacterManager unitCharacterManager); //CharacterManager characterManager
    void UpdateState(UnitCharacterManager unitCharacterManager);
    void ExitState(UnitCharacterManager unitCharacterManager);
}
