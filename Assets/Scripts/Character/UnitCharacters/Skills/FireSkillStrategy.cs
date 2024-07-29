using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireSkillStrategy", menuName = "ScriptableObjects/Skills/FireSkillStrategy")]
public class FireSkillStrategy : SkillStrategy
{
    public override void CastSkill(Transform origin)
    {
        Debug.Log("FIREEE");
    }
}
