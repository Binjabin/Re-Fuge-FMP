using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 startPosition;
    Transform targetNode;
    MapPlayerTracker playerTracker;
    float minCameraSize = 3f;
    Bounds bounds;
    Vector3 targetCameraPosition;
    Vector3 velocity;
    float velocity2;
    [SerializeField] float zoomSmoothTime;
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
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref velocity, smoothTime);
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, Mathf.Max(DetermineZoom(), minCameraSize)/1.2f, ref velocity2, zoomSmoothTime);
        
        }



    }

    public float DetermineZoom()
    {
        List<MapNode> attainableNodes = FindObjectOfType<MapPlayerTracker>().currentAttainableNodes;
        bounds = new Bounds(targetCameraPosition, Vector3.zero);
        for(int i = 0; i < attainableNodes.Count; i++)
        {
            bounds.Encapsulate(attainableNodes[i].transform.position);
        }
        
        return Mathf.Max(bounds.size.x, bounds.size.z);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetCameraPosition, new Vector3(2*bounds.size.x, 0f, 2*bounds.size.z));
    }
}
