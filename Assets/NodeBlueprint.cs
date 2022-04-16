using UnityEngine;
public enum NodeType
{
    Merchant,
    Danger

}


[CreateAssetMenu]
public class NodeBlueprint : ScriptibleObject
{
    public NodeType type;
}
