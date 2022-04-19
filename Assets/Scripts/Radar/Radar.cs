using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] Transform player;
    [SerializeField] GameObject ping;
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
        transform.position = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + (rotationSpeed * Time.deltaTime), 0f);
        float currentRotation = (transform.eulerAngles.y % 360) - 180;

        if(previousRotation < 0 && currentRotation >= 0)
        {
            pingedColliders.Clear();
        }

        RaycastHit ray;
        Physics.Raycast(transform.position, transform.forward, out ray, rayDistance, layers);

        if (ray.collider != null)
        {
            //Instantiate(ping, ray.point, Quaternion.identity);
            //Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);

            if(!pingedColliders.Contains(ray.collider))
            {
                pingedColliders.Add(ray.collider);
                GameObject newPing = Instantiate(ping, ray.collider.transform.position, Quaternion.Euler(90, 0, 0));
                newPing.transform.parent = radarCamera.transform;
            }
        }
    }
}
