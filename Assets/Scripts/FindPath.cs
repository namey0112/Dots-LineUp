using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public int width, height;
    public Node[,] grid;

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        Node startNode = grid[start.x, start.y];
        Node endNode = grid[end.x, end.y];

        if (startNode.isBlocked || endNode.isBlocked)
        {
            Debug.Log("Start or End node is blocked.");
            return new List<Vector2Int>();
        }

        openSet.Add(startNode);
        ResetGrid();

        startNode.gCost = 0;
        startNode.hCost = GetDistance(startNode, endNode);

        while (openSet.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openSet);
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (neighbor.isBlocked || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newCostToNeighbor = currentNode.gCost + 1;
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        Debug.Log("No valid path found.");
        return new List<Vector2Int>();
    }

    private Node GetLowestFCostNode(List<Node> openSet)
    {
        Node lowestFCostNode = openSet[0];
        foreach (var node in openSet)
        {
            if (node.fCost < lowestFCostNode.fCost || (node.fCost == lowestFCostNode.fCost && node.hCost < lowestFCostNode.hCost))
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }

    private List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(new Vector2Int(currentNode.x, currentNode.y));
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int checkX = node.x + dx[i];
            int checkY = node.y + dy[i];

            if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height && !grid[checkX, checkY].isBlocked)
            {
                neighbors.Add(grid[checkX, checkY]);
            }
        }
        return neighbors;
    }

    private int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private void ResetGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].gCost = int.MaxValue;
                grid[x, y].hCost = 0;
                grid[x, y].parent = null;
            }
        }
    }
}

public class Node
{
    public int x, y;
    public bool isBlocked;
    public int gCost, hCost;
    public Node parent;
    public int fCost => gCost + hCost;

    public Node(int x, int y, bool isBlocked)
    {
        this.x = x;
        this.y = y;
        this.isBlocked = isBlocked;
        this.gCost = int.MaxValue;
        this.hCost = 0;
        this.parent = null;
    }
}