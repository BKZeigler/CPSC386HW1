using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid grid; // Assign your Grid with Hex layout

    private Dictionary<Vector3Int, GameObject> occupied = new();

    private void Awake()
    {
        if (grid == null)
            grid = GetComponent<Grid>();
    }

    // -----------------------------
    // Occupancy
    // -----------------------------

    public bool IsOccupied(Vector3Int cell)
    {
        return occupied.ContainsKey(cell);
    }

    public void RegisterUnit(GameObject unit)
    {
        Vector3Int cell = grid.WorldToCell(unit.transform.position);
        if (!occupied.ContainsKey(cell))
            occupied.Add(cell, unit);
    }

    public void UnregisterUnit(GameObject unit)
    {
        Vector3Int cell = grid.WorldToCell(unit.transform.position);
        if (occupied.TryGetValue(cell, out GameObject u) && u == unit)
            occupied.Remove(cell);
    }

    public void UpdateUnitPosition(GameObject unit, Vector3Int oldCell, Vector3Int newCell)
    {
        if (occupied.TryGetValue(oldCell, out GameObject u) && u == unit)
            occupied.Remove(oldCell);

        if (!occupied.ContainsKey(newCell))
            occupied.Add(newCell, unit);
    }

    // -----------------------------
    // Position Conversion
    // -----------------------------

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return grid.WorldToCell(worldPos);
    }

    public Vector3 CellToWorldCenter(Vector3Int cell)
    {
        return grid.GetCellCenterWorld(cell);
    }
}