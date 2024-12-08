using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour{
    /*
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject ingameUI;
    [SerializeField]
    private GameObject ingame2UI;
    [SerializeField]
    private GameObject gameoverUI;
    */
    [SerializeField]
    private GameObject parentUI;
    [SerializeField]
    private GameObject goodUI;
    [SerializeField]
    private GameObject badUI;

    public void GameStart(){
        /*
        mainUI.SetActive(false);
        ingameUI.SetActive(true);
        */
        SceneManager.LoadScene("Tutorial");
    }

    public void GameReStart(){
        /*
        gameoverUI.SetActive(false);
        ingameUI.SetActive(true);
        */
        SceneManager.LoadScene("GameStart");
    }

    public void GoodEnding(){
        parentUI.SetActive(false);
        goodUI.SetActive(true);
    }

    public void BadEnding(){
        parentUI.SetActive(false);
        badUI.SetActive(true);
    }

    public void Update()
    {
        
    }
}
