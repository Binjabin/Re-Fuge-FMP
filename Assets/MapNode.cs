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
    SpriteRenderer icon;
    public void SetUp(Node n, Color starColor, float starSize, NodeBlueprint blueprint)
    {
        
        Node = n; 
        sr = GetComponent<MeshRenderer>();
        icon = GetComponentInChildren<SpriteRenderer>();
        icon.enabled = false;
        icon.sprite = blueprint.icon;
        if(blueprint.type == NodeType.Boss)
        {
            starSize = FindObjectOfType<MapView>().maxStarSize * 1.5f;
        }
        transform.localScale = transform.localScale * starSize;
        Color cell;
        cell = starColor;
        cell.r += 0.5f;
        cell.b += 0.5f;
        cell = cell * 9f;

        sr.material.SetColor("_CellColor", cell);
        sr.material.SetColor("_Color", starColor);
    }

    private void OnMouseEnter()
    {
        icon.enabled = true;
    }

    private void OnMouseExit()
    {
        icon.enabled = false;
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
