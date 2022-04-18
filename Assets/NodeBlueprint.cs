using UnityEngine;
public enum NodeType
{
    Merchant,
    Danger,
    Boss

}


[CreateAssetMenu]
public class NodeBlueprint : ScriptableObject
{
    public NodeType type;
    public Sprite icon;
}
