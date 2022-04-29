using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    Transform target;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    Rigidbody rb;
    [SerializeField] GameObject missileExplosion;
    [SerializeField] float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0f;
        Quaternion targetRotatation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotatation, rotSpeed);
        rb.AddForce(lookDirection.normalized * speed);
    }


    private void OnCollisionEnter(Collision other)
    {
        Instantiate(missileExplosion, other.contacts[0].point, Quaternion.identity);
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<HealthBar>().ShieldOnlyDamage(damage);
        }
        
        Destroy(gameObject);

    }

}
