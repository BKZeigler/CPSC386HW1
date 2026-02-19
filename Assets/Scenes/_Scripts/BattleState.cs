using UnityEngine;

// Will check for win/loss conditions and call appropriate functions

public class BattleState : MonoBehaviour
{

    public int AllyCount;
    public int EnemyCount;
    // Update is called once per frame
    void Update()
    {
        if (AllyCount <= 0)
        {
            Lose();
        }
        else if (EnemyCount <= 0)
        {
            Win();
        }
    }

    private void Win()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }

    private void Lose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }
}
