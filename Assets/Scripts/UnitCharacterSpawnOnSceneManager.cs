using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class UnitCharacterSpawnOnSceneManager : MonoBehaviour
{
    [SerializeField] private int _maxPlayerUnitNumberOnScene;
    [SerializeField] private int _enemyUnitCharacterSpawnInterval;
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

    private void Start()
    {
        // CreatingEnemyUnitCharacter();
    }

    private async void HandleCreatingPlayerUnitCharacter(int index, CharacterClassType characterClassType)
    {
        if (_isCreating)
            return;

        _isCreating = true;
        UnitCharacterManager unitCharacterManager;
        float spawnTime = 0;

        EventSystem.ChangeBarVisibility(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, true);
        EventSystem.SetSliderBarValue?.Invoke(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, 0, 1, BaseType.PLAYER_BASE);

        unitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(characterClassType, CharacterOwnerType.PLAYER_UNIT);
        if (unitCharacterManager == null)
        {
            EventSystem.ChangeBarVisibility(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, false);
            _isCreating = false;
            return;
        }
        spawnTime = unitCharacterManager.CurrentClassData.spawnTime;

        float elapsedTime = 0;
        while (elapsedTime < spawnTime)
        {
            elapsedTime += Time.deltaTime;
            EventSystem.SetSliderBarValue?.Invoke(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, Mathf.Clamp01(elapsedTime / spawnTime), 1, BaseType.PLAYER_BASE);
            await UniTask.Yield(); 
        }

        unitCharacterManager.gameObject.SetActive(true);
        _playerUnitNumberOnScene++;

        EventSystem.ChangeBarVisibility(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, false);

        _isCreating = false;
    }

    private async void CreatingEnemyUnitCharacter()
    {
        await UniTask.Delay(1000);

        while (true)
        {

            UnitCharacterManager enemyUnitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(CharacterClassType.MEELE_FIGHTER, CharacterOwnerType.ENEMY_UNIT);

            if (enemyUnitCharacterManager == null)
                continue;

            await UniTask.Delay(enemyUnitCharacterManager.CurrentClassData.spawnTime * 1000);
            enemyUnitCharacterManager.gameObject.SetActive(true);
            Debug.Log("waited character spawn time");


            await UniTask.Delay(_enemyUnitCharacterSpawnInterval * 1000);
            Debug.Log("waited interval : " + _enemyUnitCharacterSpawnInterval);
        }
    }
}
