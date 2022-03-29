using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class Node
{
    public NodePoint point;
    public Vector2 position;
    public readonly List<NodePoint> incoming = new List<NodePoint>();
    public readonly List<NodePoint> outgoing = new List<NodePoint>();
    public Node(NodePoint point)
    {
        this.point = point;
    }
    public void AddIncoming(NodePoint p)
    {
        if(incoming.Any(element => element.Equals(p)))
        {
            return;
        }
        incoming.Add(p);
    }
    public void AddOutgoing(NodePoint p)
    {
        if(outgoing.Any(element => element.Equals(p)))
        {
            return;
        }
        outgoing.Add(p);
    }
}
