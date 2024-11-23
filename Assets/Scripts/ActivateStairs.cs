using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivateStairs : MonoBehaviour
{

    public gotMemoryShard gotShard;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 0);
        gotShard.haveShard = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gotShard.haveShard == true){
            GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 255);
            gameObject.layer = LayerMask.NameToLayer("Exit");
        }
    }
}
