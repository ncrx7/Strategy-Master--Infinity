using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireSkillStrategy", menuName = "ScriptableObjects/Skills/FireSkillStrategy")]
public class FireSkillStrategy : SkillStrategy
{
    public override void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("FIREEE");
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Unit_Rifle_Fire");
    }
}
