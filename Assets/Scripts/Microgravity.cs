using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgravity : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            if(rigidbody != rb)
            {
                Attract(rigidbody);
            }

        }
    }

    void Attract(Rigidbody rbAttract)
    {
        Vector3 dir = rb.position - rbAttract.position;
        float dist = dir.magnitude;
        float force = (rb.mass * rbAttract.mass) / Mathf.Pow(dist, 2);
        force = force / 10f;
        Vector3 forceToApply = force * dir.normalized;

        rbAttract.AddForce(forceToApply);
    }
}
