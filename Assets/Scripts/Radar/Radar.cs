using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] GameObject radarPing;
    [SerializeField] Transform sweepTransform;
    [SerializeField] float rotationSpeed;
    [SerializeField] float radarDistance;
    
    [SerializeField] LayerMask layers;
    [SerializeField] Transform radarCamera;
    [SerializeField] List<Collider> pingedColliders = new List<Collider>();

    [SerializeField] Color importantPingColor;
    [SerializeField] Color enemyPingColor;
    [SerializeField] Color otherPingColor;
    [SerializeField] Color pickupPingColor;
    [SerializeField] List<Collider> permanantPingedColliders = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sweepTransform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        float previousRotation = (sweepTransform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0f, (rotationSpeed * Time.deltaTime), 0f);
        float currentRotation = (sweepTransform.eulerAngles.y % 360) - 180;

        if(previousRotation < 0 && currentRotation >= 0)
        {
            pingedColliders.Clear();
        }

        RaycastHit[] ray = Physics.RaycastAll(sweepTransform.position, sweepTransform.forward, radarDistance, layers); ;
        
        Debug.DrawRay(sweepTransform.position, sweepTransform.forward * radarDistance, Color.green);
        foreach(RaycastHit raycastHit in ray)
        {
            if (raycastHit.collider != null)
            {
                if(raycastHit.collider.gameObject.tag != "AdditionalCollider" && raycastHit.collider.gameObject.tag != "Player")
                {
                    if (!pingedColliders.Contains(raycastHit.collider))
                    {
                        pingedColliders.Add(raycastHit.collider);
                        
                        if (raycastHit.collider.gameObject.GetComponent<Asteroids>() != null)
                        {
                            GameObject newPing = Instantiate(radarPing, raycastHit.collider.transform.position, Quaternion.Euler(90, 0, 0));
                            newPing.GetComponent<RadarPing>().SetColor(otherPingColor);
                            newPing.GetComponent<RadarPing>().SetDisappearTimer(900f / rotationSpeed);
                            newPing.GetComponent<RadarPing>().alpha = false;
                            newPing.transform.localScale = new Vector3(20f, 20f, 20f);
                            newPing.transform.parent = raycastHit.collider.transform;
                        }
                        else if (raycastHit.collider.gameObject.GetComponent<EnemyMovement>() != null)
                        {
                            GameObject newPing = Instantiate(radarPing, raycastHit.collider.transform.position, Quaternion.Euler(90, 0, 0));
                            newPing.GetComponent<RadarPing>().SetColor(enemyPingColor);
                            newPing.GetComponent<RadarPing>().SetDisappearTimer(900f / rotationSpeed);
                            newPing.GetComponent<RadarPing>().alpha = false;
                            newPing.transform.localScale = new Vector3(25f, 25f, 25f);
                            newPing.transform.parent = raycastHit.collider.transform;
                        }
                        else if (raycastHit.collider.gameObject.GetComponent<PickUp>() != null)
                        {
                            GameObject newPing = Instantiate(radarPing, raycastHit.collider.transform.position, Quaternion.Euler(90, 0, 0));
                            newPing.GetComponent<RadarPing>().SetColor(pickupPingColor);
                            newPing.GetComponent<RadarPing>().SetDisappearTimer(900f / rotationSpeed);
                            newPing.GetComponent<RadarPing>().alpha = false;
                            newPing.transform.localScale = new Vector3(20f, 20f, 20f);
                            newPing.transform.parent = raycastHit.collider.transform;
                        }
                        else
                        {
                            GameObject newPing = Instantiate(radarPing, raycastHit.collider.transform.position, Quaternion.Euler(90, 0, 0));
                            newPing.GetComponent<RadarPing>().SetColor(importantPingColor);
                            newPing.GetComponent<RadarPing>().SetDisappearTimer(900f / rotationSpeed);
                            newPing.GetComponent<RadarPing>().alpha = false;
                            newPing.transform.localScale = new Vector3(30f, 30f, 30f);
                            newPing.transform.parent = raycastHit.collider.transform;
                            
                        }
                        
                        
                    }
                }
            }
        }
        
    }
}
