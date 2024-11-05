using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whereCanMove;

    private int moveCounter = 0;

    public playersTurn playersTurn;


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!playersTurn.isPlayerTurn && Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            // Simple left-left-right-right movement
            switch (moveCounter)
            {
                case 0: // Left
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whereCanMove))
                    {
                        movePoint.position += new Vector3(-1f, 0f, 0f);
                    }
                    break;
                case 1: // Left
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whereCanMove))
                    {
                        movePoint.position += new Vector3(-1f, 0f, 0f);
                    }
                    break;
                case 2: // Right
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whereCanMove))
                    {
                        movePoint.position += new Vector3(1f, 0f, 0f);
                    }
                    break;
                case 3: // Right
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whereCanMove))
                    {
                        movePoint.position += new Vector3(1f, 0f, 0f);
                    }
                    break;
            }

            moveCounter = (moveCounter + 1) % 4; // Cycle through 0-3
            playersTurn.isPlayerTurn = true;
        }
    }
}
