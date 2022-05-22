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
    public NodeBlueprint blueprint;
    public List<AsteroidWeights> asteroidWeights;
    float currentCostToTravel;

    public float currentMinFoodCost;
    public float currentMaxFoodCost;
    public float currentMinWaterCost;
    public float currentMaxWaterCost;

    public void SetUp(Node n, Color starColor, float starSize, NodeBlueprint inblueprint)
    {
        blueprint = inblueprint;
        Node = n;
        sr = GetComponentInChildren<MeshRenderer>();
        icon = GetComponentInChildren<SpriteRenderer>();
        icon.enabled = false;
        icon.sprite = blueprint.icon;

        asteroidWeights = n.asteroidWeights;
        //Calculate asteroid weights



        if (blueprint.type == NodeType.Boss)
        {
            starSize = FindObjectOfType<MapView>().maxStarSize * 2.5f;
            starColor = FindObjectOfType<MapView>().starColors.Evaluate(1f);
        }

        sr.transform.localScale = sr.transform.localScale * starSize;
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
        FindObjectOfType<InfoSliders>().currentSelectedNode = this;
    }

    private void OnMouseExit()
    {
        icon.enabled = false;
        FindObjectOfType<InfoSliders>().currentSelectedNode = null;
    }

    private void OnMouseDown()
    {
        mouseDownTime = Time.time;
        FindObjectOfType<InfoSliders>().currentSelectedNode = null;
    }

    private void OnMouseUp()
    {
        if (Time.time - mouseDownTime < MaxClickDuration)
        {
            // user clicked on this node:
            FindObjectOfType<MapPlayerTracker>().SelectNode(this);
        }
    }
    public void DetermineCost()
    {
        if (FindObjectOfType<MapPlayerTracker>().currentNode == null)
        {
            currentMinFoodCost = 0f;
            currentMaxFoodCost = 0f;
            Debug.Log("first node so no cost");
            currentMinWaterCost = 0f;
            currentMaxWaterCost = 0f;
        }
        else
        {
            float currentDistance = (FindObjectOfType<MapPlayerTracker>().currentNode.transform.position - transform.position).magnitude;
            var info = FindObjectOfType<InfoSliders>();
            currentMinFoodCost = info.foodPerDist * currentDistance * 0.7f * PlayerStats.resourceMultiplier;
            currentMaxFoodCost = info.foodPerDist * currentDistance * 1.3f * PlayerStats.resourceMultiplier;

            currentMinWaterCost = info.waterPerDist * currentDistance * 0.7f * PlayerStats.resourceMultiplier;
            currentMaxWaterCost = info.waterPerDist * currentDistance * 1.3f * PlayerStats.resourceMultiplier; 
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
