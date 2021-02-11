using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = .4f;

    Vector2 dir = Vector2.zero;
    Vector2 dest = Vector2.zero;
    Vector2 queueDir = Vector2.zero;

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
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        dest = transform.position;
    }


    void FixedUpdate()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                UserInput();
                Animate();
                break;

            case GameManager.GameState.Dead:
                if (!isDead)
                    StartCoroutine("PlayDeathAnimation");
                break;

        }
    }

    IEnumerator PlayDeadAnimation()
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
    }

    void Animate()
    {
        Vector2 direction = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", direction.x);
        GetComponent<Animator>().SetFloat("DirY", direction.y);
    }

    bool Valid(Vector2 direction)
    {
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pellet" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        dest = new Vector2(14.5f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void UserInput()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if (Input.GetAxis("Horizontal") > 0)
        {
            queueDir = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            queueDir = Vector2.right;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            queueDir = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            queueDir = Vector2.left;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            queueDir = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            queueDir = Vector2.up;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            queueDir = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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

}

