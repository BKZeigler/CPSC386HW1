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
             GridManager grid = FindFirstObjectByType<GridManager>(); // grab manager of scene grid
             Vector3Int randomCell; // stores current random cell
             do
             {
                 int x = Random.Range(-6, 10); // -6 to 9 inclusive
                 int y = Random.Range(-4, 5); // -4 to 4 inclusive
                 randomCell = new Vector3Int(x, y, 0);
             } while (grid.IsOccupied(randomCell)); // keep trying until we find an unoccupied cell

             Vector3 spawnPosition = grid.CellToWorldCenter(randomCell); // get world postion of cell to spawn unit in
             GameObject newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity); //copilot autofill code line
             if (gameObject.tag == "Ally") // if spawner has the tag ally
             {
                newUnit.tag = "Ally"; // give unit ally tag
             }
             else if (gameObject.tag == "Enemy") // if spawner has the tag enemy
             {
                newUnit.tag = "Enemy"; // give unit enemy tag
             }

    }
}
