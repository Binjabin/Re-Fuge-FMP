using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleColor : MonoBehaviour
{
    [SerializeField] Color currentRubbleColor;
    [SerializeField] Material rubbleMat;
    ParticleSystemRenderer part;

    private void Start()
    {
        part = GetComponent<ParticleSystemRenderer>();
    }

    public void SetColor(Material color)
    {
        rubbleMat = color;
        part.sharedMaterial = rubbleMat;
    }
    private void Update()
    {
        

    }
}
