using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;

    public playersTurn playersTurn;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
        if(playersTurn.isPlayerTurn==true){
            if(Vector3.Distance(transform.position, movePoint.position) <= 0.5f){
                if  (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f ){
                    movePoint.position +=  new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f);
                    playersTurn.isPlayerTurn=false;
                }

                if  (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f ){
                    movePoint.position +=  new Vector3(0f,Input.GetAxisRaw("Vertical"),0f);
                    playersTurn.isPlayerTurn=false;
                }
            }
        }
    }
}