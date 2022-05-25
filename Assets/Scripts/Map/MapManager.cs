using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;


public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;
    public Map currentMap;
    [SerializeField] Map map;

    
    private void Start()
    {

        if (PlayerPrefs.HasKey("Map") && PlayerPrefs.HasKey("Player"))
        {
            PlayerStats.LoadStats();
            if(PlayerStats.isDead)
            {
                GenerateNewMap();
                PlayerStats.InitStats();
                Debug.Log("player died");
            }
            else
            {
                var mapJson = PlayerPrefs.GetString("Map");
                map = JsonConvert.DeserializeObject<Map>(mapJson);
                // using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
                {
                    // player has already reached the boss, generate a new map
                    GenerateNewMap();
                    PlayerStats.InitStats();
                }
                else
                {

                    currentMap = map;
                    // player has not reached the boss yet, load the current map

                    if (PlayerStats.levelPassed < map.path.Count)
                    {
                        if (map.path.Count > 0f)
                        {
                            map.path.RemoveAt(map.path.Count - 1);
                        }
                    }
                    view.DrawMap(map);
                    if (map.path.Count > 0f)
                    {
                        FindObjectOfType<MapCamera>().StartCamera();
                    }

                }
            }
            
        }
        else
        {
            GenerateNewMap();
            PlayerStats.InitStats();
            
        }
        Debug.Log(PlayerStats.resourceMultiplier);
        FindObjectOfType<MapDialogueTrigger>().CheckDialogue();
    }
    void Update()
    {
    }

    public void GenerateNewMap()
    {
        PlayerStats.InitStats();
        var map = MapGenerator.GetMap(config);
        currentMap = map;
        view.DrawMap(map);
    }
    
    public void SaveMap()
    {
        if (currentMap == null) return;

        var json = JsonConvert.SerializeObject(currentMap);
        PlayerPrefs.SetString("Map", json);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveMap();
        PlayerStats.SaveStats();
    }

}
