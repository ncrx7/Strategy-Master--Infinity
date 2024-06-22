using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public ManController mancontroller;
    public float maxStretchDistance = 3f;
    public float stretchSpeed = 5f;

    private bool isStretching;
    private Vector3 initialPosition;
    private float dragStartY;
    public GameObject childObject;
    public float launchForceMultiplier = 2f;

    float stretchDistance;

    public GameObject go;

    private void Start()
    {
        mancontroller = FindObjectOfType<ManController>();
        initialPosition = transform.position;
        childObject = transform.GetChild(1).gameObject;
        DrawTrajectory.Instance._lineRenderer = childObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(GameManager.Instance.SlingisLoaded){
            if (Input.GetMouseButtonDown(0))
            {
                DrawTrajectory.Instance._lineRenderer.enabled = true;
                StartStretching();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isStretching)
                {
                    Release();
                }
            }

        if (isStretching)
        {
            Stretch();
            float launchForce = stretchDistance * launchForceMultiplier;
        
            DrawTrajectory.Instance.ShowTrajectoryLine(childObject.transform.position, Vector3.forward * launchForce);
        }
        }  
    }

    private void StartStretching()
    {
        isStretching = true;
        dragStartY = GetMouseWorldPosition().y;
    }

    private void Stretch()
    {
        Vector3 dragEndPosition = GetMouseWorldPosition();
        stretchDistance = Mathf.Clamp(dragStartY - dragEndPosition.y, 0f, maxStretchDistance);
        Vector3 targetPosition = initialPosition - Vector3.forward * stretchDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, stretchSpeed * Time.deltaTime);
    }

    private void Release()
    {
        GameManager.Instance.objectkinematictrue();
        GameManager.Instance.UpdateShootLeft();
        //GameManager.Instance.shootnumber--;
        go.GetComponent<ManController>().MovingMan();
        //mancontroller.MovingMan();
        //DrawTrajectory.Instance.SetLineRendererObject(childObject);
        StartCoroutine(ReturnToInitialPosition());
        DrawTrajectory.Instance._lineRenderer.enabled = false;
        Rigidbody childRigidbody = childObject.GetComponent<Rigidbody>();
        GameManager.Instance.SlingisLoaded = false;
        float launchForce = stretchDistance * launchForceMultiplier; // the more we stretch the further
        childRigidbody.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        childRigidbody.useGravity = true;
        childObject.transform.parent = null;
        //UpdateChildObject();
        childObject = go;
    }

    private IEnumerator ReturnToInitialPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 0.2f)
        {
            float t = elapsedTime / 0.2f;
            transform.position = Vector3.Lerp(startPosition, initialPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
        isStretching = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void UpdateChildObject()
    {
        childObject = transform.GetChild(1).gameObject;
        DrawTrajectory.Instance._lineRenderer = childObject.GetComponent<LineRenderer>();
        Debug.Log(childObject);
    }
}
