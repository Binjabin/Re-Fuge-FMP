using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LayerConfig
{
    public float yDistance;
    public float yRandomFactor;
    public float xRandomFactor;
    public List<NodeWeight> weights;

    public NodeBlueprint GetNodeBlueprint()
    {
        float maxWeight = 0f;
        for(var i = 0; i < weights.Count; i++)
        {
            maxWeight += weights[i].weight;
        }
        float randomValue = Random.Range(0f, maxWeight);

        int index = 0;
        int lastIndex = weights.Count - 1;
        float weightCap = 0;
        while (index < lastIndex)
        {
            weightCap += weights[index].weight;
            if (randomValue < weightCap)
            {
                return weights[index].blueprint;
            }
            index++;
        }
    
        // No other item was selected, so return very last index.
        return weights[index].blueprint;
    }
}
