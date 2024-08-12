using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRangeEuipmentManager : MonoBehaviour
{
    [SerializeField] private NonRangedEquipmentDamage _nonRangedEquipmentDamage;
    /*     [SerializeField] private Animator _modelAnimator;
        private void Start()
        {
            //EventSystem.OnUnitModelInstantiated?.Invoke(_modelAnimator);
        } */
    public void EnableWeaponCollider()
    {
        _nonRangedEquipmentDamage.EnableCollider();
    }
    public void DisableWeaponCollider()
    {
        //_swordCollider.enabled = false;
        _nonRangedEquipmentDamage.DisableCollider();
    }
}
