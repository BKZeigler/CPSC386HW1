using UnityEngine;

// Will handle most UI buttons

public class MainMenu : MonoBehaviour // stores UI functions
{
    public void LoadGame() // load first level scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
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
