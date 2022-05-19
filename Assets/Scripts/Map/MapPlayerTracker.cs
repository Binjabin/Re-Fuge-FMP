using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class MapPlayerTracker : MonoBehaviour
{
    public MapManager mapManager;
    public MapView view;
    public MapNode currentNode;
    public List<MapNode> currentAttainableNodes;
    public bool enteringScene;
    [SerializeField] AudioClip cannotGo;
    [SerializeField] AudioClip canGo;
    // Start is called before the first frame update

    private void Start()
    {
        enteringScene = false;
        
    }

    public void SelectNode(MapNode mapNode)
    {
        if(mapManager.currentMap.path.Count == 0)
        {
            PlayerStats.init = false;
            Debug.Log("first node");
            if(mapNode.Node.point.y == 0)
            {
                if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                {
                    SendPlayerToNode(mapNode);
                    GetComponent<AudioSource>().clip = canGo;
                    GetComponent<AudioSource>().Play();
                }


            }
            else
            {
                if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                {
                    GetComponent<AudioSource>().clip = cannotGo;
                    GetComponent<AudioSource>().Play();
                }
                    
            }
                
        }
        else
        {
            PlayerStats.init = false;
            var currentPoint = mapManager.currentMap.path[mapManager.currentMap.path.Count - 1];
            var currentNodePoint = mapManager.currentMap.GetNode(currentPoint);
            

            if (currentNodePoint != null && currentNodePoint.outgoing.Any(point => point.Equals(mapNode.Node.point)))
            {
                if(PlayerStats.food > mapNode.currentMinFoodCost && PlayerStats.water > mapNode.currentMinWaterCost)
                {
                    if(!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                    {
                        SendPlayerToNode(mapNode);
                        currentNode = mapNode;
                        GetComponent<AudioSource>().clip = canGo;
                        GetComponent<AudioSource>().Play();
                    }

                }
                else
                {
                    GetComponent<AudioSource>().clip = cannotGo;
                    GetComponent<AudioSource>().Play();
                }
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
        LevelToLoad.seed = node.Node.seed;
        LevelToLoad.containsMerchant = node.blueprint.containsMerchant;
        LevelToLoad.containsRefugee = node.blueprint.containsRefugee;
        LevelToLoad.containsMysteriousMan = node.blueprint.containsMysteroiusMan;

        LevelToLoad.heavyEnemyCount = Random.Range(node.blueprint.minHeavyEnemyCount, node.blueprint.maxHeavyEnemyCount + 1);
        LevelToLoad.standardEnemyCount = Random.Range(node.blueprint.minLightEnemyCount, node.blueprint.maxLightEnemyCount + 1);
        LevelToLoad.asteroidWeights = node.asteroidWeights;
        Debug.Log(PlayerStats.energy);
        PlayerStats.food -= Random.Range(node.currentMinFoodCost, node.currentMaxFoodCost);
        
        PlayerStats.water -= Random.Range(node.currentMinWaterCost, node.currentMaxWaterCost);
        PlayerStats.food = Mathf.Clamp(PlayerStats.food, 0f, 100f);
        PlayerStats.water = Mathf.Clamp(PlayerStats.water, 0f, 100f);


        yield return new WaitForSeconds(1f);
        enteringScene = true;
        FindObjectOfType<SceneManagment>().LeaveScene();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Safe");
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
