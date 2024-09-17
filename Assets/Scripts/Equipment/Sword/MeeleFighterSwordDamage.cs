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

    public void DealDamageToUnitCharacter(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;

        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() -
        (_swordBaseDamage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF));

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());

    }

    public void DealDamageToBaseBuilding(ArenaBaseManager arenaBaseManager)
    {
        if ((arenaBaseManager.baseType == BaseType.PLAYER_BASE && _unitCharacterManager.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
         || (arenaBaseManager.baseType == BaseType.OPPOSING_BASE && _unitCharacterManager.characterOwnerType == CharacterOwnerType.ENEMY_UNIT))
        {
            return;
        }

        float newHealth = arenaBaseManager.CurrentBaseHealth -
        (_swordBaseDamage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.INT));

        arenaBaseManager.CurrentBaseHealth = newHealth;
        arenaBaseManager.SetNewHealth(newHealth);
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
