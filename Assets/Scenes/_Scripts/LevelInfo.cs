using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField]
    public string levelName; // store level name

    public string GetLevelName() // return level name
    {
        return levelName;
    }
}
