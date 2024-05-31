using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public CharacterController characterController;

    #region FIELDS

    #region FLAGS
    public bool IsGrounded {get; set;}
    #endregion

    #endregion 

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }
}
