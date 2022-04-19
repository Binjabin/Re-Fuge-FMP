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
    [SerializeField] MapConfig testConfig;

    [SerializeField, GradientUsageAttribute(true)] public Gradient starColors;
    public float maxStarSize;
    public float minStarSize;

    //[ColorUsage(true, true)] public Color lockedColor;
    //[ColorUsage(true, true)] public Color attainableColor;
    //[ColorUsage(true, true)] public Color visitedColor;

    public Color lockedLineColor;
    public Color attainableLineColor;
    public Color visitedLineColor;

    GameObject firstParent;
    public static MapView Instance;
    public MapManager mapManager;

    private List<LineConnection> lineConnections = new List<LineConnection>();

    void Start()
    {
        
        Instance = this;
        Debug.Log(Instance);
    }

    MapNode CreateMapNode(Node node)
    {
        var mapNodeObject = Instantiate(nodePrefab, firstParent.transform);
        var mapNode = mapNodeObject.GetComponent<MapNode>();
        mapNode.transform.localPosition = node.position;
        float starSize = Random.Range(minStarSize, maxStarSize);
        Color starColor = starColors.Evaluate((starSize-minStarSize)/(maxStarSize-minStarSize));
        mapNode.SetUp(node, starColor, starSize, node.blueprint);
        return mapNode;
    }

    public void DrawMap(Map m)
    {
        CreateMapParent();
        DrawNodes(m.nodes);
        DrawLines();
        SetAttainableNodes();
        SetLineColors();
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
        lineConnections.Add(new LineConnection(lineRenderer, from, to));
    }
    void CreateMapParent()
    {
        firstParent = new GameObject("MapParent");
    }

    public void SetAttainableNodes()
    {
        foreach(var node in mapNodes)
        {
            node.SetState(NodeStates.Locked);
        }
        if(mapManager.currentMap.path.Count == 0)
        {
            foreach (var node in mapNodes.Where(n => n.Node.point.y == 0))
            {
                node.SetState(NodeStates.Attainable);
            }
        }
        else
        {
            foreach (var point in mapManager.currentMap.path)
            {
                var mapNode = GetNode(point);
                if (mapNode != null)
                {
                    mapNode.SetState(NodeStates.Visited);
                }
            }

            var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
            var currentNode = mapManager.currentMap.GetNode(currentPoint);

            foreach (var point in currentNode.outgoing)
            {
                var mapNode = GetNode(point);
                if(mapNode != null)
                {
                    mapNode.SetState(NodeStates.Attainable);
                }
            }
        }
    }

    public void SetLineColors()
    {
        foreach(var connection in lineConnections)
        {
            connection.SetColor(lockedLineColor);
        }

        if (mapManager.currentMap.path.Count == 0)
            return;

        var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
        var currentNode = mapManager.currentMap.GetNode(currentPoint);
        foreach (var point in currentNode.outgoing)
        {
            var lineConnection = lineConnections.FirstOrDefault(conn => conn.from.Node == currentNode && conn.to.Node.point.Equals(point));
            lineConnection.SetColor(attainableLineColor);
        }

        if (mapManager.currentMap.path.Count <= 1) return;

        for (var i = 0; i < mapManager.currentMap.path.Count - 1; i++)
        {
            var current = mapManager.currentMap.path[i];
            var next = mapManager.currentMap.path[i + 1];
            var lineConnection = lineConnections.FirstOrDefault(conn => conn.@from.Node.point.Equals(current) &&
                                                                        conn.to.Node.point.Equals(next));
            lineConnection?.SetColor(visitedLineColor);
        } 
    }

    private MapNode GetNode(NodePoint p)
    {
        return mapNodes.FirstOrDefault(n => n.Node.point.Equals(p));
    }
}