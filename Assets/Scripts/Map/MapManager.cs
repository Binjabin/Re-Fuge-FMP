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

        if (PlayerPrefs.HasKey("Map"))
        {
            var mapJson = PlayerPrefs.GetString("Map");
            map = JsonConvert.DeserializeObject<Map>(mapJson);
            // using this instead of .Contains()
            if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            {
                // payer has already reached the boss, generate a new map
                GenerateNewMap();
            }
            else
            {
                currentMap = map;
                // player has not reached the boss yet, load the current map
                view.DrawMap(map);
            }
        }
        else
        {
            GenerateNewMap();
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNewMap();
        }
    }

    public void GenerateNewMap()
    {
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
    }

}
