using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefugeeShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 spin = new Vector3(0f, Random.Range(-0.2f, 0.2f), 0f);
        Vector3 movement = new Vector3(Random.Range(-.1f, .1f), 0f, Random.Range(-.1f, .1f));
        rb.angularVelocity = spin;
        rb.velocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
