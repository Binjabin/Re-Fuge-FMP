using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryMan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rotationSpeed;
    [SerializeField] float viewDistance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        if (player != null)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                FaceTarget(player.transform.position);
            }
            
        }
        
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        targetDirection.y = 0f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
