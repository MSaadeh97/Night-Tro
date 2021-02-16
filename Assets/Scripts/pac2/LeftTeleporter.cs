using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTeleporter : MonoBehaviour
{
    public GameObject knightro;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "knightro")
        {
            knightro.GetComponent<PlayerController>().TeleportRight();
        }
    }
}
