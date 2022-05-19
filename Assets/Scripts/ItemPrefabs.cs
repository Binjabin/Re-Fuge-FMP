using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabs : MonoBehaviour
{
    public static ItemPrefabs instance;
    public List<GameObject> itemList;
    private void Awake()
    {
        instance = this;
    }
    public static List<GameObject> prefabList
    {
        get
        {
            return instance.itemList;
        }
    }
}
