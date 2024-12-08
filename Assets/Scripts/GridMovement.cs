using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
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


    //for animation
    private Animator animator_;


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        canInvis = true;
        invisActive = false;

        //for animation
        animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
        // to ignore the movement during story is on air
        if(StoryController.isStory == true){
            if (Input.anyKeyDown){
            // check whether any keys pressd
                if (!Input.GetKeyDown(KeyCode.Space)){
                    // ignore that key
                    //Debug.Log("Input Other Key");
                    return;
                }
            }
        }
        else{
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
        }

        if (Keyboard.current.spaceKey.isPressed){
            goInvis();
        }

    }

    void FixedUpdate(){
        // for animation
        Move_Animation();
    }

    public void goInvis(){
        if(canInvis == true){
            canInvis = false;
            invisActive = true;
            invisTimer = 0;
            //GetComponent<SpriteRenderer>().color = new Color(255,255,255,.3f);
        }
    }

    public void endInvis(){
        //GetComponent<SpriteRenderer>().color = new Color(255,255,255,1);
        invisActive = false;
    }


    public void LoadScene(string sceneName)
    {
        floorTracker.floorNumber++;
        SceneManager.LoadScene(sceneName);
        playerStorage.initialValue = playerSave;
    }

    //for animation
    void Move_Animation() {
        Vector3 movePosition = Vector3.zero;

        // move left
        if(Input.GetAxisRaw("Horizontal") < 0) {
            movePosition = Vector3.left;
            GetComponent<SpriteRenderer>().flipX = true;
            animator_.SetBool("isMove", true);
        }
        // move right
        else if(Input.GetAxisRaw("Horizontal") > 0) {
            movePosition = Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
            animator_.SetBool("isMove", true);
        }
        //move down
        else if(Input.GetAxisRaw("Vertical") < 0){
            movePosition = Vector3.down;
            animator_.SetBool("isMove", true);
        }
        //move up
        else if(Input.GetAxisRaw("Vertical") > 0){
            movePosition = Vector3.up;
            animator_.SetBool("isMove", true);
        }
        // no move
        else {
            animator_.SetBool("isMove", false);
        }
    }
}