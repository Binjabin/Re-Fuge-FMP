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
    private const float MaxClickDuration = 0.5f;
    float mouseDownTime;

    public void SetUp(Node n)
    {
        Node = n; 
        sr = GetComponent<MeshRenderer>();
        
        float randomScale = Random.Range(0.5f, 1.5f);
        transform.localScale = transform.localScale * randomScale;
    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {
    }

    private void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }

    private void OnMouseUp()
    {
        if (Time.time - mouseDownTime < MaxClickDuration)
        {
            // user clicked on this node:
            Debug.Log("clicked");
            FindObjectOfType<MapPlayerTracker>().SelectNode(this);
        }
    }

    public void SetState(NodeStates state)
    {
        switch (state)
        {
            
            case NodeStates.Locked:
                //sr.material.SetColor("_Color", FindObjectOfType<MapView>().lockedColor);
                break;
            case NodeStates.Visited:
                //sr.material.SetColor("_Color", FindObjectOfType<MapView>().visitedColor);
                break;
            case NodeStates.Attainable:
                // start pulsating from visited to locked color:
                //sr.material.SetColor("_Color", FindObjectOfType<MapView>().attainableColor);
                break;
        }
    }

}
