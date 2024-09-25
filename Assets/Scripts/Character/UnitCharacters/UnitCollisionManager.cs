using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollisionManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;

    private void OnTriggerEnter(Collider other)
    {
        //UnitCharacterManager otherCharacterManager = other.gameObject.GetComponentInParent<UnitCharacterManager>();

        //Debug.Log($"this owner type: {_unitCharacterManager.characterOwnerType} - hit owner type {other.gameObject.GetComponentInParent<UnitCharacterManager>().characterOwnerType}");
        if (other.TryGetComponent<IUnitEquipmentDamage>(out IUnitEquipmentDamage damage))
        {
            damage.DealDamageToUnitCharacter(_unitCharacterManager); 
            //damage.PlayParticleVfx();
            
            Debug.Log("unit collision manager worked : ");
        }

        if(_unitCharacterManager.GetUnitCharacterStatManager().CheckHealth())
        {
            _unitCharacterManager.ChangeState(new UnitCharacterDeadState());

            if(_unitCharacterManager.characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
                EventSystem.SpRefund?.Invoke(_unitCharacterManager.CurrentClassData.SpPrice);
            //RETURN OBJECT AFTER 2 SECONDS
            //UnitCharacterPoolManager.Instance.ReturnUnitCharacter(_unitCharacterManager);
        }
    }
}
