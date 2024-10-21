using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public static SceneControlManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadTheLevelScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        //SceneManager.LoadSceneAsync(sceneIndex);
    }
}
