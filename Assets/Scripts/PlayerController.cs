using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = .32f;

    public Vector2 dir = Vector2.zero;
    public Vector2 dest = Vector2.zero;
    public Vector2 queueDir = Vector2.zero;

    public bool isDead = false;
    public bool isVictory = false;

    public static int killstreak = 0;

    private GameManager GM;
    //private ScoreManager SM;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public int speedUpCounter;

    void Start()
    {
        GetComponent<Animator>().SetFloat("DirX", 0);
        GetComponent<Animator>().SetFloat("DirY", 0);
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        dest = transform.position;
        GameManager.gameState = GameManager.GameState.Game;
    }


    void FixedUpdate()
    {
        UserInput();
        Animate();
        if (GameManager.gameState == GameManager.GameState.Game)
        {
            UserInput();
            Animate();
        }


        if (GameManager.gameState == GameManager.GameState.Dead)
        {
            StartCoroutine("PlayDeathAnim");
        }
    }

    IEnumerator PlayDeathAnim()
    {
        isDead = true;
        GetComponent<Animator>().SetBool("isDead", true);
        yield return new WaitForSeconds(1.02f);
        GetComponent<Animator>().SetBool("isDead", false);
        isDead = false;

        if (GameManager.lives <= 0)
        {
            //Show End Screen
        }
        else
        {
            GM.ResetScene();
        }
        StopCoroutine("PlayDeathAnim");
    }

    void Animate()
    {
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "Pellet" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        dest = new Vector2(13f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void UserInput()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed/2);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if (GameManager.gameState != GameManager.GameState.Dead)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                queueDir = Vector2.right;

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                queueDir = Vector2.left;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                queueDir = Vector2.up;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                queueDir = Vector2.down;
            }
            if (Vector2.Distance(dest, transform.position) < 0.00001f)
            {
                if (Valid(queueDir))
                {
                    dest = (Vector2)transform.position + queueDir;
                    dir = queueDir;
                }
                else
                {
                    if (Valid(dir))
                        dest = (Vector2)transform.position + dir;
                }
            }
        }
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            if (GameManager.gameState != GameManager.GameState.Paused || GameManager.gameState != GameManager.GameState.Dead)
            {
                GameManager.gameState = GameManager.GameState.Game;
            }
        }
    }

    public Vector2 getDir()
    {
        return dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;

    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Speed"))
        {
            Debug.Log("SpeedUp");
            speed += .05f;
            speedUpCounter += 6;
            StartCoroutine("SpeedUpTimer");
            Destroy(collision.gameObject);
        }
    }

    IEnumerator SpeedUpTimer()
    {
        while (speedUpCounter > 0)
        {
            yield return new WaitForSeconds(1);
            speedUpCounter--;
        }
        Invoke("SpeedUpEnd", 0);
    }

    void SpeedUpEnd()
    {
        speed = .32f;
        speedUpCounter = 0;
        StopCoroutine("SpeedUpTimer");
    }
}

