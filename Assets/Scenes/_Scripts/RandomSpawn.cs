using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawn : MonoBehaviour
{

    public GameObject unitPrefab; // assign in inspector, will be the unit that spawns when button is pressed

    // grid size to search for unoccupied spaces (max x = 9, max y = 4.5, min x = -6, min y = -4.5)
    public void RandomSpawnUnit() //on button press, will spawn a unit appropriately based on tag of spawner
    {
        Debug.Log("Trying to spawn unit...");
            //select a random unoccupied cell within the grid and spawn the unit there
            //use GridManager's IsOccupied function to check if a cell is occupied
            //use GridManager's CellToWorldCenter function to get the world position of the cell center for spawning
            //then spawn an ally unit at that position
             GridManager grid = FindFirstObjectByType<GridManager>();
             Vector3Int randomCell;
             do
             {
                 int x = Random.Range(-6, 10); // -6 to 9 inclusive
                 int y = Random.Range(-4, 5); // -4 to 4 inclusive
                 randomCell = new Vector3Int(x, y, 0);
             } while (grid.IsOccupied(randomCell)); // keep trying until we find an unoccupied cell

             Vector3 spawnPosition = grid.CellToWorldCenter(randomCell);
             //create an autobattler unit at the spawn position
             GameObject newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity); //copilot autofill code line
             //then will spawn the unit based on the tag of the spawner (ally or enemy)
             if (gameObject.tag == "Ally")
             {
                // units have a UnitTeam variable that determines if they are ally or enemy, set that variable appropriately based on the tag of the spawner
                newUnit.tag = "Ally";
             }
             else if (gameObject.tag == "Enemy")
             {
                newUnit.tag = "Enemy";
             }

    }
}
