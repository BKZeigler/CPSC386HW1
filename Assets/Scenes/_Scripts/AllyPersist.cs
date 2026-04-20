using UnityEngine;

public class AllyPersist : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //void OnApplicationQuit()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //}
}
