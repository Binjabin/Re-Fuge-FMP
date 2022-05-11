using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AsteroidWeights
{
    public ItemType type;
    public float weight;

    public AsteroidWeights(ItemType intype, float inweight)
    {
        type = intype;
        weight = inweight;
    }
}
