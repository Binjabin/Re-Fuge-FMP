using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{   
    float sizeThreshold;
    [SerializeField] bool breakAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 sizeV3 = new Vector3(50f, 50f, 50f);
        sizeThreshold = sizeV3.magnitude;
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 spin = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        Vector3 movement = new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        Vector3 axisScale = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        float generalScale = Random.Range(0.4f, 1.2f);
        Vector3 scale = new Vector3(transform.localScale.x * (axisScale.x + generalScale), transform.localScale.y * (axisScale.y + generalScale), transform.localScale.z * (axisScale.z + generalScale));
        rb.angularVelocity = spin;
        rb.velocity = movement;
        transform.localScale = scale;
        rb.mass = generalScale * 3;
    }
    // Update is called once per frame
    void Update()
    {
        if(breakAsteroid)
        {
            breakAsteroid = false;

            if(transform.localScale.magnitude < sizeThreshold)
            {
                Destroy(gameObject);
                Debug.Log("got too small");
            }
            else
            {
                GameObject newAsteroid;
                newAsteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
                newAsteroid.transform.localScale = transform.localScale / 2f;
                newAsteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
                newAsteroid.transform.localScale = transform.localScale / 2f;
                Destroy(gameObject);
            }
            
        }
    }
}
