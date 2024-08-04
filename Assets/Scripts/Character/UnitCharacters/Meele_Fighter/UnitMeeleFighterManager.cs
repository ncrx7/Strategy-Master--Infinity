using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMeeleFighterManager : MonoBehaviour
{
    [SerializeField] private MeeleFighterSwordDamage _meeleFighterSwordDamage;
    /*     [SerializeField] private Animator _modelAnimator;
        private void Start()
        {
            //EventSystem.OnUnitModelInstantiated?.Invoke(_modelAnimator);
        } */
    public void EnableWeaponCollider()
    {
        _meeleFighterSwordDamage.EnableCollider();
    }
    public void DisableWeaponCollider()
    {
        //_swordCollider.enabled = false;
        _meeleFighterSwordDamage.DisableCollider();
    }
}
