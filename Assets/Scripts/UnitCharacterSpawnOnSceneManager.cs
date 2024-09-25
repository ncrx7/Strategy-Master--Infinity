using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System;

public class UnitCharacterSpawnOnSceneManager : MonoBehaviour
{
    [SerializeField] private int _maxPlayerUnitNumberOnScene;
    [SerializeField] private int _enemyUnitCharacterSpawnInterval;
    private int _playerUnitNumberOnScene = 0;
    private bool _isCreating = false;
    [SerializeField] private CharacterClassType _previousSpawnedUnit; //IT IS SerializeField TO SEE IN EDITOR

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
        CreatingEnemyUnitCharacterAI();
    }

    private async void HandleCreatingPlayerUnitCharacter(int index, CharacterClassType characterClassType, Action<int> callback)
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

        int unitCharacterSpPrice = unitCharacterManager.CurrentClassData.SpPrice;

        if (!EventSystem.SpCheck.Invoke(unitCharacterSpPrice))
        {
            EventSystem.ChangeBarVisibility(BarType.UNITCHARACTER_SPAWN_PROGRESS_BAR, false);
            _isCreating = false;
            UnitCharacterPoolManager.Instance.ReturnUnitCharacter(unitCharacterManager);
            unitCharacterManager = null;
            return;
        }
        callback?.Invoke(unitCharacterSpPrice);

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

    private async void CreatingEnemyUnitCharacterAI()
    {
        await UniTask.Delay(1000);

        while (true)
        {
            CharacterClassType chosenType = ChooseUnitType();
            UnitCharacterManager enemyUnitCharacterManager = UnitCharacterPoolManager.Instance.GetUnitCharacter(chosenType, CharacterOwnerType.ENEMY_UNIT);

            if (enemyUnitCharacterManager == null)
            {
                await UniTask.Delay(_enemyUnitCharacterSpawnInterval * 1000);
                continue;
            }

            _previousSpawnedUnit = chosenType;

            await UniTask.Delay(enemyUnitCharacterManager.CurrentClassData.spawnTime * 1000);

            enemyUnitCharacterManager.gameObject.SetActive(true);

            await UniTask.Delay(_enemyUnitCharacterSpawnInterval * 1000);
        }
    }

    private CharacterClassType ChooseUnitType()
    {
        if (_previousSpawnedUnit == CharacterClassType.MEELE_FIGHTER)
        {
            return (UnityEngine.Random.value > 0.5f) ? CharacterClassType.MAGE : CharacterClassType.RIFLE;
        }
        else
        {
            int randomValue = UnityEngine.Random.Range(0, 100);
            if (randomValue < 40) return CharacterClassType.MEELE_FIGHTER;
            if (randomValue < 65) return CharacterClassType.MAGE;
            if (randomValue < 85) return CharacterClassType.RIFLE;
            return CharacterClassType.HEALER;
        }
    }
}
