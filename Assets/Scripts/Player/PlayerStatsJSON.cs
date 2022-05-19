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

    public List<ItemData> items;

    public float levelPassed;

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
