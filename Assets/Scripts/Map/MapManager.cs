using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;
    public Map getmap;
    public Map currentMap;

    private void Start()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mapsave";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            getmap = formatter.Deserialize(stream) as Map;
            Map map = getmap;
            stream.Close();
            view.DrawMap(map);

            //using this instead of .Contains()
            if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            {
             //payer has already reached the boss, generate a new map
            GenerateNewMap();
            }
            else
            {
                currentMap = map;
            //player has not reached the boss yet, load the current map
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
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mapsave";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, currentMap);
        stream.Close();
    }

    private void OnApplicationQuit()
    {
        SaveMap();
    }

}
