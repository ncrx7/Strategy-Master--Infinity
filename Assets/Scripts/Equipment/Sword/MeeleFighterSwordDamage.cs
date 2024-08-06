using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleFighterSwordDamage : MonoBehaviour, IUnitEquipmentDamage
{
    [SerializeField] private float _swordBaseDamage;
    [SerializeField] private Collider _swordCollider;
    private UnitCharacterManager _unitCharacterManager;
    /*     public void DealDamage(ref float healthVariable, int playerPF)
        {
            Debug.Log("dealed damage from the sword");
            healthVariable -= _swordBaseDamage + playerPF;
        } */

    private void Start()
    {
        _unitCharacterManager = GetComponentInParent<UnitCharacterManager>();
    }

    public void DealDamage(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;

        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() -
        (_swordBaseDamage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF));

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());

    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    public void EnableCollider()
    {
        _swordCollider.enabled = true;
    }
    public void DisableCollider()
    {
        _swordCollider.enabled = false;
    }
}
