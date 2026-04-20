using UnityEngine;

public class LevelProgress : MonoBehaviour // keeps track of what levels player has beat
{

    public static LevelProgress Instance;
    void Awake()
    {
        if(Instance == null) // if progress doesnt exist
        {
            Instance = this; // create it
            DontDestroyOnLoad(gameObject); // level progress should not reset on scene change
        }
        else // if it already exists
        {
            Destroy(gameObject); // Only one instance of level progress should exist
        }

    }
    void Start()
    {
    }
    void Update()
    {        
    }

    public void LevelDefeated(string levelName) // call this whe a boss is defeated
    {
        PlayerPrefs.SetInt(levelName + "Defeated", 1); // set key to 1 for that level to say it is defeated
        PlayerPrefs.Save(); // save that data
    }

    public void ClearProgress() // called when game is won so you can play again
    {
        PlayerPrefs.SetInt("LevelOneDefeated", 0); // mark level 1 as undefeated
        PlayerPrefs.SetInt("LevelTwoDefeated", 0); // mark level 2 as undefeated
        PlayerPrefs.SetInt("LevelThreeDefeated", 0); // mark level 3 as undefeated
        PlayerPrefs.Save(); // save that data
    }

    public bool IsLevelDefeated(string levelName) // called when loading the game to check progress
    {
        return PlayerPrefs.GetInt(levelName + "Defeated", 0) == 1; // level is defeated if value is 1
    }
}
