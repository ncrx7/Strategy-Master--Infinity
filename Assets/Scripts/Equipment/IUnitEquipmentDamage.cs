using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitEquipmentDamage
{
    void DealDamageToUnitCharacter(UnitCharacterManager unitCharacterManager);
    void DealDamageToBaseBuilding(ArenaBaseManager arenaBaseManager);
    void PlayParticleVfx(GameObject box);
}
