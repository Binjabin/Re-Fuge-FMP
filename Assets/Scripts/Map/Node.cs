using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public class Node
{
    public NodePoint point;
    public string blueprintName;
    public NodeType nodeType;
    public Vector3 position;
    public List<NodePoint> incoming = new List<NodePoint>();
    public List<NodePoint> outgoing = new List<NodePoint>();

    public Node(NodeType nodeType, string blueprintName, NodePoint point)
    {
        this.nodeType = nodeType;
        this.blueprintName = blueprintName;
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

    public void RemoveIncoming(NodePoint p)
    {
        incoming.RemoveAll(element => element.Equals(p));
    }

    public void RemoveOutgoing(NodePoint p)
    {
        outgoing.RemoveAll(element => element.Equals(p));
    }

    public bool HasNoConnections()
    {
        return incoming.Count == 0 && outgoing.Count == 0;
    }
}
