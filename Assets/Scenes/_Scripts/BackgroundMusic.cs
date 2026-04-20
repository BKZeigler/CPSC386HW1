using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    void Awake()
    {
        if(Instance == null) // if background music doesnt exist
        {
            Instance = this; // create it
            DontDestroyOnLoad(gameObject); // background music should not reset on scene change
        }
        else // if it already exists
        {
            Destroy(gameObject); // Only one instance of background music should exist
        }

    }
}
