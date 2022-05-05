using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    bool open;
    [SerializeField] GameObject inventory;
    [SerializeField] Slider energySlider;
    public float currentEnergy;
    [SerializeField] float maxEnergy;
    [SerializeField] Slider foodSlider;
    [SerializeField] float maxFood;
    public float currentFood;
    [SerializeField] Slider waterSlider;
    [SerializeField] float maxWater;
    public float currentWater;
    EnergyBar energy;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStats.init == false)
        {
            currentEnergy = PlayerStats.energy;
            currentFood = PlayerStats.food;
            currentWater = PlayerStats.water;
        }
        else
        {
            currentEnergy = maxEnergy/2f;
            currentFood = maxFood/2f;
            currentWater = maxWater/2f;
        }
        energy = FindObjectOfType<EnergyBar>();
        open = false;
        energy.maxEnergy = maxEnergy;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            open = !open;
        }
        inventory.SetActive(open);

        energy.currentEnergy = currentEnergy;
        energySlider.value = currentEnergy/maxEnergy;
        waterSlider.value = currentWater/maxWater;
        foodSlider.value = currentFood/maxFood;
    }
    public void TopUp(ItemType type, float value)
    {
        switch(type)
        {
            case ItemType.Energy:
                currentEnergy += value;
                currentEnergy += Mathf.Clamp(currentEnergy, 0f, maxEnergy);
                energy.currentEnergy = currentEnergy;
                return;
            case ItemType.Food:
                currentFood += value;
                currentFood = Mathf.Clamp(currentFood, 0f, maxFood);
                return;
            case ItemType.Water:
                currentWater += value;
                currentWater = Mathf.Clamp(currentWater, 0f, maxWater);
                return;
        }
            
    }
    public void ReduceEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }
}
