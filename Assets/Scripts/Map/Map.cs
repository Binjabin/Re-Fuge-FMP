using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
[System.Serializable]
public class Map
{
    public List<Node> nodes;
    public List<NodePoint> path;
    public Map(List<Node> nodes, List<NodePoint> path) 
    {
        this.nodes = nodes;
        this.path = path;


    }
    public Node GetNode(NodePoint point)
    {
        return nodes.FirstOrDefault(n => n.point.Equals(point));
    }
    public Node GetBossNode()
    {
        return nodes.FirstOrDefault(n => n.blueprint.type == NodeType.Boss);
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings(){ Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
    }
}
