using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Level = 0;

    public static int lives = 3;
    public List<Image> livesImage = new List<Image>(3);

    public Text scoreText;
    public Text finalScoreText, victoryScoreText;
    static public int score;

    public GameObject pauseCanvas, gameOverCanvas, victoryCanvas;
    public Button resumeButton, menuButton, menuButton2, menuButton3;
    
    public enum GameState { Init, Game, Dead, Paused, GameOver, Victory }

    public static GameState gameState;

    public GameObject knightro;
    private GameObject blinky;
    private GameObject pinky;
    private GameObject inky;
    private GameObject clyde;


    public static bool scared;
    public float scareLength;
    private float _timeToCalm;

    public float ghostSpeed;

    private static GameManager _instance;

    public GameObject GM;

   
   
    public AudioSource musicSource;
    public AudioSource musicSource3;
    public AudioSource musicSource4;
    public AudioClip musicClipThree;
    public AudioClip musicClipTwo;
    public AudioClip gamePlay;
    

    


    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }

        Assign();
    }

    void Start()
    {
        gameState = GameState.Init;
        ResetScene();
        musicSource.clip = gamePlay;
        musicSource.Play();
    }

    void OnLevelWasLoaded(int level)
    {
        if (Level == 0) lives = 3;

        Debug.Log("Level " + Level + " Loaded!");
        Assign();
        ResetVars();


        clyde.GetComponent<GhostAI>().speed = clyde.GetComponent<GhostAI>().speed;
        blinky.GetComponent<GhostAI>().speed = blinky.GetComponent<GhostAI>().speed;
        pinky.GetComponent<GhostAI>().speed = pinky.GetComponent<GhostAI>().speed;
        inky.GetComponent<GhostAI>().speed = inky.GetComponent<GhostAI>().speed;
        knightro.GetComponent<PlayerController>().speed += Level * ghostSpeed / 2;
    }

    private void ResetVars()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerController.killstreak = 0;
        lives = 3;
        UpdateLives(lives);
    }

    void Update()
    {
        scoreText.text = "" + score;

        if (scared && _timeToCalm <= Time.time)
        {
            CalmGhosts();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            gameState = GameState.Paused;
            if (GameManager.gameState == GameManager.GameState.Paused)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (lives == 0)
        {
            gameState = GameState.GameOver;
            
            gameOverCanvas.SetActive(true);
            finalScoreText.text = "" + score;
            Time.timeScale = 0;
        }
        
        if (PlayerController.pelletCount == 320)
        {
            DisplayVictoryScreen();
        }
    }
    public void Resume()
    {
        gameState = GameState.Game;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        gameState = GameState.Init;
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        PlayerController.pelletCount = 0;
        score = 0;
        ResetVars();
        SceneManager.LoadScene("Menu Scene");
        Time.timeScale = 1;
 
    }
    public void ResetScene()
    {
        CalmGhosts();

        knightro.transform.position = new Vector3(13f, 11f, 0f);
        blinky.transform.position = new Vector3(14.5f, 20f, 0f);
        pinky.transform.position = new Vector3(14.5f, 17f, 0f);
        inky.transform.position = new Vector3(16.5f, 17f, 0f);
        clyde.transform.position = new Vector3(12.5f, 17f, 0f);

        knightro.GetComponent<PlayerController>().ResetDestination();
        blinky.GetComponent<GhostAI>().InitializeGhost();
        pinky.GetComponent<GhostAI>().InitializeGhost();
        inky.GetComponent<GhostAI>().InitializeGhost();
        clyde.GetComponent<GhostAI>().InitializeGhost();

        gameState = GameState.Init;
        Invoke("StartGame", 2);
    }

    public void ResetScene2()
    {
        CalmGhosts();

        knightro.transform.position = new Vector3(14.5f, 8f, 0f);
        blinky.transform.position = new Vector3(14.5f, 20f, 0f);
        pinky.transform.position = new Vector3(14.5f, 17f, 0f);
        inky.transform.position = new Vector3(16.5f, 17f, 0f);
        clyde.transform.position = new Vector3(12.5f, 17f, 0f);

        knightro.GetComponent<PlayerController>().ResetDestination2();
        blinky.GetComponent<GhostAI>().InitializeGhost2();
        pinky.GetComponent<GhostAI>().InitializeGhost2();
        inky.GetComponent<GhostAI>().InitializeGhost2();
        clyde.GetComponent<GhostAI>().InitializeGhost2();

        gameState = GameState.Init;
        Invoke("StartGame", 2);
    }

    public void StartGame()
    {
        gameState = GameState.Game;
    }

    public void ToggleScare()
    {
        if (!scared) ScareGhosts();
        else CalmGhosts();
    }
    public void ScareGhosts()
    {
        scared = true;
        blinky.GetComponent<GhostAI>().Frighten();
        pinky.GetComponent<GhostAI>().Frighten();
        inky.GetComponent<GhostAI>().Frighten();
        clyde.GetComponent<GhostAI>().Frighten();
        _timeToCalm = Time.time + scareLength;
        if (scared == true)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();            
        }
        

    }
    public void CalmGhosts()
    {
        scared = false;
        blinky.GetComponent<GhostAI>().Calm();
        pinky.GetComponent<GhostAI>().Calm();
        inky.GetComponent<GhostAI>().Calm();
        clyde.GetComponent<GhostAI>().Calm();
        PlayerController.killstreak = 0;
        if(scared == false)
        {
            musicSource.clip = gamePlay;
            musicSource.Play();
        }
    }
    void Assign()
    {
        clyde = GameObject.Find("clyde");
        pinky = GameObject.Find("pinky");
        inky = GameObject.Find("inky");
        blinky = GameObject.Find("blinky");
        knightro = GameObject.Find("knightro");


    }

    public void LoseLife()
    {
        lives--;
        gameState = GameState.Dead;

        UpdateLives(lives);
        if(gameState == GameState.Dead)
        {
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }
        else
        {
            musicSource.clip = gamePlay;
            musicSource.Play();
        }

    }

    public void UpdateLives(int l)
    {
        switch(l)
        {
            case 3:
                livesImage[2].enabled = true;
                livesImage[1].enabled = true;
                livesImage[0].enabled = true;
                break;
            case 2:
                livesImage[2].enabled = false;
                livesImage[1].enabled = true;
                livesImage[0].enabled = true;
                break;
            case 1:
                livesImage[2].enabled = false;
                livesImage[1].enabled = false;
                livesImage[0].enabled = true;
                break;
            case 0:
                livesImage[2].enabled = false;
                livesImage[1].enabled = false;
                livesImage[0].enabled = false;
                break;
        }
    }

    public void DisplayVictoryScreen()
    {
        victoryCanvas.SetActive(true);
        gameState = GameState.Victory;
        victoryScoreText.text = "" + score;
        Time.timeScale = 0;
    }
}
