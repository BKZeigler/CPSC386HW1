using UnityEngine;

public static class HexNeighbors // created by Microsoft Copilot
{
    private static readonly Vector3Int[] evenRow = new Vector3Int[] // list of 6 neighbors for even rows in hex grid
    {
        new(+1, 0, 0),
        new(0, -1, 0),
        new(-1, -1, 0),
        new(-1, 0, 0),
        new(-1, +1, 0),
        new(0, +1, 0)
    };

    private static readonly Vector3Int[] oddRow = new Vector3Int[] // list of 6 neighbors for odd rows in hex grid
    {
        new(+1, 0, 0),
        new(+1, -1, 0),
        new(0, -1, 0),
        new(-1, 0, 0),
        new(0, +1, 0),
        new(+1, +1, 0)
    };

    public static Vector3Int[] Get(Vector3Int cell) // use the y value to determine if even or odd row
    {
        return (cell.y % 2 == 0) ? evenRow : oddRow; // height modulus 2 correponds to even or odd
    }
}