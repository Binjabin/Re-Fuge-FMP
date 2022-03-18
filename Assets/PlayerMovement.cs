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

    bool thrusting;
    bool breaking;
    bool boosting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
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
        if(boosting)
        {
            rb.AddForce(transform.right * thrustSpeed * 10);
        }

        if (turnDirection != 0f)
        {
            rb.AddTorque(transform.up * rotationSpeed * -turnDirection);
        }
        
    }

}
