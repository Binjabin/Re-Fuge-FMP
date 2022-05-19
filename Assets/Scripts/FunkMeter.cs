using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunkMeter : MonoBehaviour
{
    [SerializeField] float chanceOfFunk;
    // Start is called before the first frame update
    void Start()
    {
        Random.Range(0f, 10000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
