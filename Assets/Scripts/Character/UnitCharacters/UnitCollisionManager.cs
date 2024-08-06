using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollisionManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    private float health = 10;
    private void OnTriggerEnter(Collider other)
    {
        //UnitCharacterManager otherCharacterManager = other.gameObject.GetComponentInParent<UnitCharacterManager>();

        //Debug.Log($"this owner type: {_unitCharacterManager.characterOwnerType} - hit owner type {other.gameObject.GetComponentInParent<UnitCharacterManager>().characterOwnerType}");
        if (other.TryGetComponent<IDamage>(out IDamage damage))
        {
            UnitCharacterManager senderUnitCharacterManager = other.gameObject.GetComponentInParent<UnitCharacterManager>();

            if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
                return;
            

            ref float currentHealthRef = ref _unitCharacterManager.GetUnitCharacterStatManager().currentHealth;
            damage.DealDamage(ref currentHealthRef, (int)senderUnitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.PF)); // TODO: use switch for other class
            _unitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage(currentHealthRef, _unitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());
            //Debug.Log("unit collision manager worked : ");
            //damage.PlayParticleVfx();
        }
    }
}
