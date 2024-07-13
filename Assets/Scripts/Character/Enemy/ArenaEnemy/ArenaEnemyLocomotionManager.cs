using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemyLocomotionManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ArenaEnemyManager _arenaEnemyManager;
    

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }


}
