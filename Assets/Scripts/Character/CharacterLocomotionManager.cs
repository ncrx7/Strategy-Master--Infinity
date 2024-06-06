using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    // REF CHARACTER STAT MANAGER
    // REF CHARACTER MANAGER
    #region fields
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private float _movementSpeed = 5; //SHOULD COME FROM STAT MANAGER
    [SerializeField] private float _rotationSpeed = 500;

    //GRAVITY FIELDS
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] protected float gravityForce = -5.5f;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float groundedYVelocity = -20;
    [SerializeField] protected float fallStartVelocity = -5;
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTime = 0;

    #endregion


    #region MB CALLBACK
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        HandleGroundCheck();
        HandleGravity();
    }
    #endregion

    #region LOCOMOTION METHODS
    private void HandleGroundCheck()
    {
        _characterManager.IsGrounded = Physics.CheckSphere(_characterManager.transform.position, groundCheckSphereRadius, groundLayer);
    }

    protected void HandleGravity()
    {
        if (_characterManager.IsGrounded)
        {
            if (yVelocity.y < 0)
            {
                inAirTime = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            /*             if (!_characterManager.isJumping && !fallingVelocityHasBeenSet)
                        {
                            fallingVelocityHasBeenSet = true;
                            yVelocity.y = fallStartVelocity;
                        } */

            inAirTime += Time.deltaTime;
            //EventSystem.UpdateAnimatorParameterAction?.Invoke(_characterManager.networkID, AnimatorValueType.FLOAT, "inAirTimer", inAirTime, false);

            yVelocity.y += gravityForce * Time.deltaTime;
            //_characterManager.isJumping = false;
        }

        //Debug.Log("yvelocity: " + yVelocity);
        _characterManager.characterController.Move(yVelocity * Time.deltaTime);
    }
    #endregion

    #region GETTER METHOD
    public CharacterManager GetCharacterManager()
    {
        return _characterManager;
    }

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }
    #endregion

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_characterManager.transform.position, groundCheckSphereRadius);
    }
}
