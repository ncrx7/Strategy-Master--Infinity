using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemyLocomotionManager : MonoBehaviour
{
    public float Speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ArenaEnemyManager _arenaEnemyManager;
    Coroutine SpeedAdjustingCoroutine;


/*     private void FixedUpdate()
    {
        //MoveForward();
//        Debug.Log("velocity: " + _rigidbody.velocity);
    } */

    public void MoveForward()
    {
        _rigidbody.velocity = -Vector3.forward * Speed;
//        Debug.Log("enemy velocity : " + _rigidbody.velocity);
    }

    public void StartAdjustSpeedCoroutine()
    {
        SpeedAdjustingCoroutine = StartCoroutine(AdjustSpeedNumerator());
    }

    public void StopAdjustSpeedCoroutine()
    {
        StopCoroutine(SpeedAdjustingCoroutine);
    }

    IEnumerator AdjustSpeedNumerator()
    {
        Speed = 5f;
        yield return new WaitForSeconds(2);
        Speed = 3f;
    }


}
