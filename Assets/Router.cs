using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4);
        SceneControlManager.Instance.LoadTheLevelScene(1);
    }
}
