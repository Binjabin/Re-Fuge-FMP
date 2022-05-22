using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
[System.Serializable]
public class PlayerStatsJSON
{
    public float energy;
    public float food;
    public float water;
    public float health;
    public float shield;
    public bool hasID;
    public List<ItemData> items;
    public bool helpedRefugee;   
    public float levelPassed;
    public bool isDead;
    public float resourceMultiplier;
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
