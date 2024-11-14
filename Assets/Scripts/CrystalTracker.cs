using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalTracker : MonoBehaviour
{
    int crystalCollected = 0;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Crystal")){
            crystalCollected++;
            if(crystalCollected==3){
                SceneManager.LoadScene("VictoryScene");
            }
        }
   }
}
