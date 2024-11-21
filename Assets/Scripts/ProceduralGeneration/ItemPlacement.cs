using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public GameObject prefab;
    void Start()
    {
        for (var i = 0; i < 3; i++)
        {
            Instantiate(prefab, new Vector3(i * 2.0f, 0, 4), Quaternion.identity);
        }
    }
}