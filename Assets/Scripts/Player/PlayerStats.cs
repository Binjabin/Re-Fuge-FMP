using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
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
        values.Add(Random.Range(60f, 80f));
        values.Add(Random.Range(80f, 100f));
        int index = Random.Range(1, values.Count);
        energy = values[index];
        values.Remove(values[index]);
        index = Random.Range(0, values.Count);
        water = values[index];
        values.Remove(values[index]);
        index = Random.Range(0, values.Count);
        food = values[index];
        values.Remove(values[index]);
        items = new List<GameObject>();
        levelPassed = 0f;
        health = 100f;
        shield = 100f;
    }
    public static void SaveStats()
    {
        PlayerStatsJSON playerJSON = new PlayerStatsJSON();
        playerJSON.energy = energy;
        playerJSON.food = food;
        playerJSON.water = water;
        playerJSON.health = health;
        playerJSON.shield = shield;
        playerJSON.items = new List<ItemData>();
        foreach(GameObject item in items)
        {
            ItemData itemData = new ItemData();
            itemData.value = item.GetComponent<Item>().value;
            itemData.type = item.GetComponent<Item>().itemType;
            playerJSON.items.Add(itemData);
        }
        
        playerJSON.levelPassed = levelPassed;


        var json = JsonConvert.SerializeObject(playerJSON);
        PlayerPrefs.SetString("Player", json);
        PlayerPrefs.Save();
    }

    public static void LoadStats()
    {
        
        
        var json = PlayerPrefs.GetString("Player");
        Debug.Log(json);
        PlayerStatsJSON playerJSON = JsonConvert.DeserializeObject<PlayerStatsJSON>(json);

        energy = playerJSON.energy;
        food = playerJSON.food;
        water = playerJSON.water;
        health = playerJSON.health;
        shield = playerJSON.shield;
        if(playerJSON.items != null)
        {
            items = new List<GameObject>();
            foreach (ItemData data in playerJSON.items)
            {
                bool foundItem = false;
                int itemIndex = 0;
                List<GameObject> itemPrefabList = ItemPrefabs.prefabList;
                while (foundItem == false)
                {
                    GameObject item = itemPrefabList[itemIndex];
                    var itemScript = item.GetComponent<Item>();
                    if (itemScript.value == data.value && itemScript.itemType == data.type)
                    {
                        items.Add(item);
                        foundItem = true;
                    }
                    itemIndex++;
                }

            }
        }
            
        levelPassed = playerJSON.levelPassed;
    }
}
