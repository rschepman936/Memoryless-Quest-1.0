using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whereCanMove;
    public playersTurn playersTurn;

    public Transform goal; // Assign the goal object in the Inspector

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!playersTurn.isPlayerTurn && Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            MoveTowardsGoal();
        }
    }

    void MoveTowardsGoal()
    {
        if (goal == null)
        {
            Debug.LogError("Goal not assigned to EnemyGridMovement script!");
            return;
        }

        Vector2 direction = (goal.position - transform.position).normalized;
        Vector2 nextMove = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));


        if (Physics2D.OverlapCircle(movePoint.position + nextMove, .2f, whereCanMove))
        {
            movePoint.position += nextMove;
            playersTurn.isPlayerTurn = true;
        }
        else
        {
            // Try alternative moves if the direct path is blocked
            Vector2[] alternativeMoves = { new Vector2(nextMove.x, 0), new Vector2(0, nextMove.y) };

            foreach (Vector2 move in alternativeMoves)
            {
                if (Physics2D.OverlapCircle(movePoint.position + move, .2f, whereCanMove))
                {
                    movePoint.position += move;
                    playersTurn.isPlayerTurn = true;
                    return; // Exit after finding a valid move
                }
            }

            // No valid moves found
            Debug.Log("Enemy stuck!"); // Or handle differently, e.g., find a new path
        }
    }
}
