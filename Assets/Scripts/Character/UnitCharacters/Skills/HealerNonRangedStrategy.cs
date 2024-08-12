using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealerNonRangedStrategy", menuName = "ScriptableObjects/Skills/HealerNonRangedStrategy")]
public class HealerNonRangedStrategy : SkillStrategy
{
    public override void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Unit_Healer_Attack");
    }
}
