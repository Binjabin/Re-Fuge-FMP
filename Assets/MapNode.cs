using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public Node Node;
    public SpriteRenderer sr;

    public void SetUp(Node n)
    {
        Node = n; 
    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {
    }

}
