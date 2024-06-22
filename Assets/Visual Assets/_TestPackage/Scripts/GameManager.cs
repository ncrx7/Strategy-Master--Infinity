using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Stand, scoreCounter, UIBackgroundGameOver;
    [SerializeField] private float rotationSpeed = 10f;

    private static GameManager _instance;
    public bool SlingisLoaded = true;
    public int shootnumber = 2;
    public int sinkcounter = 0;
    public int finishthelevertimer = 4;
    private float ratio;

    [SerializeField] private Image scoreImage;
    [SerializeField] private TMP_Text rateofratiotext, currentLevel, nextLevel, shootwarningtext, levelHolderGameOver, rateofratioGameOver;
    public int level = 1;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {   
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        level = DontDestroyOnLoadScript.Instance.levelconstant;
        currentLevel.text = level.ToString();
        levelHolderGameOver.text = "Level " + level.ToString() + " Completed";
        nextLevel.text = (++level).ToString();
    }

    private void Update()
    {
       Stand.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        if(shootnumber == 0)
        {
            shootwarningtext.text = "No More Shots";
            StartCoroutine(FinishTheLevel());
        }
    }

    public void UpdateScore()
    {
        ratio = sinkcounter / 36f; 
        scoreImage.fillAmount = ratio;
        rateofratiotext.text = "%" + ((int)(ratio * 100)).ToString();
    }

    public void UpdateShootLeft()
    {
        shootnumber--;
        shootwarningtext.text = shootnumber.ToString() + " Shot Left";
    }

    public void objectkinematictrue() //I couldnt balance on the stand. That is why I set their kinematics true so they dont tip over until the shot is fired
    {
        Transform childTransform = Stand.transform.GetChild(0);

        foreach (Transform child in childTransform)
        {
            Rigidbody childRigidbody = child.GetComponent<Rigidbody>();
            childRigidbody.isKinematic = false;
        }
    }

    private IEnumerator FinishTheLevel() //A certain second after the last man is thrown, calculations are made and the game is over.
    {
        yield return new WaitForSeconds(finishthelevertimer);
        rateofratioGameOver.text = "%" + ((int)(ratio * 100)).ToString();
        scoreCounter.SetActive(false);
        UIBackgroundGameOver.SetActive(true);
        Debug.Log("oyun bitti");

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

        public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DontDestroyOnLoadScript.Instance.levelconstant++;
    }

  
}
