using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealAreaSkillImpact : Spell, IUnitEquipmentDamage
{
    private void OnEnable()
    {
        _lifeTimeCounter = 0;
        //_particleSystem.Play();

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1.1f)
                .AppendCallback(() => _collider.enabled = true)
                .AppendCallback(() => _particleSystem.Play())
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

    public void DealDamageToUnitCharacter(UnitCharacterManager senderUnitCharacterManager)
    {
        if (_unitCharacterManager.characterOwnerType != senderUnitCharacterManager.characterOwnerType) // should be fixed
            return;


        float totalHealAmount = _baseDamage + (_unitCharacterManager.GetUnitCharacterStatManager().GetUnitCharacterFixedStatValue(StatType.INT) / 2);
        Debug.Log("total heal amount: " + totalHealAmount);
        float newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetCurrentHealth() + totalHealAmount;

        if (newHealth >= senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth())
            newHealth = senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth();

        senderUnitCharacterManager.GetUnitCharacterStatManager().SetCurrentHealth(newHealth);

        senderUnitCharacterManager.GetUnitCharacterHealthBarController().SetCurrentValueSliderImage
        (newHealth, senderUnitCharacterManager.GetUnitCharacterStatManager().GetMaxHealth());

        //EventSystem.PlaySoundClip?.Invoke(SoundType.MAGE_SPELL_FIRESTORM);
    }

    public void DealDamageToBaseBuilding(ArenaBaseManager arenaBaseManager)
    {
        throw new System.NotImplementedException();
    }

    public void PlayParticleVfx(GameObject box)
    {
        throw new System.NotImplementedException();
    }
}
