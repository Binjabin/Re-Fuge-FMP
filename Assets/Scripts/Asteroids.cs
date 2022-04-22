using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 spin = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        Vector3 movement = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        Vector3 axisScale = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        float generalScale = Random.Range(0.1f, 1.2f);
        Vector3 scale = new Vector3(transform.localScale.x * (axisScale.x + generalScale), transform.localScale.y * (axisScale.y + generalScale), transform.localScale.z * (axisScale.z + generalScale));
        rb.angularVelocity = spin;
        rb.velocity = movement;
        transform.localScale = scale;
        rb.mass = generalScale * 3;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
