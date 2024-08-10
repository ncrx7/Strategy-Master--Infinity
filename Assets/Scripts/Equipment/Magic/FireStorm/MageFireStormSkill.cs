using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MageFireStormSkill : Spell, IUnitEquipmentDamage
{
    //float yAxisStart = 4.5f;
    //float yAxisTarget = 0;
    [SerializeField] private UnitCharacterManager _unitCharacterManager;

    private void OnEnable()
    {
        _lifeTimeCounter = 0;
        _particleSystem.Play();

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1f)
                .AppendCallback(() => _collider.enabled = true)
                .AppendInterval(1f)
                .AppendCallback(() => _collider.enabled = false);
                
        sequence.Play();

        //Vector3 startPos = new Vector3(transform.position.x, yAxisStart, transform.position.z);
        //transform.position = startPos;
        //transform.DOMoveY(0f, 4f);
    }

    public override void Update()
    {
        base.Update();
    }

    public void DealDamage(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType == senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;
        
        float totalDamageAmount = _baseDamage + _unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.INT);
        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() - totalDamageAmount;
        

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());

        EventSystem.PlaySoundClip?.Invoke(SoundType.MAGE_SPELL_FIRESTORM);
    }

    public void PlayParticleVfx(GameObject box)
    {

    }

    public void SetUnitCharacterManager(UnitCharacterManager unitCharacterManager)
    {
        _unitCharacterManager = unitCharacterManager;
    }
}
