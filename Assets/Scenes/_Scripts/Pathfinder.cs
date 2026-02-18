using System.Collections.Generic;
using UnityEngine;

public class Pathfinder // A* pathfinding on hex grids created by https://www.redblobgames.com/grids/hexagons/ method
{                       // and Microsoft's Copilot implementation
    private GridManager grid;

    public Pathfinder(GridManager grid)
    {
        this.grid = grid;
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int goal)
    {
        var open = new PriorityQueue<Vector3Int>();
        var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        var gScore = new Dictionary<Vector3Int, int>();

        open.Enqueue(start, 0);
        gScore[start] = 0;

        while (open.Count > 0)
        {
            Vector3Int current = open.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, current);

            foreach (var offset in HexNeighbors.Get(current))
            {
                Vector3Int neighbor = current + offset;

                if (grid.IsOccupied(neighbor) && neighbor != goal)
                    continue;

                int tentative = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                {
                    gScore[neighbor] = tentative;
                    int priority = tentative + HexDistance(current, goal);
                    open.Enqueue(neighbor, priority);
                    cameFrom[neighbor] = current;
                }
            }
        }

        return null;
    }

    private int HexDistance(Vector3Int a, Vector3Int b) // takes the hex distance between two points in A* to calculate distance
    {
        return Mathf.Abs(a.x - b.x)
             + Mathf.Abs(a.y - b.y);
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3Int> path = new() { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}