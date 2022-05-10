using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelBounds : MonoBehaviour
{
    List<Rigidbody> rbs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rbs = FindObjectsOfType<Rigidbody>().ToList();
        foreach(Rigidbody rb in rbs)
        {
            
        }
    }
}
