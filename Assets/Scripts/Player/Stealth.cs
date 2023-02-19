using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    [SerializeField, ColorUsage(true, true)] Color stealthColor;
    [ColorUsage(true, true)] Color standardColor;
    [SerializeField] float fadeFactor;
    Renderer rend;
    [SerializeField] float fadeTime;
    Renderer[] childRenderers;
    List<Material> childMaterials = new List<Material>();
    List<Color> childColors = new List<Color>();
    int childIndex;
    public bool stealthOn;
    float elapsedTime;
    bool isFading;
    [SerializeField] float energyPerSecondStealth;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fadeFactor = 0;
        stealthOn = false;
        
        if(rend != null)
        {
            standardColor = rend.material.color;
        }
        childRenderers = GetComponentsInChildren<Renderer>();
        if(childRenderers.Length > 0)
        {
            foreach(Renderer ren in childRenderers)
            {
                childMaterials.AddRange(ren.materials);
            }
        }
        if (childMaterials.Count > 0)
        {
            childIndex = 0;
            foreach(Material mat in childMaterials) 
            {
                childIndex++;
                childColors.Add(mat.color);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (gameObject.GetComponent<EnergyBar>().currentEnergy > 0f)
            {
                if(!isFading)
                {
                    if (stealthOn == false)
                    {
                        stealthOn = true;
                        StartCoroutine(ToggleStealth(true));
                    }
                }
                
            }
            else    
            {
                stealthOn = false;
            }
            
        }
        else
        {
            if(stealthOn == true)
            {
                if(!isFading)
                {
                    stealthOn = false;
                    StartCoroutine(ToggleStealth(false));
                }
                
            }
        }

        float currentShadows = Mathf.Lerp(1f, 0f, fadeFactor);
        if(rend != null)
        {
            rend.material.color = Color.Lerp(standardColor, stealthColor, fadeFactor);
            rend.material.SetFloat("_ShadowIntensity", currentShadows);
        }
        if (childRenderers.Length > 0)
        {
            childIndex = 0;
            foreach (Material mat in childMaterials)
            {
                mat.color = Color.Lerp(childColors[childIndex], stealthColor, fadeFactor);
                mat.SetFloat("_ShadowIntensity", currentShadows);
                childIndex++;
            }
        }
        if(stealthOn)
        {
            
            if(FindObjectOfType<Inventory>() != null)
            {
                FindObjectOfType<Inventory>().ReduceEnergy(energyPerSecondStealth * Time.deltaTime);
            }
            if (gameObject.GetComponent<EnergyBar>().currentEnergy > 0f)
            {

            }
            else
            {
                StartCoroutine(ToggleStealth(false));
            }
        }
    }
    IEnumerator ToggleStealth(bool stealthOn)
    {
        isFading = true;
        float targetStealthValue = stealthOn ? 1f : 0f;
        float startStealthValue = fadeFactor;
        elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fadeFactor = Mathf.Lerp(startStealthValue, targetStealthValue, elapsedTime/fadeTime);
            yield return null;
        }
        isFading = false;
        yield return null;
    }

}
