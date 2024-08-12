using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

[CreateAssetMenu(fileName = "HealStrategy", menuName = "ScriptableObjects/Skills/HealStrategy")]
public class HealStrategy : SkillStrategy
{
    [SerializeField] private GameObject _healingFx;

    public override async void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Unit_Healer_Heal");

        await Task.Run(() =>
        {
            Thread.Sleep(1140);
        });

        Spell spell = SpellVfxPoolManager.Instance.GetSpell(SpellType.SPELL_HEALER_HEALAREA);
        spell.SetUnitCharacterManager(unitCharacterManager);
        spell.transform.position = origin.position;
        //Instantiate(_healingFx, unitCharacterManager.transform.position, Quaternion.identity);
    }
}
