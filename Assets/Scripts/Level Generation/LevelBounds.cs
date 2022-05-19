using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelBounds : MonoBehaviour
{
    List<Rigidbody> rbs;
    LevelGenerator level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rbs = FindObjectsOfType<Rigidbody>().ToList();
        level = FindObjectOfType<LevelGenerator>();
        foreach(Rigidbody rb in rbs)
        {
            if(rb.transform.position.x > level.maxDistance)
            {
                Vector3 force = new Vector3(-(rb.transform.position.x - level.maxDistance)*Time.deltaTime*100, 0f, 0f);
                rb.AddForce(force);
            }
            if(rb.transform.position.x < -level.maxDistance)
            {
                Vector3 force = new Vector3(-(rb.transform.position.x + level.maxDistance)*Time.deltaTime*100, 0f, 0f);
                rb.AddForce(force);
            }
            if(rb.transform.position.z > level.maxDistance)
            {
                Vector3 force = new Vector3(0f, 0f, -(rb.transform.position.z - level.maxDistance)*Time.deltaTime*100);
                rb.AddForce(force);
            }
            if(rb.transform.position.z < -level.maxDistance)
            {
                Vector3 force = new Vector3(0f, 0f, -(rb.transform.position.z + level.maxDistance)*Time.deltaTime*100);
                rb.AddForce(force);
            }
        }
    }
}
