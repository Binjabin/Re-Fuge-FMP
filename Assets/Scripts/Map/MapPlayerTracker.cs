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
    public bool enteringScene;
    // Start is called before the first frame update

    private void Start()
    {
        enteringScene = false;
    }
    public void SelectNode(MapNode mapNode)
    {
        if(mapManager.currentMap.path.Count == 0)
        {
            PlayerStats.init = true;
            Debug.Log("first node");
            if(mapNode.Node.point.y == 0)
            {
                SendPlayerToNode(mapNode);
                
            }
                
        }
        else
        {
            PlayerStats.init = false;
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
        if(enteringScene == false)
        {
            mapManager.currentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            currentNode = mapNode;
            FindObjectOfType<MapCamera>().DetermineZoom();
            StartCoroutine(EnterNode(mapNode));
        }
        
        
    }

    private IEnumerator EnterNode(MapNode node)
    {
        LevelToLoad.asteroidCount = Random.Range(node.blueprint.minAsteroidCount, node.blueprint.maxAsteroidCount);
        LevelToLoad.seed = Random.Range(0, 100000);
        LevelToLoad.containsMerchant = node.blueprint.containsMerchant;
        LevelToLoad.heavyEnemyCount = Random.Range(node.blueprint.minHeavyEnemyCount, node.blueprint.maxHeavyEnemyCount + 1);
        LevelToLoad.standardEnemyCount = Random.Range(node.blueprint.minLightEnemyCount, node.blueprint.maxLightEnemyCount + 1);
        LevelToLoad.asteroidWeights = node.blueprint.asteroidWeights;


        yield return new WaitForSeconds(1f);
        enteringScene = true;
        FindObjectOfType<SceneManagment>().LeaveScene();
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("Safe");
        switch (node.Node.nodeType)
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

                break;
            case NodeType.Mystery:
                Debug.Log("boss scene");
                break;
        }
        
    }
}
