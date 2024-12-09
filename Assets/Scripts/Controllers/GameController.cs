using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
    [SerializeField]
    private UIController uiController;

    public bool IsGamePlay {private set; get;} = false;

    public void GameStart(){
        uiController.GameStart();

        Debug.Log("Start Button");

        MemoryController.Instance.ResetMemoryCount();
    }

    public void GameExit(){
        #if UNITY_EDITOR //if it's in the editor mode, exit it
        UnityEditor.EditorApplication.ExitPlaymode();
        #else //if it's in the game, destroy
        Application.Quit();
        #endif

        Debug.Log("Exit Button");

        MemoryController.Instance.ResetMemoryCount();
    }

    public void GameReTry(){
        uiController.GameReStart();

        Debug.Log("Re-try Button");

        MemoryController.Instance.ResetMemoryCount();
    }

    public void GoodEnding(){
        uiController.GoodEnding();

        Debug.Log("Good Ending");

        MemoryController.Instance.ResetMemoryCount();
    }

    public void BadEnding(){
        uiController.BadEnding();

        Debug.Log("Bad Ending");

        MemoryController.Instance.ResetMemoryCount();
    }

    public void Update()
    {
        
    }
}
