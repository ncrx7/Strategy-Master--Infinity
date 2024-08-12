using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageFireStormStrategy", menuName = "ScriptableObjects/Skills/MageFireStormStrategy")]
public class MageFireStormStrategy : SkillStrategy
{
    public GameObject fireStormFx;
    public override void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Unit_Mage_Attack");

        Transform targetAreaPoint;

        GameObject friendForwarddUnit = unitCharacterManager.GetUnitDistanceManager().FriendForwardUnitCharacter;

        if (friendForwarddUnit != null)
        {
            UnitDistanceManager forwardUnitCharacterDistanceManager = friendForwarddUnit.GetComponent<UnitCharacterManager>(). //ForwardUnitCharacter null check // can be extension method
            GetUnitDistanceManager();
            targetAreaPoint = forwardUnitCharacterDistanceManager.OpposingForwardUnitCharacter.transform;
            //Instantiate(fireStormFx, targetAreaPoint.position, Quaternion.identity);
            Debug.Log("mage ranged target are point: " + targetAreaPoint.position);
        }
        else
        {
            targetAreaPoint = unitCharacterManager.GetUnitDistanceManager().OpposingForwardUnitCharacter.transform;
            //Instantiate(fireStormFx, targetAreaPoint);
            Debug.Log("mage not ranged target are point: " + targetAreaPoint.position);
        }

        //GameObject fireStromFxSpellOnScene = Instantiate(fireStormFx, targetAreaPoint.position, Quaternion.identity);

        Spell spell = SpellVfxPoolManager.Instance.GetSpell(SpellType.SPELL_MAGE_FIRESTORM);

        //MageFireStormSkill fireStormSpell = spell as MageFireStormSkill;
        spell.SetUnitCharacterManager(unitCharacterManager);
        spell.transform.position = targetAreaPoint.position;


    }
}
