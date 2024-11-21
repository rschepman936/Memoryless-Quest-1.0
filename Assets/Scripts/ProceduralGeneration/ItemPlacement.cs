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
            Instantiate(prefab, new Vector3(-10, 0, 0), Quaternion.identity);
        }
    }
}