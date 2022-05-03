using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float maxEnergy;
    [SerializeField] Slider energyBar;
    public float currentEnergy;
    
    void Start()
    {
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.value = currentEnergy / maxEnergy;
        
    }

    public void ReduceEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }

    
}
