using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetShard : MonoBehaviour
{

    public gotMemoryShard gotShard;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            gotShard.haveShard = true;
        }
   }
}
