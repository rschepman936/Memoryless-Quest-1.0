using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour{
    public string levelName;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerStorage.initialValue = playerPos;
            SceneManager.LoadScene(levelName);
        }  
    }
}