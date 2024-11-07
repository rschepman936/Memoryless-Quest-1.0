using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whereCanMove;

    public LayerMask whereExit;

    public playersTurn playersTurn;
    public string sceneToLoad;
    public Transform playerPos;
    public Vector2 playerSave;
    public VectorValue playerStorage;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
        
        //checks if it is the players turn
        if(playersTurn.isPlayerTurn==true){
        
            //Stops the player from holding down directional input by checking if they have pressed a key this turn
            //if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D)){
        
                //checks if the object is far enough away from the movePoint to see if it should begin to move 
                if(Vector3.Distance(transform.position, movePoint.position) <= 0.5f){
        
                    //checks if the input was horizontal and then moves the user towards movePoint before ending the players turn
                    if  (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f ){
                        if(Physics2D.OverlapCircle(playerPos.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f),.2f,whereExit)){
                            LoadScene(sceneToLoad);
                        }else if(Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f),.2f,whereCanMove)){
                            movePoint.position +=  new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f);
                            playersTurn.isPlayerTurn=false;
                        }
                    }else if  (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f ){
                        if(Physics2D.OverlapCircle(playerPos.position + new Vector3(0f,Input.GetAxisRaw("Vertical"),0f),.2f,whereExit)){
                            LoadScene(sceneToLoad);
                        }else if(Physics2D.OverlapCircle(movePoint.position + new Vector3(0f,Input.GetAxisRaw("Vertical"),0f),.2f,whereCanMove)){
                            movePoint.position +=  new Vector3(0f,Input.GetAxisRaw("Vertical"),0f);
                            playersTurn.isPlayerTurn=false;
                        }
                    }
                }
            }
        //}
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        playerStorage.initialValue = playerSave;
    }
}
