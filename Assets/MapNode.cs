using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeStates
{
    Locked,
    Visited,
    Attainable
}


public class MapNode : MonoBehaviour
{
    public Node Node;
    public MeshRenderer sr;

    public void SetUp(Node n)
    {
        Node = n; 
        sr = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {
    }

    public void SetState(NodeStates state)
    {
        switch (state)
        {
            case NodeStates.Locked:
                sr.material.color = MapView.Instance.lockedColor;
                break;
            case NodeStates.Visited:
                sr.material.color = MapView.Instance.visitedColor;
                break;
            case NodeStates.Attainable:
                // start pulsating from visited to locked color:
                sr.material.color = MapView.Instance.visitedColor;
                break;
        }
    }

}
