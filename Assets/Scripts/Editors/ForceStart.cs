using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceStart : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void FirstLoad(){
        if (SceneManager.GetActiveScene().name.CompareTo("GameStart") != 0){
            SceneManager.LoadScene("GameStart");
        }
    }
}