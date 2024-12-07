using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
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

    public gotMemoryShard gotShard;

    public FloorTracker floorTracker;

    [SerializeField]
    public whichEnemyTurn enemyTracker;

    public bool invisActive  = false;

    public bool canInvis = true;

    public int invisTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        canInvis = true;
        invisActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
        if(enemyTracker.enemyTurn==0){
            if(Vector3.Distance(transform.position, movePoint.position) <= 0.1f){
                if  (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f ){
                    if(Physics2D.OverlapCircle(playerPos.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f),.2f,whereExit)){                            
                        if(floorTracker.floorNumber == 5){
                            LoadScene("VictoryScene");
                        }else{
                            LoadScene(sceneToLoad);
                        }
                    }else if(Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f),.2f,whereCanMove)){
                        movePoint.position +=  new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f);
                        if(invisActive == true){
                            invisTimer++;
                            if(invisTimer == 5){
                                endInvis();
                            }
                        }else{
                            enemyTracker.enemyTurn = enemyTracker.totalEnemies; 
                        } 
                    }
                }else if  (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f ){
                    if(Physics2D.OverlapCircle(playerPos.position + new Vector3(0f,Input.GetAxisRaw("Vertical"),0f),.2f,whereExit)){
                        if(floorTracker.floorNumber == 5){
                            LoadScene("VictoryScene");
                        }else{
                            LoadScene(sceneToLoad);
                        }
                    }else if(Physics2D.OverlapCircle(movePoint.position + new Vector3(0f,Input.GetAxisRaw("Vertical"),0f),.2f,whereCanMove)){
                        movePoint.position +=  new Vector3(0f,Input.GetAxisRaw("Vertical"),0f);
                        if(invisActive == true){
                            invisTimer++;
                            if(invisTimer == 5){
                                endInvis();
                            }
                        }else{
                            enemyTracker.enemyTurn = enemyTracker.totalEnemies; 
                        }                       
                    }
                }
            }
        }

        if (Keyboard.current.spaceKey.isPressed){
            goInvis();
        }

    }

    public void goInvis(){
        if(canInvis == true){
            canInvis = false;
            invisActive = true;
            invisTimer = 0;
            GetComponent<SpriteRenderer>().color = new Color(255,255,255,.3f);
        }
    }

    public void endInvis(){
        GetComponent<SpriteRenderer>().color = new Color(255,255,255,1);
        invisActive = false;
    }


    public void LoadScene(string sceneName)
    {
        floorTracker.floorNumber++;
        SceneManager.LoadScene(sceneName);
        playerStorage.initialValue = playerSave;
    }
}
