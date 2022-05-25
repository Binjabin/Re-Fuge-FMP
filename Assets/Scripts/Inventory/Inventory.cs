using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public bool invOpen;
    public List<GameObject> startingItems;
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
    public List<GameObject> currentItems;
    public List<GameObject> allItems;
    public bool inShop;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject waterWarning;
    [SerializeField] GameObject foodWarning;
    [SerializeField] GameObject energyWarning;
    bool absorbing;
    [SerializeField] GameObject tutorial;
    public bool showTutorial;
    public bool hasID;
    public bool helpedRefugee;
    public bool pickedUpRefugee;
    public float resourceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        allItems = ItemPrefabs.prefabList;
        if (PlayerStats.init == false)
        {
            Debug.Log(PlayerStats.energy);
            currentEnergy = PlayerStats.energy;
            Debug.Log(currentEnergy);
            currentFood = PlayerStats.food;
            currentWater = PlayerStats.water;
            hasID = PlayerStats.hasID;
            resourceMultiplier = PlayerStats.resourceMultiplier;
            if(PlayerStats.items != null)
            {
                foreach (GameObject item in PlayerStats.items)
                {
                    GameObject newItem = Instantiate(item);
                    newItem.transform.parent = inventory.transform;
                    newItem.GetComponent<Item>().prefab = item;
                    currentItems.Add(newItem);
                    newItem.GetComponent<Item>().startSlot = null;
                }
            }
            
        
        }
        energy = FindObjectOfType<EnergyBar>();
        invOpen = false;
        energy.maxEnergy = maxEnergy;


    }

    // Update is called once per frame
    void Update()
    {
        
        if(inShop)
        {
            invOpen = true;
            if (Input.GetKeyDown(KeyCode.I))
            {
                inShop = false;
                invOpen = false;
            }
        }
        else if(FindObjectOfType<DialogueManager>().dialogueIsPlaying)
        {
            invOpen = false;
            FindObjectOfType<SceneStartDialogueTrigger>().checkedInventory = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(invOpen)
                {
                    if (!absorbing)
                    {
                        invOpen = false;
                    }
                }
                else
                {
                    invOpen = true;
                    FindObjectOfType<SceneStartDialogueTrigger>().checkedInventory = true;
                }
            }

        }
        if (currentEnergy / maxEnergy < 0.2f)
        {
            energyWarning.SetActive(true);
        }
        else
        {
            energyWarning.SetActive(false);
        }
        if (currentFood / maxFood < 0.2f)
        {
            foodWarning.SetActive(true);
        }
        else
        {
            foodWarning.SetActive(false);
        }
        if (currentWater / maxWater < 0.2f)
        {
            waterWarning.SetActive(true);
        }
        else
        {
            waterWarning.SetActive(false);
        }
        inventory.SetActive(invOpen);
        shop.SetActive(inShop);
        energy.currentEnergy = currentEnergy;
        energySlider.value = currentEnergy / maxEnergy;
        waterSlider.value = currentWater / maxWater;
        foodSlider.value = currentFood / maxFood;
        if(invOpen)
        {
            absorbing = false;
            foreach(GameObject item in currentItems)
            {
                if(item.GetComponent<Item>().absorbing == true)
                {
                    absorbing = true;
                }
            }
        }
    }
    public void TopUp(ItemType type, float value)
    {
        switch (type)
        {
            case ItemType.Energy:
                currentEnergy += value;
                currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
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

    public void TradeOutput(ItemType type, InventorySlot slot)
    {
        GameObject tradedItem = AddItem(type);
        tradedItem.GetComponent<Item>().startSlot = slot;
    }
    public GameObject AddItem(ItemType type)
    {
        List<GameObject> objectsOfType = new List<GameObject>();
        foreach(GameObject prefab in allItems)
        {
            if(prefab.GetComponent<Item>().itemType == type)
            {
                objectsOfType.Add(prefab);
            }
        }
        int randIndex = Random.Range(0, objectsOfType.Count);
        GameObject newItem = Instantiate(objectsOfType[randIndex]);
        newItem.transform.parent = inventory.transform;
        newItem.GetComponent<Item>().prefab = objectsOfType[randIndex];
        currentItems.Add(newItem);
        return newItem;
    }
}
