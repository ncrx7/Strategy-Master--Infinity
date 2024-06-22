using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject objectthrown;
    [SerializeField] private Transform objectThrownSpawnTransform;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int shootNumber = 2;


    void Start()
    {
        for (int i = 0; i < shootNumber; i++)
        {
            GameObject spawnedObject = Instantiate(objectthrown, transform.position, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
        }
    }


    void Update()
    {
        
    }
}
