using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public List<Node> nodes;
    public List<NodePoint> path;
    public Map(List<Node> nodes, List<NodePoint> path) 
    {
        this.nodes = nodes;
        this.path = path;
    }
}
