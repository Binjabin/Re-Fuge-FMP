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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sweepTransform.position = transform.position;
        float previousRotation = (sweepTransform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0f, (rotationSpeed * Time.deltaTime), 0f);
        float currentRotation = (sweepTransform.eulerAngles.y % 360) - 180;

        if(previousRotation < 0 && currentRotation >= 0)
        {
            pingedColliders.Clear();
            Debug.Log("clear list");
        }

        RaycastHit[] ray = Physics.RaycastAll(sweepTransform.position, sweepTransform.forward, radarDistance, layers); ;
        
        Debug.DrawRay(transform.position, sweepTransform.forward * radarDistance, Color.green);
        foreach(RaycastHit raycastHit in ray)
        {
            if (raycastHit.collider != null)
            {
                if(raycastHit.collider.gameObject.tag != "Player")
                {
                    Debug.Log(raycastHit.collider.gameObject.tag);
                    if (!pingedColliders.Contains(raycastHit.collider))
                    {
                        pingedColliders.Add(raycastHit.collider);
                        GameObject newPing = Instantiate(radarPing, raycastHit.collider.transform.position, Quaternion.Euler(90, 0, 0));
                        if (raycastHit.collider.gameObject.GetComponent<Asteroids>() != null)
                        {
                            newPing.GetComponent<RadarPing>().SetColor(otherPingColor);
                        }
                        else if (raycastHit.collider.gameObject.GetComponent<EnemyMovement>() != null)
                        {
                            newPing.GetComponent<RadarPing>().SetColor(enemyPingColor);
                        }
                        else
                        {
                            newPing.GetComponent<RadarPing>().SetColor(importantPingColor);
                        }
                        newPing.GetComponent<RadarPing>().SetDisappearTimer(360f / rotationSpeed);
                        //newPing.transform.parent = radarCamera.transform;
                    }
                }

                
            }
        }
        
    }
}
