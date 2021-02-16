using UnityEngine;
using System.Collections;

public class PowerPellet: MonoBehaviour
{

    public GameManager gm;

    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gm == null)
        {
            Debug.Log("Energizer did not find Game Manager!");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "knightro")
        {
            gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
