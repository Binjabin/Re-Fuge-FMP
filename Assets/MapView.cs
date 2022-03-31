using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapView : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject linePrefab;
    int linePointsCount = 10;
    public List<MapNode> mapNodes = new List<MapNode>();
    public List<Node> nodesList = new List<Node>();
    [SerializeField] MapConfig testConfig;




    GameObject firstParent;

    void Start()
    {
        nodesList = MapGenerator.GenerateMap(testConfig).nodes;
        DrawMap();
    }

    MapNode CreateMapNode(Node node)
    {
        var mapNodeObject = Instantiate(nodePrefab, firstParent.transform);
        var mapNode = mapNodeObject.GetComponent<MapNode>();
        mapNode.transform.localPosition = node.position;
        mapNode.SetUp(node);
        return mapNode;
    }

    void DrawMap()
    {
        CreateMapParent();
        DrawNodes(nodesList);
        DrawLines();
    }

    void DrawNodes(IEnumerable<Node> nodes)
    {
        foreach(var node in nodes)
        {
            var mapNode = CreateMapNode(node);
            mapNodes.Add(mapNode);
        }
    }

    void DrawLines()
    {
        foreach(var node in mapNodes)
        {
            foreach(var connection in node.Node.outgoing)
            {
                AddLineConnection(node, mapNodes.FirstOrDefault(n => n.Node.point.Equals(connection)));
            }
        }
    }
    void AddLineConnection(MapNode from, MapNode to)
    {
        var lineObject = Instantiate(linePrefab, firstParent.transform);
        var lineRenderer = lineObject.GetComponent<LineRenderer>();
        var fromPoint = from.transform.position + (to.transform.position - from.transform.position).normalized * testConfig.lineOffset;
        var toPoint = to.transform.position + (from.transform.position - to.transform.position).normalized * testConfig.lineOffset;
        lineRenderer.positionCount = linePointsCount;
        for (var i = 0; i < linePointsCount; i++)
        {
            lineRenderer.SetPosition(i, Vector3.Lerp(toPoint, fromPoint, (float)i / (linePointsCount - 1)));
        }
    }
    void CreateMapParent()
    {
        firstParent = new GameObject("MapParent");
    }
}
