using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Camera alignCamera;
    [SerializeField] bool isRadar = false;

    // Start is called before the first frame update
    void Start()
    {
        if(isRadar)
        {
            alignCamera = FindObjectOfType<RadarCamera>().gameObject.GetComponent<Camera>();
        }
        else
        {
            alignCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = alignCamera.transform.forward;
    } 
}
