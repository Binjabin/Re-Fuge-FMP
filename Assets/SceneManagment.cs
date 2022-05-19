using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class SceneManagment : MonoBehaviour
{
    [SerializeField] Animator outAnimation;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource rumbleAudio;


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
        
        float elapsedTime = 0f;
        rumbleAudio.Play();
        rumbleAudio.volume = 0.3f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(0.2f, 0f, (elapsedTime / 5f));
            rumbleAudio.volume = Mathf.Lerp(.5f, 1f, (elapsedTime / 5f));
            yield return null;
        }
        outAnimation.SetTrigger("Out");
        while (elapsedTime < 4f)
        {
            elapsedTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(0.2f, 0f, (elapsedTime / 4f));
            rumbleAudio.volume = Mathf.Lerp(.5f, 1f, (elapsedTime / 4f));
            yield return null;
        }
    }

    IEnumerator ReturnToMapAnimation()
    {
        outAnimation.SetTrigger("Out");
        float elapsedTime = 0f;
        rumbleAudio.Play();
        rumbleAudio.volume = 0.3f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(0.2f, 0f, (elapsedTime / 5f));
            rumbleAudio.volume = Mathf.Lerp(.5f, 1f, (elapsedTime / 5f));
            yield return null;
        }
        outAnimation.SetTrigger("Out");
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(0.2f, 0f, (elapsedTime / 5f));
            rumbleAudio.volume = Mathf.Lerp(.5f, 1f, (elapsedTime / 5f));
            yield return null;
        }
        SceneManager.LoadScene("Map");
    }

    void SavePlayerStats()
    {
        PlayerStats.energy = FindObjectOfType<Inventory>().currentEnergy;
        PlayerStats.food = FindObjectOfType<Inventory>().currentFood;
        PlayerStats.water = FindObjectOfType<Inventory>().currentWater;
        PlayerStats.health = FindObjectOfType<HealthBar>().currentHealth;
        PlayerStats.shield = FindObjectOfType<HealthBar>().currentShield;
        List<Item> itemScriptList = FindObjectsOfType<Item>(true).ToList();
        PlayerStats.items = new List<GameObject>();
        foreach(Item item in itemScriptList)
        {
            PlayerStats.items.Add(item.GetComponent<Item>().prefab);
        }
        PlayerStats.levelPassed += 1;
        PlayerStats.SaveStats();
        

    }
}
