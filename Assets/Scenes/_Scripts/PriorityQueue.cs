using System.Collections.Generic;

public class PriorityQueue<T> // file created by Microsoft Copilot, used in A* pathfinding
{
    private List<(T item, int priority)> elements = new List<(T, int)>(); // list of hex and their priority

    public int Count => elements.Count; // number of hexes in the queue

    public void Enqueue(T item, int priority) // add a hex and its priority to the queue
    {
        elements.Add((item, priority));
    }

    public T Dequeue() // remove and return the hex with the lowest priority (best path)
    {
        int bestIndex = 0; // start with first hex as best
        int bestPriority = elements[0].priority; // priority of first hex

        for (int i = 1; i < elements.Count; i++) // loop through hexes to find the one with the lowest cost
        {
            if (elements[i].priority < bestPriority) // if hex is lower cost
            {
                bestPriority = elements[i].priority; // update best cost
                bestIndex = i; // update best index
            }
        }

        T bestItem = elements[bestIndex].item; // get hex with lowest cost
        elements.RemoveAt(bestIndex); // remove it from queue
        return bestItem; // feed A* the hex with lowest cost
    }
}
