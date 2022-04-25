using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float thrustSpeed;
    Rigidbody rb;
    Vector3 targetDirection;
    [SerializeField] float idealRange;
    [SerializeField] float speedLimit;
    [SerializeField] float comfortableSpeed;
    bool breaking;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform player = FindObjectOfType<PlayerMovement>().transform;
        FaceTarget(player);
        if (Vector3.Dot(transform.forward, targetDirection) > 0.75f)
        {
            if (targetDirection.magnitude < idealRange)
            {
                breaking = true;
            }
            else
            {
                ThrustForwards();
            }

        }
        if (rb.velocity.magnitude > speedLimit)
        {
            breaking = true;
        }
        if (breaking)
        {
            rb.drag = 0.5f;
            if (rb.velocity.magnitude < comfortableSpeed)
            {

                breaking = false;
            }
        }
        else
        {
            rb.drag = 0.1f;
        }

    }
    void FaceTarget(Transform target)
    {
        targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ThrustForwards()
    {
        rb.AddForce(transform.forward * thrustSpeed);
    }
}
