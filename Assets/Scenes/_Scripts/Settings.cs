using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu; // manually assign in inspector

    void Start()
    {
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // escape toggles settings
        {
            if (settingsMenu.activeSelf)
            {
                if (Time.timeScale == 0f)
                {
                    Time.timeScale = 1f; // Unpause the game by setting time scale back to 1
                }
                settingsMenu.SetActive(false); // close the settings menu
            }
            else
            {
                if (Time.timeScale != 0f)
                {
                    Time.timeScale = 0f; // Pause the game by setting time scale to 0
                }
                settingsMenu.SetActive(true); // open the settings menu
            }
        }
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Unpause the game by setting time scale back to 1
        }
        settingsMenu.SetActive(false); // close the settings menu
    }

    public void MainMenu() // return to main menu
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Unpause the game by setting time scale back to 1
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // load main menu scene
    }

    public void makeVisible() // used for settings button
    {
        settingsMenu.SetActive(true); // opens the settings menu
    }
}

