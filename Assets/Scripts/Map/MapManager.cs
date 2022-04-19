using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;

    public Map currentMap;

    private void Start() 
    {
        GenerateNewMap();
    }

    public void GenerateNewMap()
    {
        var map = MapGenerator.GetMap(config);
        currentMap = map;
        view.DrawMap(map);
    }
}
