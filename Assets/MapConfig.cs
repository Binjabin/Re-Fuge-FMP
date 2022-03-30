using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public float mapWidth;
    public int GridWidth => Mathf.Max(startGridWidth, endGridWidth);
    public int startGridWidth;
    public int endGridWidth;
    public float randomPosition;
    public List<LayerConfig> layers; 
}
