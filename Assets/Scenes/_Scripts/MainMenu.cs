using UnityEngine;

// Will handle most UI buttons

public class MainMenu : MonoBehaviour // stores UI functions
{
    public void LoadGame() // load first level scene
    {

        //var savedUnits = SaveManager.LoadUnits();

        //AllyPersist persist = FindFirstObjectByType<AllyPersist>();
        //if (persist == null)
        //{
        //    GameObject prefab = Resources.Load<GameObject>("AllyPersist");
        //    GameObject obj = Instantiate(prefab);
        //    DontDestroyOnLoad(obj);
        //    persist = obj.GetComponent<AllyPersist>();
        //}

        //foreach (var data in savedUnits)
        //{
        //    UnitCreator.CreateUnit(data, persist.transform);
        //}

        Debug.Log("Load Game button clicked!"); // for testing
        if (LevelProgress.Instance.IsLevelDefeated("LevelOne") && !LevelProgress.Instance.IsLevelDefeated("LevelTwo")) // if first level defeated
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTwo"); // load second
        }
        else if (LevelProgress.Instance.IsLevelDefeated("LevelTwo") && !LevelProgress.Instance.IsLevelDefeated("LevelThree")) // if second level defeated
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelThree"); // load third
        }
        else // else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelOne"); // load first
        }
        Camera.main.orthographicSize = 7;
    }

    public void BetweenLoad()
    {
        if (LevelProgress.Instance.IsLevelDefeated("LevelOne") && !LevelProgress.Instance.IsLevelDefeated("LevelTwo")) // if first level defeated
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTwo"); // load second
        }
        else if (LevelProgress.Instance.IsLevelDefeated("LevelTwo") && !LevelProgress.Instance.IsLevelDefeated("LevelThree")) // if second level defeated
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelThree"); // load third
        }
        else // else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelOne"); // load first
        }
        Camera.main.orthographicSize = 7;
        Camera.main.transform.position += new Vector3(2, 0, 0);
    }
    public void ExitGame() // exit the application
    {
        Application.Quit();
    }

    public void RestartGame() // load main menu scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
