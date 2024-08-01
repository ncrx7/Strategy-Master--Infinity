using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCharacterSkillManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    [SerializeField] private List<SkillStrategy> _classSkills = new List<SkillStrategy>();

    private void Start()
    {
        SetSkillStrageies(_unitCharacterManager);
    }


    private void SetSkillStrageies(UnitCharacterManager unitCharacterManager)
    {
        foreach (var item in unitCharacterManager.GetAllCharacterSkills())
        {
            if(item.characterClassType == unitCharacterManager.characterClassType)
            {
                _classSkills.Add(item);
            }
        }
    }

    private void HandleSkill(int index)
    {
        _classSkills[index].CastSkill(transform);
        Debug.Log("handle skill");
    }
}
