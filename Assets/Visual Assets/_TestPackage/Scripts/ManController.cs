using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour
{
    [SerializeField] private Transform FirstTarget;
    [SerializeField] private Transform SecondTarget;
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject Sling; 
    private SlingShot slingshot;
    public bool isinSling; // Man is in Sling or not ?

    void Start()
    {
        slingshot = Sling.GetComponent<SlingShot>();
    }

    void Update()
    {

    }

    public void MovingMan()
    {
        if(!isinSling)
        {
            Debug.Log("ı worked");
            transform.SetParent(Sling.transform);
            StartCoroutine(FirstMoveToTarget());
            StartCoroutine(SecondtMoveToTarget());
        }
    }

    private IEnumerator FirstMoveToTarget() //walking to sling
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        transform.Rotate(0f, -90f, 0f);
        while (elapsedTime < animationTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / animationTime ;
            transform.position = Vector3.Lerp(startPosition, FirstTarget.position, t);

            yield return null;
        }
        transform.Rotate(0f, 90f, 0f);
        transform.position = FirstTarget.position;
    }

    private IEnumerator SecondtMoveToTarget() //climbing up to sling 
    {
        yield return new WaitForSeconds(animationTime);
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < animationTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / animationTime ;
            transform.position = Vector3.Lerp(startPosition, SecondTarget.position, t);

            yield return null;
        }
        transform.position = SecondTarget.position;
        
        
        GameManager.Instance.SlingisLoaded = true;
        isinSling = true;
    }
}
