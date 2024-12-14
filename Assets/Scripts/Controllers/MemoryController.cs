using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryController : MonoBehaviour
{
    public static MemoryController Instance;  
    public int havingCount = 0;
    public GameObject memorySnd;


    public void Update(){

		Scene currentScene = SceneManager.GetActiveScene ();

	
		string sceneName = currentScene.name;

		if (sceneName == "GameStart") 
		{
			havingCount = 0;
            Debug.Log("reset count");
		}

    }
    private void Awake()
    {
        // Singleton pattern for keep the value when the scences changing
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //make it do not destroy
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMemoryCount(int amount = 1)
    {
        AudioSource havingSnd = memorySnd.GetComponent<AudioSource>();
        havingSnd.Play();
        
        havingCount += amount;
        Debug.Log("MemoryFragment: " + havingCount);
    }
}
