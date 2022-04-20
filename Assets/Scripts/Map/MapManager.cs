using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;

    public Map currentMap;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Map"))
        {
            Debug.Log(PlayerPrefs.GetString("Map"));
            var mapJson = PlayerPrefs.GetString("Map");
            var map = JsonConvert.DeserializeObject<Map>(mapJson);
            //Debug.Log(JsonConvert.DeserializeObject<Map>(mapJson));
            //view.DrawMap(map);
            // using this instead of .Contains()
            //if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            //{
                // payer has already reached the boss, generate a new map
                //GenerateNewMap();
            //}
            //else
            //{
            //    currentMap = map;
                // player has not reached the boss yet, load the current map
            //    view.DrawMap(map);
            //}
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
        var json = JsonConvert.SerializeObject(currentMap, new JsonSerializerSettings(){ Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        PlayerPrefs.SetString("Map", json);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveMap();
    }

}
