using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRifleBullet : Bullet, IUnitEquipmentDamage
{
    private UnitCharacterManager _unitCharacterManager;
    public void DealDamageToUnitCharacter(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;

        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() -
        (_damage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF));

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());

        BulletPoolManager.Instance.ReturnBullet(this);
    }

    public void DealDamageToBaseBuilding(ArenaBaseManager arenaBaseManager)
    {
        if((arenaBaseManager.baseType == BaseType.PLAYER_BASE && _unitCharacterManager.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
         || (arenaBaseManager.baseType == BaseType.OPPOSING_BASE && _unitCharacterManager.characterOwnerType == CharacterOwnerType.ENEMY_UNIT))
         {
            return;
         }

        float newHealth = arenaBaseManager.CurrentBaseHealth -
        (_damage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF));

        arenaBaseManager.CurrentBaseHealth = newHealth;
        arenaBaseManager.SetNewHealth(newHealth);

        BulletPoolManager.Instance.ReturnBullet(this);
    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    public override void Update()
    {
        base.Update();
    }

    public void SetUnitCharacterManager(UnitCharacterManager unitCharacterManager)
    {
        _unitCharacterManager = unitCharacterManager;
    }
}
