using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagment : MonoBehaviour
{
    [SerializeField] Animator outAnimation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LeaveScene()
    {

        StartCoroutine(LeaveSceneAnimation());
    }
    public void ReturnToMap()
    {

        StartCoroutine(ReturnToMapAnimation());
        SavePlayerStats();
    }

    IEnumerator LeaveSceneAnimation()
    {
        outAnimation.SetTrigger("Out");
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator ReturnToMapAnimation()
    {
        outAnimation.SetTrigger("Out");
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Map");
    }

    void SavePlayerStats()
    {
        PlayerStats.energy = FindObjectOfType<Inventory>().currentEnergy;
        PlayerStats.food = FindObjectOfType<Inventory>().currentFood;
        PlayerStats.water = FindObjectOfType<Inventory>().currentWater;
        PlayerStats.health = FindObjectOfType<HealthBar>().currentHealth;
        PlayerStats.shield = FindObjectOfType<HealthBar>().currentShield;

    }
    void LoadPlayerStats()
    {

    }
}
