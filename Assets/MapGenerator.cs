using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject mapNode;
    float currentY;
    [SerializeField] MapConfig testConfig;
    MapConfig config;
    float distanceFromPreviousLayer;
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
    }
    void GenerateLayer(int layerIndex)
    {
        var layer = config.layers[layerIndex];
        int layerNodeCount = Random.Range(layer.minNodeCount, layer.maxNodeCount);

        for(var i = 1;i <= layerNodeCount; i++)
        {
            distanceFromPreviousLayer = Random.Range(layer.minDistanceFromPreviousLayer, layer.maxDistanceFromPreviousLayer);
            Debug.Log(distanceFromPreviousLayer);
            Vector3 nodePosition = new Vector3(i * 1f, currentY + distanceFromPreviousLayer, 0f);
            Instantiate(mapNode, nodePosition, Quaternion.identity);
        }
        currentY += layer.maxDistanceFromPreviousLayer;
    }
    void StartingPoint()
    {
        GenerateLayer(0);
    }
}

