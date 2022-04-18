using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 startPosition;
    Transform targetNode;
    MapPlayerTracker playerTracker;
    private void Start() 
    {
        playerTracker = FindObjectOfType<MapPlayerTracker>();
    }
    // Update is called once per frame
    void Update()
    {

        if (playerTracker.currentNode != null)
        {
            Vector3 targetNodePosition = playerTracker.currentNode.transform.position;
            Vector3 targetCameraPosition = targetNodePosition + offset;
            transform.position = targetCameraPosition;
        }
    }
}
