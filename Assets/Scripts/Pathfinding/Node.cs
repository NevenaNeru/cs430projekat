using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node : IComparer<Node>
{
    public Vector2 pos;
    public int cost;
    public List<Node> prevNodes;

    public Node(float x, float y)
    {
        pos = new Vector2(x, y);
    }

    public int Compare(Node x, Node y)
    {
        if (x.cost < y.cost)
        {
            return -1;
        }
        else if (x.cost > y.cost)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
