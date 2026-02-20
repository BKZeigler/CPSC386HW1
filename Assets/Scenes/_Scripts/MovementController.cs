using UnityEngine;
using System.Collections;

public class UnitMovementController : MonoBehaviour // file created by Microsoft Copilot
{
    public float moveSpeed = 4f; // speed of unit movement

    private GridManager grid; // stores current grid manager in scene

    private void Start()
    {
        grid = FindFirstObjectByType<GridManager>(); // grab the grid manager in scene
    }

    public IEnumerator MoveToCell(Vector3Int targetCell) // the visual movement of the unit on the hex grid
    {
        Vector3 start = transform.position; // start position
        Vector3 end = grid.CellToWorldCenter(targetCell); // end position

        float t = 0f;
        while (t < 1f) // maximum time of cell to cell movement
        {
            t += Time.deltaTime * moveSpeed; // movespeed increase speed between cells
            transform.position = Vector3.Lerp(start, end, t); // lets unit move smoothly even if not on middle of hex
            yield return null;
        }

        transform.position = end; // ensure unit ends in center of hex
    }
}
