using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapPlayerTracker : MonoBehaviour
{
    public MapManager mapManager;
    public MapView view;
    public MapNode currentNode;
    public List<MapNode> currentAttainableNodes;
    // Start is called before the first frame update
    public void SelectNode(MapNode mapNode)
    {
        if(mapManager.currentMap.path.Count == 0)
        {
            Debug.Log(mapNode.Node.point.y);
            if(mapNode.Node.point.y == 0)
            {
                SendPlayerToNode(mapNode);
            }
                
        }
        else
        {
            var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
            var currentNode = mapManager.currentMap.GetNode(currentPoint);

            if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
            {
                SendPlayerToNode(mapNode);
            }
        }
    }

    private void SendPlayerToNode(MapNode mapNode)
    {
        mapManager.currentMap.path.Add(mapNode.Node.point);
        view.SetAttainableNodes();
        view.SetLineColors();
        currentNode = mapNode;
        FindObjectOfType<MapCamera>().DetermineZoom();
        EnterNode(mapNode);
        
    }

    private void EnterNode(MapNode node)
    {
        
        switch(node.Node.blueprint.type)
        {
            case NodeType.Merchant:
                Debug.Log("merchant scene");
                break;
            case NodeType.Danger:
                Debug.Log("danger scene");
                break;
            case NodeType.Boss:
                Debug.Log("danger scene");
                break;
            case NodeType.Safe:
                Debug.Log("danger scene");
                break;
            case NodeType.Mystery:
                Debug.Log("boss scene");
                break;
        }
    }
}
