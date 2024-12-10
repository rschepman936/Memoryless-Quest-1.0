using UnityEngine;

public class MemoryController : MonoBehaviour
{
    public static MemoryController Instance;  
    public int havingCount = 0;
    public GameObject memorySnd;

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
