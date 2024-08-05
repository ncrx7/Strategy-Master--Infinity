using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCharacterSkillManager : MonoBehaviour
{
    [SerializeField] private UnitCharacterManager _unitCharacterManager;
    [SerializeField] private List<SkillStrategy> _classSkills = new List<SkillStrategy>();
    public Coroutine attackCoroutine;

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

    public void HandleStartAttacking(int index)
    {
        //_classSkills[index].CastSkill(transform, _unitCharacterManager);
        attackCoroutine = StartCoroutine(HandleSkillDelayed(index));
        //Debug.Log("handle skill");
    }

    public void HandleStopAttacking()
    {
        StopCoroutine(attackCoroutine);
    }

    private IEnumerator HandleSkillDelayed(int index)
    {
        while (true)
        {
            _classSkills[index].CastSkill(transform, _unitCharacterManager);
            yield return new WaitForSeconds(_classSkills[index].skillCooldown);
        }
    }
}
