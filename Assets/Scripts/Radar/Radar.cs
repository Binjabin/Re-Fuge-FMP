using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] GameObject radarPing;
    [SerializeField] Transform sweepTransform;
    [SerializeField] float rotationSpeed;
    [SerializeField] float radarDistance;
    private List<Collider> colliderList;
    [SerializeField] LayerMask layers;
    [SerializeField] Transform radarCamera;
    List<Collider> pingedColliders = new List<Collider>();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float previousRotation = (transform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0f, (rotationSpeed * Time.deltaTime), 0f);
        float currentRotation = (transform.eulerAngles.y % 360) - 180;

        if(previousRotation < 0 && currentRotation >= 0)
        {
            pingedColliders.Clear();
        }

        RaycastHit ray;
        Physics.Raycast(sweepTransform.position, sweepTransform.forward, out ray, radarDistance, layers);
        Debug.DrawRay(transform.position, sweepTransform.forward * radarDistance, Color.green);

        if (ray.collider != null)
        {
            //Instantiate(ping, ray.point, Quaternion.identity);
            //Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);

            if(!pingedColliders.Contains(ray.collider))
            {
                pingedColliders.Add(ray.collider);
                GameObject newPing = Instantiate(radarPing, ray.collider.transform.position, Quaternion.Euler(90, 0, 0));
                newPing.transform.parent = radarCamera.transform;
            }
        }
    }
}
