using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitClass", menuName = "ScriptableObjects/Unit/UnitClass")]
public class UnitClass : ScriptableObject
{
    public CharacterClassType CharacterClassType;
    public GameObject ModelPrefab;
    public int spawnTime;
}
