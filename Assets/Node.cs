using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node
{
    public NodePoint point;
    public Vector2 position;
    public Node(NodePoint point)
    {
        this.point = point;
    }
}
