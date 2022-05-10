using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{   
    float sizeThreshold;
    [SerializeField] bool breakAsteroid;
    [SerializeField] Vector3 shrinkSpeed;
    [SerializeField] Vector3 minSize;
    float timeSinceLazerHit;
    public ItemType itemType;
    [SerializeField] GameObject dropPrefab;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLazerHit = 0f;
        sizeThreshold = minSize.magnitude;
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
            if(timeSinceLazerHit > 1f)
            {
                breakAsteroid = false;
            }
            transform.localScale = transform.localScale - shrinkSpeed * Time.deltaTime;
            if(transform.localScale.magnitude < sizeThreshold)
            {
                GameObject spawnedItem = Instantiate(dropPrefab, transform);
                spawnedItem.GetComponent<PickUp>().type = itemType;
                Destroy(gameObject);
                
            }
        }
        timeSinceLazerHit += Time.deltaTime;
    }
    void OnParticleCollision(GameObject other)
    {
        breakAsteroid = true;
        timeSinceLazerHit = 0f;
    }
    
}
