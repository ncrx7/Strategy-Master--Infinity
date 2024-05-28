using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    #region fields
    public static PlayerInputManager Instance { get; private set; }
    private Touch _touch;
    public Vector3 TouchDown { get; private set; }
    public Vector3 TouchUp { get; private set; }

    public bool DragStarted { get; private set; }
    public bool IsMoving { get; private set; }
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                DragStarted = true;
                IsMoving = true;
                TouchUp = _touch.position;
                TouchDown = _touch.position;
            }
        }
        if (DragStarted)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                TouchDown = _touch.position;
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                TouchDown = _touch.position;
                IsMoving = false;
                DragStarted = false;
            }
        }
    }


}
