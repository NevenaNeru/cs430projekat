using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public static List<Node> AStar(Grid graph, Vector2 startPos, Vector2 endPos)
    {
        Dictionary<Node, Node> dic = new Dictionary<Node, Node>();
        Dictionary<Node, int> visited = new Dictionary<Node, int>();

        List<Node> path = new List<Node>();
        SimplePriorityQueue<Node> front = new SimplePriorityQueue<Node>();

        Node startNode = new Node(startPos.x, startPos.y);
        Node endNode = new Node(endPos.x, endPos.y);

        front.Enqueue(startNode, 0);
        dic.Add(startNode, startNode);
        visited.Add(startNode, 0);

        Node currentNode = new Node(0, 0);

        while (front.Count > 0)
        {

            currentNode = front.Dequeue();
            if (currentNode.pos == endNode.pos)
            {
                break;
            }
            else
            {
                foreach (Node currentNeighbour in graph.Neighbours(currentNode))
                {
                    int currentCost = visited[currentNode] + graph.nodeCost(currentNeighbour);
                    if (!visited.ContainsKey(currentNeighbour) || currentCost < visited[currentNeighbour])
                    {
                        visited[currentNeighbour] = currentCost;
                        dic[currentNeighbour] = currentNode;
                        front.Enqueue(currentNeighbour, currentCost + Heuristic(currentNode, endNode));
                        currentNeighbour.cost = currentCost;
                    }
                }
            }
        }

        while (currentNode.pos != startNode.pos)
        {
            path.Add(currentNode);
            currentNode = dic[currentNode];
        }
        path.Reverse();
        return path;
    }

    public static float Heuristic(Node a, Node b)
    {
        return Mathf.Abs(a.pos.x - b.pos.x) + Mathf.Abs(a.pos.y - b.pos.y);
    }
}
