using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xInput;
    float zInput;
    Vector3 velocity;
    Vector3 additionalForce;
    Vector3 inputVector;
    Rigidbody rb;
    [SerializeField] float playerEngineForce = 5f;
    [SerializeField] float breakForce = 5f;

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
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }
    void ProcessInput()
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        
        inputVector = new Vector3(xInput, 0f, zInput);
        var skewedInput = matrix.MultiplyPoint3x4(inputVector);
        Vector3 normalizedInputVector = Vector3.Normalize(skewedInput);
        additionalForce = normalizedInputVector * playerEngineForce * Time.deltaTime;
        
    }

    void FixedUpdate() 
    {
        velocity = rb.velocity;
        velocity += additionalForce;

        if(Input.GetKey(KeyCode.Space))
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, breakForce * Time.deltaTime);
		    velocity.z = Mathf.MoveTowards(velocity.z, 0f, breakForce * Time.deltaTime);
        }

        rb.velocity = velocity;

    }
}
