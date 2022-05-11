using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    float timeInCollider;
    [SerializeField] float timeToWarp;
    bool playerInWarpZone ;
    [SerializeField] GameObject nextLevelPrompt;

    private void Start()
    {
        playerInWarpZone = false;
        nextLevelPrompt = GameObject.FindGameObjectWithTag("NextLevelPrompt");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInWarpZone = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInWarpZone = false;
        }
    }
    
    
    private void Update()
    {
        if(playerInWarpZone)
        {
            nextLevelPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                FindObjectOfType<SceneManagment>().ReturnToMap();
            }
        }
        else
        {
            nextLevelPrompt.SetActive(false);
        }
    }

}
