using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollisionManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    private float health = 10;
    private void OnTriggerEnter(Collider other)
    {
        if(_unitCharacterManager.characterOwnerType == other.gameObject.GetComponentInParent<UnitCharacterManager>().characterOwnerType)
        {
            return;
        }

        //Debug.Log($"this owner type: {_unitCharacterManager.characterOwnerType} - hit owner type {other.gameObject.GetComponentInParent<UnitCharacterManager>().characterOwnerType}");
        if(other.TryGetComponent<IDamage>(out IDamage damage))
        {
            ref float currentHealthRef = ref _unitCharacterManager.GetUnitCharacterStatManager().currentHealth;
            damage.DealDamage(ref currentHealthRef, 5); //
            _unitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage(currentHealthRef, _unitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());
            Debug.Log("unit collision manager worked : ");
            //damage.PlayParticleVfx();
        }
    }
}
