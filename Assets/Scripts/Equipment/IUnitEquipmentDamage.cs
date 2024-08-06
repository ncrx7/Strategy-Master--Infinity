using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitEquipmentDamage
{
    void DealDamage(UnitCharacterManager unitCharacterManager);
    void PlayParticleVfx(GameObject box);
}
