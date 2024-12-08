using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetShard : MonoBehaviour
{
    public gotMemoryShard gotShard;
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            gotShard.haveShard = true;
            if (MemoryController.Instance != null)
            {
                MemoryController.Instance.AddMemoryCount(1);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("MemoryController.Instance is null");
            }
        }
   }
}
