using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinder : Pathfinder
{
    public AStarPathfinder(HexagonTabletop tabletop) : base(tabletop) {}

    public override Stack<HexagonCell> FindPath(HexagonCell start, HexagonCell objective)
    {
        List<HexagonCell> openList = new() { start };
        List<HexagonCell> closedList = new();

        Debug.Log("Starting pathfinding from " + start + " to " + objective);

        while (openList.Any())
        {
            // Find the cell with the lowest F score
            HexagonCell current = openList.OrderBy(cell => cell.F).ThenBy(cell => cell.H).First();
            // Debug.Log("Current node: " + current );

            openList.Remove(current);
            closedList.Add(current);

            // If we reached the objective, reconstruct the path
            if (current == objective)
            {
                HexagonCell currentCell = objective;
                Stack<HexagonCell> path = new();

                // Debug.Log("Reached objective, starting path reconstruction.");
                while (currentCell != start)
                {
                    // Debug.Log("Add " + currentCell + " to path.");
                    path.Push(currentCell);
                    currentCell = currentCell.Connection;
                }

                // Add the start cell to complete the path
                path.Push(start);
                // Debug.Log("Path found, returning path.");
                return path;
            }

            // Check all neighbors
            foreach (HexagonCell neighbor in current.Neighbors.Where(t => t.Walkable && !closedList.Contains(t)))
            {
                float costToNeighbor = current.G + current.GetDistance(neighbor);
                // Debug.Log("Evaluating neighbor " + neighbor + " with G: " + costToNeighbor);

                // If neighbor is not in the open list or has a better G cost
                if (!openList.Contains(neighbor) || costToNeighbor < neighbor.G)
                {
                    // Debug.Log("Found better G for " + neighbor + " now: " + costToNeighbor + ", setting connection between " + neighbor + " and " + current);

                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);
                    neighbor.SetH(neighbor.GetDistance(objective));
                    neighbor.SetF(neighbor.G + neighbor.H);

                    // Debug.Log("Updated neighbor " + neighbor );

                    // If neighbor is not in the open list, add it
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        // Debug.Log("Added " + neighbor + " to open list.");
                    }
                }
                else
                {
                    // Debug.Log("Neighbor " + neighbor + " already in open list with a higher or equal G, skipping.");
                }
            }

            // Debug.Log("End of current iteration, open list contains " + openList.Count + " nodes.");
        }

        Debug.Log("No path found.");
        return null; // No path found
    }
}
