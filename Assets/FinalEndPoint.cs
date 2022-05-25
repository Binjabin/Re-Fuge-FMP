using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEndPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject nextLevelPrompt;
    void Start()
    {
        nextLevelPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerMovement>().Win();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Win());
        }
            
        
    }
}
