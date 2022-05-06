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

    bool inDialogue;

    TrailRenderer[] trail;
    float[] standardTrailLength = { 0f, 0f, 0f };
    EnergyBar energy;

    [SerializeField] float energyPerSecondThrusting;
    [SerializeField] float energyPerSecondBoosting;


    GameObject dialogueFocus;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        energy = gameObject.GetComponent<EnergyBar>();
        inDialogue = false;
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
        if(!inDialogue && !FindObjectOfType<Inventory>().invOpen)
        {
            ProcessInput();
        }
        else
        {
            Vector3 targetDirection = dialogueFocus.transform.position - transform.position;
            targetDirection.y = 0f;
            float rotationStep = 3f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, rotationStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection) * Quaternion.Euler(0f, -90f, 0f);
        }
        
    }

    void GetInput()
    {
        thrusting = Input.GetKey(KeyCode.W);
        breaking = Input.GetKey(KeyCode.S) || inDialogue;
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
            if(!(energy.currentEnergy > 0))
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, 0f, ref velocityref, 0.1f);
            }
            else if (thrusting || boosting)
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, standardTrailLength[i], ref velocityref, 0.1f);
            }
            else
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, 0f, ref velocityref, 0.1f);
            }
        }
    }
    void ProcessInput()
    {
        if (thrusting)
        {
            FindObjectOfType<Inventory>().ReduceEnergy(energyPerSecondThrusting * Time.deltaTime);
            if(energy.currentEnergy > 0f)
            {
                rb.AddForce(transform.right * thrustSpeed);
            }
            
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
            FindObjectOfType<Inventory>().ReduceEnergy(energyPerSecondBoosting * Time.deltaTime);
            if (energy.currentEnergy > 0f)
            {
                rb.AddForce(transform.right * thrustSpeed * 5);
            }
                
            
        }

        if (turnDirection != 0f)
        {
            rb.AddTorque(transform.up * rotationSpeed * -turnDirection);
        }

    }

    public void EnterDialogue(GameObject focus)
    {
        inDialogue = true;
        rb.drag = 3f;
        dialogueFocus = focus;
        FindObjectOfType<CameraStateController>().ToMerchant();
    }
    public void ExitDialogue()
    {
        inDialogue = false;
        FindObjectOfType<CameraStateController>().ToPlayer();
    }
}
