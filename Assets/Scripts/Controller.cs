using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 4.0f;

    public Vector2 dir = Vector2.zero;
    public Vector2 queueDir;

    public Node currentNode, prevNode, targetNode;


    void Start()
    {
       /* Node node = GetNodeAtPosition(transform.localPosition);

        if (node != null)
        {
            currentNode = node;
        }
        */

        dir = Vector2.left;
        ChangePos(dir);
    }

    void Update()
    {
        CheckInput();

        AnimUpdate();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePos(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangePos(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePos(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePos(Vector2.right);
        }


    }

    void ChangePos (Vector2 d)
    {
        if (d != dir)
        {
            queueDir = d;
        }

        if (currentNode != null)
        {
            Node movetoNode = ValidMove(d);

            if (movetoNode != null)
            {
                dir = d;
                targetNode = movetoNode;
                prevNode = currentNode;
                currentNode = null;
            }
        }
    
    }

    void Move()
    {
        if (targetNode != currentNode && targetNode != null)
        {
            if (OverShotTarget())
            {
                currentNode = targetNode;

                transform.localPosition = currentNode.transform.position;

                Node moveToNode = ValidMove(queueDir);

                if(moveToNode != null)
                {
                    dir = queueDir;
                }
                if(moveToNode == null)
                {
                    moveToNode = ValidMove(dir);
                }
                if(moveToNode != null)
                {
                    targetNode = moveToNode;
                    prevNode = currentNode;
                    currentNode = null;
                }
                else
                {
                    dir = Vector2.zero;
                }
            }
            else
            {
                transform.localPosition += (Vector3)(dir * speed) * Time.deltaTime;
            }
        }   
    }

    void MoveToNode (Vector2 d)
    {
        Node moveToNode = ValidMove(d);
        if (moveToNode != null)
        {
            transform.localPosition = moveToNode.transform.position;
            currentNode = moveToNode;
        }
    }

    void AnimUpdate()
    {
        if (dir == Vector2.up)
        {

        }
        if (dir == Vector2.left)
        {

        }
        if (dir == Vector2.down)
        {

        }
        if (dir == Vector2.right)
        {

        }
    }

    Node ValidMove (Vector2 d)
    {
        Node moveToNode = null;

        for (int i = 0; i < currentNode.adjacent.Length; i ++)
        {
            if (currentNode.validDir [i] == d)
            {
                moveToNode = currentNode.adjacent[i];
                break;
            }
        }

        return moveToNode;
    }
    /*
    Node GetNodeAtPosition (Vector2 pos)
    {
        GameObject tile = GameObject.Find("Gameboard").GetComponent<Board>().board[(int)pos.x, (int)pos.y];

        if (tile != null)
        {
            return tile.GetComponent<Node>();
        }
        return null;
    }
    */
    bool OverShotTarget()
    {
        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float nodeToSelf = LengthFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float LengthFromNode(Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)prevNode.transform.position;
        return vec.sqrMagnitude;
    }

}
