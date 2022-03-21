using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    Material standardMat;
    Material stealthMat;
    float fadeSpeed = 2.0f;
    Renderer rend;
    [SerializeField] float fadeFactor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fadeFactor = 0;
        standardMat = rend.material;
        stealthMat = Resources.Load("Material/Stealth", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.Lerp (standardMat, stealthMat, fadeFactor);
    }
}
