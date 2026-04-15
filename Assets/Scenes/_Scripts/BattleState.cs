using UnityEngine;

// Will check for win/loss conditions and call appropriate functions

public class BattleState : MonoBehaviour
{

    public int AllyCount; // stores how many allies are alive
    public int EnemyCount; // stores how many enemies are alive
    void Start()
    {
        Time.timeScale = 0; // battle should start paused, so that player can drag units to the field
    }
    
    void Update() // look for win/loss conditions every frame
    {
        if (AllyCount <= 0)
        {
            Lose(); // if no allies, must have lost
        }
        else if (EnemyCount <= 0)
        {
            Win(); // if no enemies, must have won
        }
    }

    private void Win() // loads the win scnene
    {
        Camera.main.orthographicSize = 7;
        Camera.main.transform.position += new Vector3(2, 0, 0);
        // call saving and loading script to always move to a between screen unless the cleared battle was the last
        UnityEngine.SceneManagement.SceneManager.LoadScene("Between");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }

    private void Lose() // loads the lose scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Between"); // for testing rn
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }
}
