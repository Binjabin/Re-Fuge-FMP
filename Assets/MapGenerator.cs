using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;
    [SerializeField] GameObject linePrefab;
    float currentY;
    [SerializeField] MapConfig testConfig;
    MapConfig config;
    LayerConfig layer;
    float distanceFromPreviousLayer;
    float xDistance;
    List<List<NodePoint>> paths = new List<List<NodePoint>>();
    public List<List<Node>> nodes = new List<List<Node>>();
    public List<Node> nodesList = new List<Node>();
    public List<MapNode> mapNodes = new List<MapNode>();
    public List<float> layerDistances;
    int linePointsCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentY = 0f;
        GenerateMap(testConfig);
        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateMap(MapConfig conf)
    {
        config = conf;
        GenerateLayerDistances();
        for(var i = 0;i < config.layers.Count; i++)
        {
            GenerateLayer(i);
        }
        GeneratePaths();
        RandomiseNodePositions();
        SetupConnections();
        nodesList = nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();
    }
    void GenerateLayer(int layerIndex)
    {
        layer = config.layers[layerIndex];
        currentY += layer.yDistance;
        var layerNodes = new List<Node>();
        for(var i = 1;i <= config.GridWidth; i++)
        {
            distanceFromPreviousLayer = 1f;
            xDistance = config.mapWidth / (config.GridWidth + 1);
            float baseXPosition = -(config.mapWidth/2) + xDistance * i;

            float baseYPosition = currentY;

            float randomizedX = baseXPosition;
            float randomizedY = baseYPosition;
            var node = new Node(new NodePoint(i, layerIndex))
            {
                position = new Vector2(randomizedX, randomizedY)
            };
            layerNodes.Add(node);
        }
        nodes.Add(layerNodes);

        
    }

    void StartingPoint()
    {
        GenerateLayer(0);
    }

    void GeneratePaths()
    {
        var finalNode = GetFinalNode();
        paths = new List<List<NodePoint>>();
        var candidateXs = new List<int>();
        var attempts = 0;
        for (var i = 0; i < config.GridWidth; i++)
        {
            candidateXs.Add(i);
        }
        

        for (int i = 0; i < candidateXs.Count; i++) 
        {
            int temp = candidateXs[i];
            int randomIndex = Random.Range(i, candidateXs.Count);
            candidateXs[i] = candidateXs[randomIndex];
            candidateXs[randomIndex] = temp;
        }

        var prebossX = candidateXs.Take(config.endGridWidth);
        var preBossPoints = (from x in prebossX select new NodePoint(x, finalNode.y - 1)).ToList();
        foreach(var point in preBossPoints)
        {
            var path = Path(point, 0, config.GridWidth);
            path.Insert(0, finalNode);
            paths.Add(path);
            attempts++;
        }
        while(!PathsLeadToAtLeastNDifferentPoints(paths, config.startGridWidth) && attempts < 100)
        {
            var randomPreBossPoint = preBossPoints[UnityEngine.Random.Range(0, preBossPoints.Count)];
            var path = Path(randomPreBossPoint, 0, config.GridWidth);
            path.Insert(0, finalNode);
            paths.Add(path);
            attempts++;
        }
        Debug.Log("Attempts to generate paths: " + attempts);
    }

    List<NodePoint> Path(NodePoint from, float toY, int width)
    {
        if(from.y == toY)
        {
            return null;
        }
        var direction = from.y > toY ? -1 : 1;
        var path = new List<NodePoint>{from};
        while (path[path.Count - 1].y != toY)
        {
            var lastPoint = path[path.Count - 1];
            var candidateXs = new List<int>();
            if(false)
            {

            }
            else
            {
                candidateXs.Add(lastPoint.x);
                // left
                if (lastPoint.x - 1 >= 0)
                {
                    candidateXs.Add(lastPoint.x - 1);
                } 
                // right
                if (lastPoint.x + 1 < width)
                {
                    candidateXs.Add(lastPoint.x + 1);
                } 
            }
            var nextPoint = new NodePoint(candidateXs[Random.Range(0, candidateXs.Count)], lastPoint.y + direction);
            path.Add(nextPoint);
        }
        return path;
    }

    NodePoint GetFinalNode()
    {
        var y = config.layers.Count - 1;
        if (config.GridWidth % 2 == 1)
        {
            return new NodePoint(config.GridWidth / 2, y);
        }
            

        return Random.Range(0, 2) == 0 ? new NodePoint(config.GridWidth / 2, y) : new NodePoint(config.GridWidth / 2 - 1, y);

        
    }

    private static bool PathsLeadToAtLeastNDifferentPoints(IEnumerable<List<NodePoint>> paths, int n)
    {
        return (from path in paths select path[path.Count - 1].x).Distinct().Count() >= n;
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
        var lineObject = Instantiate(linePrefab, transform.position, Quaternion.identity);
        var lineRenderer = lineObject.GetComponent<LineRenderer>();
        var fromPoint = from.transform.position;
        var toPoint = to.transform.position;
        lineRenderer.positionCount = linePointsCount;
        for (var i = 0; i < linePointsCount; i++)
        {
            lineRenderer.SetPosition(i, Vector3.Lerp(toPoint, fromPoint, (float)i / (linePointsCount - 1)));
        }
    }

    void DrawNodes(IEnumerable<Node> nodes)
    {
        foreach(var node in nodes)
        {
            var mapNode = CreateMapNode(node);
            mapNodes.Add(mapNode);
        }
    }

    void SetupConnections()
    {
        foreach (var path in paths)
        {
            for (var i = 0; i < path.Count; i++)
            {
                var node = nodes[path[i].y][path[i].x];
                if(i > 0)
                {
                    var nextNode = nodes[path[i-1].y][path[i-1].x];
                    nextNode.AddIncoming(node.point);
                    node.AddOutgoing(nextNode.point);
                }
                if(i < path.Count - 1)
                {
                    var previousNode = nodes[path[i+1].y][path[i+1].x];
                    previousNode.AddOutgoing(node.point);
                    node.AddIncoming(previousNode.point);

                }
            }
        }
    }

    void DrawMap()
    {
        
        DrawNodes(nodesList);
        DrawLines();
    }
    MapNode CreateMapNode(Node node)
    {
        var mapNodeObject = Instantiate(nodePrefab, node.position, Quaternion.identity);
        var mapNode = mapNodeObject.GetComponent<MapNode>();
        mapNode.SetUp(node);
        return mapNode;
    }

    void RandomiseNodePositions()
    {
        for (var index = 0; index < nodes.Count; index++)
        {
            var list = nodes[index];
            var layer = config.layers[index];
            var distToNextLayer = index + 1 >= layerDistances.Count ? 0f : layerDistances[index + 1];
            
            var distToPreviousLayer = layerDistances[index];

            foreach (var node in list)
            {
                var xRnd = Random.Range(-1f, 1f);
                var yRnd = Random.Range(-1f, 1f);

                var x = xRnd * xDistance * config.randomPosition/ 2f;
                var y = yRnd < 0 ? distToPreviousLayer * yRnd * config.randomPosition / 2f : distToNextLayer * yRnd * config.randomPosition/ 2f;

                node.position += new Vector2(x, y);
            }
        }
    }
    void GenerateLayerDistances()
    {
        layerDistances = new List<float>();
        foreach(var layer in config.layers)
        {
            layerDistances.Add(layer.yDistance);
        }
    }

}
