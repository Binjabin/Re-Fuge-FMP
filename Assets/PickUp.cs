using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject player;
    [SerializeField] float pickupRange;
    [SerializeField] float speed;
    public ItemType type;
    Rigidbody rb;
    Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody>();
        inv = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = -(transform.position - player.transform.position).normalized;
        float distance = (transform.position - player.transform.position).magnitude;
        Debug.Log(distance);
        if(distance < pickupRange)
        {
            float force = (pickupRange - distance) * speed * Time.deltaTime;
            rb.AddForce(force * dir);
        }
        if(distance < 1f)
        {
            Destroy(gameObject);
            inv.AddItem(type);
        }
    }

}
