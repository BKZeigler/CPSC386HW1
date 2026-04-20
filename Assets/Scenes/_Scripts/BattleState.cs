using System.Collections.Generic;
using UnityEngine;

// Will check for win/loss conditions and call appropriate functions

public class BattleState : MonoBehaviour
{

    public int AllyCount; // stores how many allies are alive
    public int EnemyCount; // stores how many enemies are alive
    void Start()
    {
        //Time.timeScale = 0; // battle should start paused, so that player can drag units to the field
    }
    
    void Awake()
    {
        Time.timeScale = 0;
    }
    //void Update() // look for win/loss conditions every frame
    //{
        
    //}

    private void Win() // loads the win scnene
    {
        var allies = FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None);

        foreach (var ally in allies)
        {
            if (ally.tag == "Ally") // if unit is an ally
            {
                ally.skillPoints += 3;
            }
        }

        var levelInfo = FindFirstObjectByType<LevelInfo>();

        LevelProgress.Instance.LevelDefeated(levelInfo.GetLevelName());
        Debug.Log("Level " + levelInfo.GetLevelName() + " defeated!"); // for testing

        if (LevelProgress.Instance.IsLevelDefeated("LevelOne") && LevelProgress.Instance.IsLevelDefeated("LevelTwo") && LevelProgress.Instance.IsLevelDefeated("LevelThree")) // if all levels are defeated
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
            LevelProgress.Instance.ClearProgress(); // clear progress so you can play again
            PlayerPrefs.DeleteKey("Units"); // clear unit data
            return;
        }
        // call saving and loading script to always move to a between screen unless the cleared battle was the last
        UnityEngine.SceneManagement.SceneManager.LoadScene("Between");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        //Camera.main.orthographicSize = 7;
        //Time.timeScale = 0;
    }

    private void Lose() // loads the lose scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }

    public void CheckWinLoss()
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

    public void SaveAllAllies()
    {
        var units = new List<UnitSaveData>();

        foreach (var unit in FindObjectsByType<AutoBattlerUnit>(FindObjectsSortMode.None))
        {
            if (unit.team != UnitTeam.Ally) continue;

            units.Add(new UnitSaveData
            {
                prefabName = unit.prefabName,
                level = unit.level,
                skillPoints = unit.skillPoints,
                maxHealth = unit.maxHealth,
                damage = unit.damage,
                range = unit.range,
                thinkInterval = unit.thinkInterval
            });
    }

    //SaveManager.SaveUnits(units);
    }
}
