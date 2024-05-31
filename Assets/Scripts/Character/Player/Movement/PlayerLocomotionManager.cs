using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    // SHOULD COME FROM PLAYER STATS
    #region fields

    #endregion


    #region mb callbacks
    public override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        base.Update();

        HandleMovement();
    }
    #endregion

    #region locomotion methods
    private void HandleMovement()
    {
        if (PlayerInputManager.Instance.DragStarted)
        {
            SetRotation();
            MoveForward();
        }
    }

    private Quaternion CalculateRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return targetRotation;
    }
    private Vector3 CalculateDirection()
    {
        Vector3 dir = (PlayerInputManager.Instance.TouchDown - PlayerInputManager.Instance.TouchUp).normalized;
        dir.z = dir.y;
        dir.y = 0;
        //Debug.Log(dir);
        return dir;
    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), GetRotationSpeed() * Time.deltaTime);
    }

    private void MoveForward()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * _movementSpeed);
        GetCharacterManager().characterController.Move(transform.forward * GetMovementSpeed() * Time.deltaTime);
    }
    #endregion
}
