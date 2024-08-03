using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeeleAttackStrategy", menuName = "ScriptableObjects/Skills/MeeleAttackStrategy")]
public class MeeleAttackStrategy : SkillStrategy
{
    public override void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        //MEELE ATTACK ANIMATION
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Meele_Figter_Attack_0");
        Debug.Log("MELE ATTACK");
    }
}
