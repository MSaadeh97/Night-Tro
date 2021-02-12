using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] adjacent;
    public Vector2[] validDir;

    void Start()
    {
        validDir = new Vector2[adjacent.Length];

        for (int i = 0; i < adjacent.Length; i++)
        {
            Node adj = adjacent[i];
            Vector2 tempVect = adj.transform.localPosition - transform.localPosition;

            validDir[i] = tempVect.normalized;
        }
    }

    void Update()
    {
        
    }
}
