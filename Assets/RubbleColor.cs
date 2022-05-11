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

    public void SetColor(Color color)
    {
        currentRubbleColor = color;
    }
    private void Update()
    {
        rubbleMat = part.sharedMaterial;
        part.sharedMaterial.SetColor("_Color", currentRubbleColor);

    }
}
