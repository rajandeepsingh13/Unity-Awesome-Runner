using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    private Text scoreText, healthText, levelText;
    private float score, health, level;

    [HideInInspector]
    public bool canCountScore;

    private BGScroller bgscroller;

    public static GameplayController instance;
    private GameObject pausePanel;

    void Awake () {
        MakeInstance();
        scoreText = GameObject.Find(Tags.Score_Text_Obj).GetComponent<Text>();
        healthText = GameObject.Find(Tags.Health_Text_Obj).GetComponent<Text>();
        levelText = GameObject.Find(Tags.Level_Text_Obj).GetComponent<Text>();

        bgscroller = GameObject.Find(Tags.Backgrund_GameObjTag).GetComponent<BGScroller>();

        pausePanel = GameObject.Find(Tags.Pause_Panel_Obj);
        pausePanel.SetActive(false);
    }
    private void Start()
    {
        if (GameManager.instance.canPlayMusic)
        {
            audioSource.Play();
        }

        
    }
    private void Update()
    {
        IncrementScore(1);
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Tags.Gameplay_Scene)
        {
            //Debug.Log("lol");
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                GameManager.instance.gameStartedFromMainMenu = false;
                score = 0;
                health = 3;
                level = 0;
                
            } else if (GameManager.instance.gameRestartedPlayerDied)
            {
                //Debug.Log("lol");
                GameManager.instance.gameRestartedPlayerDied = false;
                score = GameManager.instance.score;
                health = GameManager.instance.health;

            }
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();

        }
    }

    public void TakeDamage()
    {
        health--;
        if (health >= 0)
        {
            StartCoroutine(PlayerDied(Tags.Gameplay_Scene));
            healthText.text = health.ToString();
        }
        else
        {
            StartCoroutine(PlayerDied(Tags.MainMenu_Scene));
        }
    }

    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
    }

    public void IncrementScore(float scoreValue)
    {
        if (canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator PlayerDied(string sceneName)
    {
        canCountScore = false;
        bgscroller.canScroll = false;

        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.gameRestartedPlayerDied = true;
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        canCountScore = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        canCountScore = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;//always set timescale back to one
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.MainMenu_Scene);
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.Gameplay_Scene);
        GameManager.instance.gameStartedFromMainMenu = true;
    }
}
