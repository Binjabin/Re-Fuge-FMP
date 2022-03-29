using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject mapNode;
    float currentY;
    [SerializeField] MapConfig testConfig;
    MapConfig config;
    LayerConfig layer;
    float distanceFromPreviousLayer;
    float xDistance;
    public List<List<Node>> nodes = new List<List<Node>>();
    public List<Node> nodesList = new List<Node>();
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
        StartingPoint();
        for(var i = 1;i <= config.layers.Count - 1; i++)
        {
            GenerateLayer(i);
        }
        GeneratePaths();
        nodesList = nodes.SelectMany(x => x).ToList();
    }
    void GenerateLayer(int layerIndex)
    {
        layer = config.layers[layerIndex];
        currentY += layer.yDistance;
        var layerNodes = new List<Node>();
        for(var i = 1;i <= config.GridWidth; i++)
        {
            distanceFromPreviousLayer = 1f;
            xDistance = config.mapWidth / (config.GridWidth + 1) * i;
            float baseXPosition = -(config.mapWidth/2) + xDistance;

            float baseYPosition = currentY;

            float randomizedX = RandomizeNodeXPosition(baseXPosition);
            float randomizedY = RandomizeNodeYPosition(baseYPosition);
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

    float RandomizeNodeXPosition(float baseX)
    {
        float randomOffset = layer.xRandomFactor * xDistance;
        return baseX + Random.Range(-randomOffset, randomOffset);
    }

    float RandomizeNodeYPosition(float baseY)
    {
        float randomOffset = layer.yRandomFactor * layer.yDistance;
        return baseY + Random.Range(-randomOffset, randomOffset);
    }

    void GeneratePaths()
    {
        var finalNode = GetFinalNode();
        var paths = new List<List<NodePoint>>();
        var candidateXs = new List<int>();
        var attempts = 0;
        for (var i = 0; i < config.GridWidth; i++)
        {
            candidateXs.Add(i);
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
                if (lastPoint.x - 1 >= 0) candidateXs.Add(lastPoint.x - 1);
                // right
                if (lastPoint.x + 1 < width) candidateXs.Add(lastPoint.x + 1);
            }
            var nextPoint = new NodePoint(candidateXs[Random.Range(0, candidateXs.Count)], lastPoint.y + direction);
            path.Add(nextPoint);
        }
        return path;
    }

    NodePoint GetFinalNode()
    {
        var y = config.layers.Count - 1;
        return new NodePoint(1, y);
        
    }

    private static bool PathsLeadToAtLeastNDifferentPoints(IEnumerable<List<NodePoint>> paths, int n)
    {
        return (from path in paths select path[path.Count - 1].x).Distinct().Count() >= n;
    }

    void DrawLines()
    {

    }

    void DrawNodes(IEnumerable<Node> nodes)
    {
        foreach(Node node in nodes)
        {
            Instantiate(mapNode, node.position, Quaternion.identity);
        }
    }

    void DrawMap()
    {
        DrawNodes(nodesList);
        DrawLines();
    }
}
