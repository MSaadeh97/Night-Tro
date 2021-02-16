using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTeleporter : MonoBehaviour
{
    public GameObject knightro;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "knightro")
        {
            knightro.GetComponent<PlayerController>().TeleportLeft();
        }
    }
}
