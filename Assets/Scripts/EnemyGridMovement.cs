using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using static UnityEngine.Random;

public class EnemyGridMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whereCanMove;
    public playersTurn playersTurn;

    public float moveChance = 0.9f;

    public Transform goal; // Assign the goal object in the Inspector

    public int enemyNumber;

    [SerializeField]
    public whichEnemyTurn enemyTracker;

    bool willMove = true;

    public float moveValue;

    public FloorTracker floorTracker;
    void Start()
    {
        moveValue = Range(0f, 1.0f);
        if (moveValue <= moveChance ){
            willMove = true;
            GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }else{
            willMove = false;
            GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
    }

    void Update()
    {

        if(movePoint.parent != null){
            movePoint.parent = null;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (enemyTracker.enemyTurn == enemyNumber && Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            if (willMove == true) // Check if enemy should move
            {
                MoveTowardsGoal();
                enemyTracker.enemyTurn--;
            }else{
                enemyTracker.enemyTurn--;
            }
            moveValue = Range(0f, 1.0f);
            if (moveValue <= moveChance ){
                willMove = true;
                GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }else{
                willMove = false;
                GetComponent<Renderer>().material.color = new Color(255, 255, 0);
            }
        }

        

    }

    void MoveTowardsGoal()
    {
        float x;
        float y;
        Vector2 direction = (goal.position - transform.position).normalized;
        if(direction.x>0){
            x = (float)Math.Ceiling(direction.x);
        }else{
            x = (float)Math.Floor(direction.x);
        }
        if(direction.y>0){
            y = (float)Math.Ceiling(direction.y);
        }else{
            y = (float)Math.Floor(direction.y);
        }

        if(x*y != 0){
            if(Mathf.Abs(direction.y) > Mathf.Abs(direction.x)){
                x = 0;
            }else{
                y=0;
            }
        }

        Vector3 nextMove = new Vector2(x, y);

        var prevPos = movePoint.position;


        if (Physics2D.OverlapCircle(movePoint.position + nextMove, .2f, whereCanMove))
        {
            prevPos = movePoint.position;
            movePoint.position += nextMove;
            playersTurn.isPlayerTurn = true;
        }
        else
        {
            // Try alternative moves if the direct path is blocked
            Vector3[] alternativeMoves = { new Vector3(nextMove.x, 0,0), new Vector3(0, nextMove.y,0) };

            foreach (Vector3 move in alternativeMoves)
            {
                if (Physics2D.OverlapCircle(movePoint.position + move, .2f, whereCanMove))
                {
                    prevPos = movePoint.position;
                    movePoint.position += move;
                    playersTurn.isPlayerTurn = true;
                    return; // Exit after finding a valid move
                }
            }

            // No valid moves found

            movePoint.position = prevPos;
        }
    }
}
