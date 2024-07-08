using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{

    public void LoadTheLevelScene()
    {
        SceneManager.LoadScene(1);
    }
}
