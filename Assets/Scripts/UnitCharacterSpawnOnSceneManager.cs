using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

public class UnitCharacterSpawnOnSceneManager : MonoBehaviour
{
    [SerializeField] private int _enemyUnitCharacterSpawnInterval;
    [SerializeField] private int _playerUnitNumberOnScene;
    private bool _isCreating = false;
    [SerializeField] private CharacterClassType _previousSpawnedUnit; //IT IS SerializeField TO SEE IN EDITOR

    [Header("Unit Creator Settings")]
    [SerializeField] private float maxInterval = 5f; 
    [SerializeField] private float minInterval = 0.5f; 
    [SerializeField] private int maxPlayerUnits = 10; 
    [SerializeField] private int minPlayerUnits = 1; 

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

    public void ReduceNumberOfPlayerUnitOnScene() => --_playerUnitNumberOnScene;

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

            SetEnemySpawnInterval(_playerUnitNumberOnScene);
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

    private void SetEnemySpawnInterval(int playerUnitCount)
    {
        float normalizedCount = Mathf.Clamp01((float)(playerUnitCount - minPlayerUnits) / (maxPlayerUnits - minPlayerUnits));
        _enemyUnitCharacterSpawnInterval = (int)Mathf.Lerp(maxInterval, minInterval, Mathf.Pow(normalizedCount, 2));
    }
}

