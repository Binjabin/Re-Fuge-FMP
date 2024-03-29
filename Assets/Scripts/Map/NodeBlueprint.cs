using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum NodeType
{
    Merchant,
    Danger,
    Boss,
    Mystery,
    Safe,
    Empty

}

[CreateAssetMenu]
public class NodeBlueprint : ScriptableObject
{
    public NodeType type;
    public Sprite icon;
    public int minAsteroidCount;
    public int maxAsteroidCount;
    public bool containsMerchant;
    public bool containsRefugee;
    public bool containsMysteroiusMan;
    public int minLightEnemyCount;
    public int maxLightEnemyCount;
    public int minHeavyEnemyCount;
    public int maxHeavyEnemyCount;
    
    

}
