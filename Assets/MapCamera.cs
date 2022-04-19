using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 startPosition;
    Transform targetNode;
    MapPlayerTracker playerTracker;
    
    Vector3 velocity;
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
            Vector3 targetCameraPosition = targetNodePosition;
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, smoothTime);
        }
    }
}
