using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator
{
    
    static MapConfig config;
    static LayerConfig layer;
    //static float distanceFromPreviousLayer;
    static float xDistance;
    static List<List<NodePoint>> paths = new List<List<NodePoint>>();
    static public List<List<Node>> nodes = new List<List<Node>>();
    static public List<float> layerDistances;
    public static Map GetMap(MapConfig conf)
    {
        config = conf;
        nodes.Clear();
        GenerateLayerDistances();
        for(var i = 0;i < config.layers.Count; i++)
        {
            GenerateLayer(i);
        }
        GeneratePaths();
        RandomiseNodePositions();
        SetupConnections();
        RemoveCrossPaths();
        var nodesList = nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();
        return new Map(nodesList, new List<NodePoint>());
    }
    static void GenerateLayer(int layerIndex)
    {
        layer = config.layers[layerIndex];
        var layerNodes = new List<Node>();
        for(var i = 1;i <= config.GridWidth; i++)
        {
            //distanceFromPreviousLayer = 1f;
            xDistance = config.mapWidth / (config.GridWidth + 1);
            float baseXPosition = -(config.mapWidth/2) + xDistance * i;


            float baseX = baseXPosition;
            float baseY = GetDistanceToLayer(layerIndex);
            var nodeBlueprint = layer.GetNodeBlueprint();
            var nodeType = nodeBlueprint.type;
            var blueprintName = nodeBlueprint.name;
            var newAsteroidWeights = GetAsteroidWeights();
            var node = new Node(nodeType, blueprintName, new NodePoint(i, layerIndex))
            {
                position = new Vector3(baseX, 0f, baseY),
                asteroidWeights = newAsteroidWeights,
                seed = Random.Range(0, 100000)
            };
            layerNodes.Add(node);
        }
        nodes.Add(layerNodes);

        
    }

    private static float GetDistanceToLayer(int layerIndex)
    {
        if (layerIndex < 0 || layerIndex > layerDistances.Count) return 0f;

        return layerDistances.Take(layerIndex + 1).Sum();
    }

    static void GeneratePaths()
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
            var path = Path(point, 1, config.GridWidth);
            path.Insert(0, finalNode);
            path.Add(new NodePoint(finalNode.x, 0));
            paths.Add(path);
            attempts++;
        }
        while(!PathsLeadToAtLeastNDifferentPoints(paths, config.startGridWidth) && attempts < 100)
        {
            var randomPreBossPoint = preBossPoints[UnityEngine.Random.Range(0, preBossPoints.Count)];
            var path = Path(randomPreBossPoint, 1, config.GridWidth);
            path.Insert(0, finalNode);
            path.Add(new NodePoint(finalNode.x, 0));
            paths.Add(path);
            attempts++;
        }
        Debug.Log("Attempts to generate paths: " + attempts);
    }

    static List<NodePoint> Path(NodePoint from, float toY, int width)
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

    static NodePoint GetFinalNode()
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

    

    

    static void SetupConnections()
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


    private static void RandomiseNodePositions()
    {
        Debug.Log(nodes.Count);
        for (int index = 0; index < nodes.Count; index++)
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

                node.position += new Vector3(x, 0, y);
            }
        }
    }

    static private Node GetNode(NodePoint p)
    {
        if (p.y >= nodes.Count) return null;
        if (p.x >= nodes[p.y].Count) return null;

        return nodes[p.y][p.x];
    }

    static void GenerateLayerDistances()
    {
        layerDistances = new List<float>();
        foreach(var layer in config.layers)
        {
            layerDistances.Add(layer.yDistance);
        }
    }

    static void RemoveCrossPaths()
    {
        for(var i = 0; i < config.GridWidth - 1; i++)
        {
            for(var j = 0; j < config.layers.Count; j++)
            {
                var node = GetNode(new NodePoint(i, j));
                if (node == null || node.HasNoConnections()) continue;
                var right = GetNode(new NodePoint(i + 1, j));
                if (right == null || right.HasNoConnections()) continue;
                var top = GetNode(new NodePoint(i, j + 1));
                if (top == null || top.HasNoConnections()) continue;
                var topRight = GetNode(new NodePoint(i + 1, j + 1));
                if (topRight == null || topRight.HasNoConnections()) continue;


                if (!node.outgoing.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outgoing.Any(element => element.Equals(top.point))) continue;

                    // Debug.Log("Found a cross node: " + node.point);

                    // we managed to find a cross node:
                    // 1) add direct connections:
                    node.AddOutgoing(top.point);
                    top.AddIncoming(node.point);

                    right.AddOutgoing(topRight.point);
                    topRight.AddIncoming(right.point);

                    var rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        // remove both cross connections:
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                    }
                    else
                    {
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
            }
        }
    }
    static List<AsteroidWeights> GetAsteroidWeights()
    {
        List<AsteroidWeights> asteroidWeights = new List<AsteroidWeights>();
        List<ItemType> itemLeft = new List<ItemType>();
        itemLeft.Add(ItemType.Food);
        itemLeft.Add(ItemType.Energy);
        itemLeft.Add(ItemType.Water);

        float probabilityLeft = 1f;
        int loopAmount = itemLeft.Count;
        for (int i = 0; i < loopAmount - 1; i++)
        {
            var thisItem = itemLeft[Random.Range(0, itemLeft.Count)];
            var thisWeight = Random.Range(0f, probabilityLeft);
            var newAsteroidWeights = new AsteroidWeights(thisItem, thisWeight);
            probabilityLeft -= thisWeight;
            itemLeft.Remove(thisItem);
            asteroidWeights.Add(newAsteroidWeights);
        }
        asteroidWeights.Add(new AsteroidWeights(itemLeft[0], probabilityLeft));
        return asteroidWeights;
    }
}
