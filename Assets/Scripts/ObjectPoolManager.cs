// Assets/Scripts/ObjectPoolManager.cs
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager<T> where T : Component
{
    private Queue<T> objectPool = new Queue<T>();
    private T prefab;
    private Transform parentTransform;

    public ObjectPoolManager(T prefab, int initialSize, Transform parentTransform = null, PoolCharacterType poolCharacterType = PoolCharacterType.DEFAULT)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;

        CharacterOwnerType characterOwnerType = CharacterOwnerType.PLAYER_UNIT;

        for (int i = 0; i < initialSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab, parentTransform);

            switch (poolCharacterType)
            {
                case PoolCharacterType.DEFAULT:
                    newObject.gameObject.SetActive(false);
                    objectPool.Enqueue(newObject);
                    break;
                case PoolCharacterType.UNIT:
                    UnitCharacterManager unit = newObject as UnitCharacterManager;

                    UnitClass unitClass = unit.GetAllClassData()[i % unit.GetAllClassData().Length];
                    unit.SetCurrentClassData(unitClass);
                    unit.SetCurrentClassType(unitClass.CharacterClassType);

                    GameObject unitModelObject = GameObject.Instantiate(unitClass.ModelPrefab, unit.modelTransform);

                    if(i % 4 == 0)
                    {
                        if(characterOwnerType == CharacterOwnerType.PLAYER_UNIT)
                        {
                            characterOwnerType = CharacterOwnerType.ENEMY_UNIT;
                        }
                        else if(characterOwnerType == CharacterOwnerType.ENEMY_UNIT)
                        {
                            characterOwnerType = CharacterOwnerType.PLAYER_UNIT;
                        }
                    }

                    unit.SetCurrentOwnerType(characterOwnerType);
                    unit.GetUnitCharacterStatManager().InitUnitCharacterStat();
            
                    newObject.gameObject.SetActive(false);
                    objectPool.Enqueue(newObject);
                    break;
                default:
                    break;
            }
        }

        //EventSystem.OnUnitCharacterTagged?.Invoke();
    }

    public T GetObject()
    {
        if (objectPool.Count > 0)
        {
            T pooledObject = objectPool.Dequeue();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }
        else
        {
            T newObject = GameObject.Instantiate(prefab, parentTransform);
            return newObject;
        }
    }

    public void ReturnObject(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectPool.Enqueue(objectToReturn);
    }
}

public enum PoolCharacterType
{
    DEFAULT,
    UNIT
}
