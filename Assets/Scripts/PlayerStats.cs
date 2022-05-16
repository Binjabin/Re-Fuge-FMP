using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static float energy;
    public static float food;
    public static float water;
    public static float health;
    public static float shield;

    public static bool init;
    public static List<GameObject> items;
    
    public static float levelPassed;

    public static void InitStats()
    {
        List<float> values = new List<float>();
        values.Add(Random.Range(20f, 30f));
        values.Add(Random.Range(40f, 60f));
        values.Add(Random.Range(80f, 100f));
        int index = Random.Range(0, values.Count);
        energy = values[index];
        values.Remove(values[index]);
        index = Random.Range(0, values.Count);
        water = values[index];
        values.Remove(values[index]);
        index = Random.Range(0, values.Count);
        food = values[index];
        values.Remove(values[index]);
    }
}
