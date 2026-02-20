using System.Collections.Generic;
using UnityEngine;

public class Pathfinder // A* pathfinding on hex grids created by https://www.redblobgames.com/grids/hexagons/ method
{                       // and Microsoft's Copilot implementation
    private GridManager grid; // stores grid manager in scene

    public Pathfinder(GridManager grid)
    {
        this.grid = grid; // constructor to set grid manager for pathfinding
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int goal) // A* pathfinding
    {
        var open = new PriorityQueue<Vector3Int>(); // hexes to explore
        var cameFrom = new Dictionary<Vector3Int, Vector3Int>(); // tracks paths
        var gScore = new Dictionary<Vector3Int, int>(); // cost from start to hex (how many from end and how many actions taken)

        open.Enqueue(start, 0); // starts with current hex
        gScore[start] = 0; // cost at start is 0

        while (open.Count > 0) // while there are hexes to explore
        {
            Vector3Int current = open.Dequeue(); // get the hex with lowest cost

            if (current == goal) // if at the goal, go reconstruct path
                return ReconstructPath(cameFrom, current);

            foreach (var offset in HexNeighbors.Get(current)) // for each of 6 neighbors given the odd/even row we are on
            {
                Vector3Int neighbor = current + offset; // try the neighbor hex

                if (grid.IsOccupied(neighbor) && neighbor != goal) // if occupied and not goal, have to do something else
                    continue;

                int tentative = gScore[current] + 1; // increase action cost by 1

                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor]) // if new path is cheaper
                {
                    gScore[neighbor] = tentative; // update cost to get to neighbor
                    int priority = tentative + HexDistance(current, goal); // final cost is action cost + distance to goal
                    open.Enqueue(neighbor, priority); // add neighbor and cost to explored
                    cameFrom[neighbor] = current; // the neighbor we will have just explored in current hex
                }
            }
        }

        return null;
    }

    private int HexDistance(Vector3Int a, Vector3Int b) // takes the hex distance between two points in A* to calculate distance
    {
        return Mathf.Abs(a.x - b.x) // x distance
             + Mathf.Abs(a.y - b.y); // y distance
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current) // best path found
    {
        List<Vector3Int> path = new() { current }; // start with goal hex (current)

        while (cameFrom.ContainsKey(current)) // when hex has a previous hex, not same as one we started on
        {
            current = cameFrom[current]; // move back to previous hex and update
            path.Add(current); // add it our best found path
        }

        path.Reverse(); // reverse it so when we feed it to unit, it will go from current to goal instead of goal to current
        return path; // the path to be fed
    }
}