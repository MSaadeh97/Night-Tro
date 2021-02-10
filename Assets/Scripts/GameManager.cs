using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Level = 0;

    public static int lives = 3;
    public List<Image> livesImage = new List<Image>(3);

    public Text scoreText;
    static public int score;

    public enum GameState { Init, Game, Dead, Scores }
    public static GameState gameState;

    private GameObject knightro;
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

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
               // _instance = GameObject.FindObjectsOfType<GameManager>();
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
    }

    void OnLevelWasLoaded()
    {
        if (Level == 0) lives = 3;

        Debug.Log("Level " + Level + " Loaded!");
        Assign();
        ResetVars();


        // Adjust Ghost variables!
        //clyde.GetComponent<GhostMove>().speed += Level * ghostSpeed;
        //blinky.GetComponent<GhostMove>().speed += Level * ghostSpeed;
        //pinky.GetComponent<GhostMove>().speed += Level * ghostSpeed;
        //inky.GetComponent<GhostMove>().speed += Level * ghostSpeed;
        knightro.GetComponent<PlayerController>().speed += Level * ghostSpeed / 2;
    }

    private void ResetVars()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerController.killstreak = 0;
    }

    void Update()
    {
        scoreText.text = "Score\n" + score;

        if (scared && _timeToCalm <= Time.time)
        {
            CalmGhosts();
        }
    }

    public void ResetScene()
    {
        CalmGhosts();

        knightro.transform.position = new Vector3(14.5f, 11f, 0f);
        blinky.transform.position = new Vector3(15f, 20f, 0f);
        pinky.transform.position = new Vector3(14.5f, 17f, 0f);
        inky.transform.position = new Vector3(16.5f, 17f, 0f);
        clyde.transform.position = new Vector3(12.5f, 17f, 0f);

        knightro.GetComponent<PlayerController>().ResetDestination();
        //blinky.GetComponent<GhostMove>().InitializeGhost();
        //pinky.GetComponent<GhostMove>().InitializeGhost();
        //inky.GetComponent<GhostMove>().InitializeGhost();
        //clyde.GetComponent<GhostMove>().InitializeGhost();

        gameState = GameState.Init;


    }


    public void ToggleScare()
    {
        if (!scared) ScareGhosts();
        else CalmGhosts();
    }
    public void ScareGhosts()
    {
        scared = true;
        //blinky.GetComponent<GhostMove>().Frighten();
        //pinky.GetComponent<GhostMove>().Frighten();
        //inky.GetComponent<GhostMove>().Frighten();
        //clyde.GetComponent<GhostMove>().Frighten();
        _timeToCalm = Time.time + scareLength;
    }
    public void CalmGhosts()
    {
        scared = false;
        //blinky.GetComponent<GhostMove>().Calm();
        //pinky.GetComponent<GhostMove>().Calm();
        //inky.GetComponent<GhostMove>().Calm();
        //clyde.GetComponent<GhostMove>().Calm();
        PlayerController.killstreak = 0;
    }
    void Assign()
    {
        // find and assign ghosts
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

        //UIScript ui = GameObject.FindObjectOfType<UIScript>();
        //Destroy(ui.lives[ui.lives.Count - 1]);
        //ui.lives.RemoveAt(ui.lives.Count - 1);
    }

    public static void DestroySelf()
    {

        score = 0;
        Level = 0;
        lives = 3;
    }
}
