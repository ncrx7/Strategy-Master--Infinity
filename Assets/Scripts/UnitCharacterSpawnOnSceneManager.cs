using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class UnitCharacterSpawnOnSceneManager : MonoBehaviour
{
    [SerializeField] private int _maxPlayerUnitNumberOnScene;
    private int _playerUnitNumberOnScene = 0;
    private bool _isCreating = false;

    private void OnEnable()
    {
        EventSystem.CreateUnitCharacter += HandleCreatingPlayerUnitCharacter;
    }

    private void OnDisable()
    {
        EventSystem.CreateUnitCharacter -= HandleCreatingPlayerUnitCharacter;
    }

    private async void HandleCreatingPlayerUnitCharacter(int index)
    {
        if (_isCreating)
            return;

        _isCreating = true;
        UnitCharacterManager unitCharacterManager;

        switch (index)
        {
            //!!!!!Code repetition. If I can take an enum as a parameter from the buttons, code repetition will be eliminated!!!!!
            case 0:
                unitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(CharacterClassType.MEELE_FIGHTER);
                await UniTask.Delay(unitCharacterManager.CurrentClassData.spawnTime * 1000);
                unitCharacterManager.gameObject.SetActive(true);
                _playerUnitNumberOnScene++;
                break;
            case 1:
                unitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(CharacterClassType.MAGE);
                await UniTask.Delay(unitCharacterManager.CurrentClassData.spawnTime * 1000);
                unitCharacterManager.gameObject.SetActive(true);
                _playerUnitNumberOnScene++;
                break;
            case 2:
                unitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(CharacterClassType.RIFLE);
                await UniTask.Delay(unitCharacterManager.CurrentClassData.spawnTime * 1000);
                unitCharacterManager.gameObject.SetActive(true);
                _playerUnitNumberOnScene++;
                break;
            case 3:
                unitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(CharacterClassType.HEALER);
                await UniTask.Delay(unitCharacterManager.CurrentClassData.spawnTime * 1000);
                unitCharacterManager.gameObject.SetActive(true);
                _playerUnitNumberOnScene++;
                break;
            default:
                Debug.LogWarning("undefined character index number");
                break;
        }
        _isCreating = false;
    }
}
