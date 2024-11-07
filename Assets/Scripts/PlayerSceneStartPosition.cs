using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneStartPosition : MonoBehaviour
{
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.initialValue;
    }
}
