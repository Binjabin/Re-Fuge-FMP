using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public float mapWidth;
    public int maxGridWidth;
    public int GridWidth => Mathf.Max(startGridWidth, endGridWidth, maxGridWidth);
    public int startGridWidth;
    public int endGridWidth;
    public float randomPosition;
    public float lineOffset;
    public List<LayerConfig> layers; 
}
