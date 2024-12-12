using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            SceneManager.LoadScene("DeathScene");

            Debug.Log("Game Over");
        }
    }
}
