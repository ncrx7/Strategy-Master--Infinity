using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaColliderManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
        {
            playerManager.isFallen = true;
            EventSystem.OnPlayerDefeat?.Invoke();
            Debug.Log("CHARACTER HAS FALLEN");
        }
    }
}
