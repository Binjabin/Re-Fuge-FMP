using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 startPosition;
    Transform targetNode;
    MapPlayerTracker playerTracker;
    [SerializeField] float minCameraSize = 3f;
    Bounds bounds;
    Vector3 targetCameraPosition;
    Vector3 velocity;
    float velocity2;
    [SerializeField] float zoomSmoothTime;
    float targetZoom;
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
            targetCameraPosition = targetNodePosition;

        }
        transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, smoothTime);
        if (!playerTracker.enteringScene)
        {
            targetZoom = Mathf.Max(DetermineZoom() / 1.6f, minCameraSize);
        }
        else
        {
            targetZoom = 0.1f;
        }
        
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref velocity2, zoomSmoothTime);


    }

    public float DetermineZoom()
    {
        List<MapNode> attainableNodes = FindObjectOfType<MapPlayerTracker>().currentAttainableNodes;
        float currentDistanceLimit = 0f;
        for (int i = 0; i < attainableNodes.Count; i++)
        {
            if (Vector3.Distance(attainableNodes[i].transform.position, targetCameraPosition) > currentDistanceLimit)
            {
                currentDistanceLimit = Vector3.Distance(attainableNodes[i].transform.position, targetCameraPosition);
            }
        }

        return currentDistanceLimit;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetCameraPosition, new Vector3(2 * bounds.size.x, 0f, 2 * bounds.size.z));
    }

    public void ResetCamera()
    {
        transform.position = startPosition;
        targetCameraPosition = startPosition;
        Camera.main.orthographicSize = minCameraSize;
    }

    public void StartCamera()
    {
        transform.position = playerTracker.currentNode.transform.position;
        targetCameraPosition = playerTracker.currentNode.transform.position;
        Camera.main.orthographicSize = minCameraSize;
    }
}
