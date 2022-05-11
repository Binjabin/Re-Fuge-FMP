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
    [ColorUsage(true, true), SerializeField] Color waterPickupColor;
    [ColorUsage(true, true), SerializeField] Color energyPickupColor;
    [ColorUsage(true, true), SerializeField] Color foodPickupColor;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody>();
        inv = FindObjectOfType<Inventory>();
        if (type == ItemType.Energy)
        {
            GetComponent<MeshRenderer>().material.SetColor("_EmmissionColor", energyPickupColor);
        }
        if (type == ItemType.Food)
        {
            GetComponent<MeshRenderer>().material.SetColor("_EmmissionColor", foodPickupColor);
        }
        if (type == ItemType.Water)
        {
            GetComponent<MeshRenderer>().material.SetColor("_EmmissionColor", waterPickupColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = -(transform.position - player.transform.position).normalized;
        float distance = (transform.position - player.transform.position).magnitude;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
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
