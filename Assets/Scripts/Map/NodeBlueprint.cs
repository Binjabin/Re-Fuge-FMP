using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum NodeType
{
    Merchant,
    Danger,
    Boss,
    Mystery,
    Safe

}

[CreateAssetMenu]
public class NodeBlueprint : ScriptableObject
{
    public NodeType type;
    public Sprite icon;
    public int minAsteroidCount;
    public int maxAsteroidCount;
    public bool containsMerchant;

    public int minLightEnemyCount;
    public int maxLightEnemyCount;
    public int minHeavyEnemyCount;
    public int maxHeavyEnemyCount;
    
    

}
