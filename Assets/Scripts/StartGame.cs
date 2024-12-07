using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class StartGame : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.enterKey.isPressed){
            SceneManager.LoadScene("Tutorial");
        }
    }
}
