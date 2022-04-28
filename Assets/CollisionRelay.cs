using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRelay : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<HealthBar>().CollisionDetected(collision);
    }

}
