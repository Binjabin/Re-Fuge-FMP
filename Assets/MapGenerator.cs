using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        currentY = 0f;
        GenerateMap(testConfig);

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
    }
    void GenerateLayer(int layerIndex)
    {
        layer = config.layers[layerIndex];
        int layerNodeCount = Random.Range(layer.minNodeCount, layer.maxNodeCount);
        currentY += layer.yDistance;
        var layerNodes = new List<Node>();
        for(var i = 1;i <= layerNodeCount; i++)
        {
            distanceFromPreviousLayer = 1f;
            xDistance = config.mapWidth / (layerNodeCount + 1) * i;
            float baseXPosition = -(config.mapWidth/2) + xDistance;

            float baseYPosition = currentY;

            float randomizedX = RandomizeNodeXPosition(baseXPosition);
            float randomizedY = RandomizeNodeYPosition(baseYPosition);
            var node = new Node(new NodePoint(i, layerIndex));
            layerNodes.Add(node);
            Vector3 nodePosition = new Vector3(randomizedX, randomizedY, 0f);
            Instantiate(mapNode, nodePosition, Quaternion.identity);
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
        Path();
    }
    List<NodePoint> Path()
    {
        return null;
    }
}

