using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float turnDirection;
    Vector3 velocity, additionalForce, inputVector;
    Rigidbody rb;
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    float velocityref;
    bool thrusting;
    bool breaking;
    bool boosting;

    TrailRenderer[] trail;
    float[] standardTrailLength = { 0f, 0f, 0f };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        trail = GetComponentsInChildren<TrailRenderer>();
        for (int i = 0; i < trail.Length; i++)
        {
            standardTrailLength[i] = trail[i].time;
            trail[i].time = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

    }
    void FixedUpdate()
    {
        ProcessInput();
    }

    void GetInput()
    {
        thrusting = Input.GetKey(KeyCode.W);
        breaking = Input.GetKey(KeyCode.S);
        boosting = Input.GetKey(KeyCode.E);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }


        for (int i = 0; i < trail.Length; i++)
        {
            if (thrusting)
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, standardTrailLength[i], ref velocityref, 0.5f);
            }
            else
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, 0f, ref velocityref, 0.5f);
            }
        }
    }
    void ProcessInput()
    {
        if (thrusting)
        {
            rb.AddForce(transform.right * thrustSpeed);
        }
        if (breaking)
        {
            rb.drag = 3f;
        }
        else
        {
            rb.drag = 0.1f;
        }
        if (boosting)
        {
            rb.AddForce(transform.right * thrustSpeed * 10);
        }

        if (turnDirection != 0f)
        {
            rb.AddTorque(transform.up * rotationSpeed * -turnDirection);
        }

    }

}
