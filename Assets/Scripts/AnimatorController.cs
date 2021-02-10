using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("DirX", 0);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Animator>().SetBool("isDeath", true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Animator>().SetBool("isVictory", true);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Animator>().SetFloat("DirY", 1);
            GetComponent<Animator>().SetFloat("DirX", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<Animator>().SetFloat("DirX", -1);
            GetComponent<Animator>().SetFloat("DirY", 0);

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetComponent<Animator>().SetFloat("DirY", -1);
            GetComponent<Animator>().SetFloat("DirX", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetComponent<Animator>().SetFloat("DirX", 1);
            GetComponent<Animator>().SetFloat("DirY", 0);
        }
    }
}

