using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour // Grid Manager file done by Microsoft Copilot
{
    public Grid grid; // stores the grid in scene

    private Dictionary<Vector3Int, GameObject> occupied = new(); // hexes with a unit gameobject are occupied

    private void Awake()
    {
        if (grid == null) // if a grid in scene, get it and store it
            grid = GetComponent<Grid>();
    }

    public bool IsOccupied(Vector3Int cell) // checks if a cell has a unit in its dictionary
    {
        return occupied.ContainsKey(cell); // true if occupied, false if not
    }

    public void RegisterUnit(GameObject unit) // adds unit to cell and unit to dictionary as key value
    {
        Vector3Int cell = grid.WorldToCell(unit.transform.position); // get cell of unit
        if (!occupied.ContainsKey(cell)) // if cell not occupied
            occupied.Add(cell, unit); // add the pair to dictionary
    }

    public void UnregisterUnit(GameObject unit) // removes the cell unit pair from dictionary
    {
        Vector3Int cell = grid.WorldToCell(unit.transform.position); // get cell of unit
        if (occupied.TryGetValue(cell, out GameObject u) && u == unit) // if cell and unit match
            occupied.Remove(cell); // remove the pair from dictionary
    }

    public void UpdateUnitPosition(GameObject unit, Vector3Int oldCell, Vector3Int newCell) // update cell postion of unit in dic
    {
        if (occupied.TryGetValue(oldCell, out GameObject u) && u == unit) // if cell moving from and unit position match
            occupied.Remove(oldCell); // unregister old cell

        if (!occupied.ContainsKey(newCell)) // if the cell moving to is not occupied
            occupied.Add(newCell, unit); // update dcitionary with updated pair
    }

    public Vector3Int WorldToCell(Vector3 worldPos) // grabs a world position and returns the hex it corresponds to
    {
        return grid.WorldToCell(worldPos);
    }

    public Vector3 CellToWorldCenter(Vector3Int cell) // grabs a cell and returns the world position of its center
    {
        return grid.GetCellCenterWorld(cell);
    }
}