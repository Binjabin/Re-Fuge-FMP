using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    [SerializeField, ColorUsage(true, true)] Color stealthColor;
    [ColorUsage(true, true)] Color standardColor;
    float fadeSpeed = 2.0f;
    Renderer rend;
    [SerializeField] float fadeFactor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fadeFactor = 0;
        standardColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Color currentColor = Color.Lerp(standardColor, stealthColor, fadeFactor);
        float currentShadows = Mathf.Lerp(1f, 0f, fadeFactor);
        rend.material.color = currentColor;
        rend.material.SetFloat("_ShadowIntensity", currentShadows);
    }
}
