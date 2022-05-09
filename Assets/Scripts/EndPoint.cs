using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    float timeInCollider;
    [SerializeField] float timeToWarp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timeInCollider = 0f;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timeInCollider += Time.deltaTime;
            if (timeInCollider > timeToWarp)
            {
                FindObjectOfType<SceneManagment>().ReturnToMap();
            }
        }
        
    }
    
}
