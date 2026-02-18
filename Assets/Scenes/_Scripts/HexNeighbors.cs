using UnityEngine;

public static class HexNeighbors
{
    private static readonly Vector3Int[] evenRow = new Vector3Int[]
    {
        new(+1, 0, 0),
        new(0, -1, 0),
        new(-1, -1, 0),
        new(-1, 0, 0),
        new(-1, +1, 0),
        new(0, +1, 0)
    };

    private static readonly Vector3Int[] oddRow = new Vector3Int[]
    {
        new(+1, 0, 0),
        new(+1, -1, 0),
        new(0, -1, 0),
        new(-1, 0, 0),
        new(0, +1, 0),
        new(+1, +1, 0)
    };

    public static Vector3Int[] Get(Vector3Int cell)
    {
        return (cell.y % 2 == 0) ? evenRow : oddRow;
    }
}