using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;

    public Node[,] matrix;
    public Vector2 start;
    public Vector2 end;
    public List<Vector2> walls;
    public List<Vector2> water;
    public List<Vector2> dirt;

    void createGrid()
    {
        matrix = new Node[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Node n = new Node(i, j);
                matrix[i, j] = n;
            }
        }
    }

    private void Awake()
    {
        createGrid();
    }

    private void OnDrawGizmosSelected()
    {
        createGrid();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(new Vector3(i, j, 0), new Vector3(.9f, .9f, .5f));

                foreach (Vector2 w in walls)
                {
                    if (matrix[i, j].pos == w)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(i, j, 0), new Vector3(.9f, .9f, .5f));
                        matrix[i, j].cost = 4;
                    }
                }

                foreach (Vector2 w in water)
                {
                    if (matrix[i, j].pos == w)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawCube(new Vector3(i, j, 0), new Vector3(.9f, .9f, .5f));
                    }
                }

                foreach (Vector2 d in dirt)
                {
                    if (matrix[i, j].pos == d)
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawCube(new Vector3(i, j, 0), new Vector3(.9f, .9f, .5f));
                    }
                }
            }
        }

            List<Node> AstarPath = Pathfinding.AStar(this, start, end);
            foreach (Node n in AstarPath)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawCube(new Vector3(n.pos.x, n.pos.y, 0), new Vector3(.9f, .9f, .5f));
            }


        if (inGrid(start))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(new Vector3(start.x, start.y, 0), new Vector3(.9f, .9f, .5f));
        }

        if (inGrid(end))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(end.x, end.y, 0), new Vector3(.9f, .9f, .5f));

        }

    }

    public bool inGrid(Vector2 vec)
    {

        if (vec.x >= 0 && vec.x < width && vec.y >= 0 && vec.y < height)
        {
            return true;
        }

        return false;
    }

    public int nodeCost(Node n)
    {

        if (water.Contains(n.pos))
        {
            return 2;
        }
        else if (dirt.Contains(n.pos))
        {
            return 3;
        }
        else if (walls.Contains(n.pos))
        {
            return 10;
        }
        else
        {
            return 1;
        }
    }

    public List<Vector2> randomPos(int size)
    {
        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < size; i++)
        {
            list.Add(new Vector2(Random.Range(0, width), Random.Range(0, height)));
        }
        return list;
    }

    public List<Node> Neighbours(Node node)
    {
        List<Node> list = new List<Node>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (matrix[i, j].pos == node.pos && nodeCost(matrix[i, j]) != 10)
                {

                    if (i > 0 && j > 0)
                    {
                        list.Add(matrix[i - 1, j - 1]);
                    }
                    if (j > 0)
                    {
                        list.Add(matrix[i, j - 1]);
                    }
                    if (j > 0 && i < height - 1)
                    {
                        list.Add(matrix[i + 1, j - 1]);
                    }
                    if (i > 0)
                    {
                        list.Add(matrix[i - 1, j]);
                    }

                    if (i < width - 1)
                    {
                        list.Add(matrix[i + 1, j]);
                    }

                    if (i > 0 && j < height - 1)
                    {
                        list.Add(matrix[i - 1, j + 1]);
                    }

                    if (j < height - 1)
                    {
                        list.Add(matrix[i, j + 1]);
                    }

                    if (i < width - 1 && j < height - 1)
                    {
                        list.Add(matrix[i + 1, j + 1]);
                    }

                }
            }
        }

        return list;
    }
}
