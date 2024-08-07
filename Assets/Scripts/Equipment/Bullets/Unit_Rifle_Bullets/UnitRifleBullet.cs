using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRifleBullet : Bullet, IUnitEquipmentDamage
{
    private UnitCharacterManager _unitCharacterManager;
    public void DealDamage(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;

        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() -
        (_damage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF));

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());
        //BulletPoolManager.Instance.ReturnBullet(this);
    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    public override void Update()
    {
        base.Update();
    }
}
