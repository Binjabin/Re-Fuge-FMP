using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoSliders : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject recourceObject;
    [SerializeField] GameObject costObject;
    [SerializeField] GameObject enemyObject;
    [SerializeField] GameObject invObject;
    [SerializeField] Slider waterSlider;
    [SerializeField] Slider foodSlider;
    [SerializeField] Slider energySlider;

    [SerializeField] Slider foodCostSlider;
    [SerializeField] Slider waterCostSlider;

    [SerializeField] Slider currentWaterSlider;
    [SerializeField] Slider currentFoodSlider;
    [SerializeField] Slider currentEnergySlider;

    [SerializeField] Slider enemySlider;
    float maxEnemies = 8f;
    public MapNode currentSelectedNode;
    [SerializeField] float uncertaintySmoothSpeed;

    public float foodPerDist;
    public float waterPerDist;

    float enemyTweenDir;
    float foodTweenDir;
    float waterTweenDir;
    float currentEnemyTweenValue;
    float currentFoodTweenValue;
    float currentWaterTweenValue;

    void Start()
    {
        recourceObject.SetActive(false);
        costObject.SetActive(false);
        enemyObject.SetActive(false);
        currentEnemyTweenValue = 0f;
        currentFoodTweenValue = 0f;
        currentWaterTweenValue = 0f;
        enemyTweenDir = 1;
        foodTweenDir = 1;
        waterTweenDir = 1;


    }

    // Update is called once per frame
    void Update()
    {
        currentEnergySlider.value = PlayerStats.energy/100f;
        currentFoodSlider.value = PlayerStats.food / 100f;
        currentWaterSlider.value = PlayerStats.water / 100f;
        if (FindObjectOfType<DialogueManager>().dialogueIsPlaying)
        {
            invObject.SetActive(false);
            recourceObject.SetActive(false);
            costObject.SetActive(false);
            enemyObject.SetActive(false);
        }
        else
        {
            invObject.SetActive(true);
            if (currentSelectedNode == null)
            {
                recourceObject.SetActive(false);
                costObject.SetActive(false);
                enemyObject.SetActive(false);
            }
            else
            {
                var asteroidWeights = currentSelectedNode.asteroidWeights;
                recourceObject.SetActive(true);
                costObject.SetActive(true);
                enemyObject.SetActive(true);
                invObject.SetActive(true);
                currentSelectedNode.DetermineCost();
                foreach (AsteroidWeights asteroidWeight in asteroidWeights)
                {
                    if (asteroidWeight.type == ItemType.Energy)
                    {
                        energySlider.value = asteroidWeight.weight;
                    }
                    if (asteroidWeight.type == ItemType.Food)
                    {
                        foodSlider.value = asteroidWeight.weight;
                    }
                    if (asteroidWeight.type == ItemType.Water)
                    {
                        waterSlider.value = asteroidWeight.weight;
                    }
                }


                DoEnemyTween();
                DoFoodTween();
                DoWaterTween();
            }
        }



    }
    void DoEnemyTween()
    {
        float maxEnemyTween = currentSelectedNode.blueprint.maxHeavyEnemyCount + currentSelectedNode.blueprint.maxLightEnemyCount;
        float minEnemyTween = currentSelectedNode.blueprint.minHeavyEnemyCount + currentSelectedNode.blueprint.minLightEnemyCount;
        currentEnemyTweenValue += enemyTweenDir * Time.deltaTime * uncertaintySmoothSpeed * (maxEnemyTween - minEnemyTween);
        if (currentEnemyTweenValue > maxEnemyTween)
        {
            currentEnemyTweenValue = maxEnemyTween;
            enemyTweenDir = -1;
        }
        else if (currentEnemyTweenValue < minEnemyTween)
        {
            currentEnemyTweenValue = minEnemyTween;
            enemyTweenDir = 1;
        }
        enemySlider.value = currentEnemyTweenValue / maxEnemies;
    }
    void DoFoodTween()
    {
        currentFoodTweenValue += foodTweenDir * Time.deltaTime * uncertaintySmoothSpeed * (currentSelectedNode.currentMaxFoodCost - currentSelectedNode.currentMinFoodCost);
        if (currentFoodTweenValue > currentSelectedNode.currentMaxFoodCost)
        {
            currentFoodTweenValue = currentSelectedNode.currentMaxFoodCost;
            foodTweenDir = -1;
        }
        else if (currentFoodTweenValue < currentSelectedNode.currentMinFoodCost)
        {
            currentFoodTweenValue = currentSelectedNode.currentMinFoodCost;
            foodTweenDir = 1;
        }
        foodCostSlider.value = currentFoodTweenValue / 100f;
    }

    void DoWaterTween()
    {
        currentWaterTweenValue += waterTweenDir * Time.deltaTime * uncertaintySmoothSpeed * (currentSelectedNode.currentMaxWaterCost - currentSelectedNode.currentMinWaterCost);
        if (currentWaterTweenValue > currentSelectedNode.currentMaxWaterCost)
        {
            currentWaterTweenValue = currentSelectedNode.currentMaxWaterCost;
            waterTweenDir = -1;
        }
        else if (currentWaterTweenValue < currentSelectedNode.currentMinWaterCost)
        {
            currentWaterTweenValue = currentSelectedNode.currentMinWaterCost;
            waterTweenDir = 1;
        }
        waterCostSlider.value = currentWaterTweenValue / 100f;
    }
}
