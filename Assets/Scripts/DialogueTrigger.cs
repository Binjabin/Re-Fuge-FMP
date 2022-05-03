using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    bool playerInRange;
    float timeInCollider;
    [SerializeField] float timeRequired;
    [SerializeField] TextAsset inkJSON;
    bool triggeredDialogue;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            triggeredDialogue = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timeInCollider += Time.deltaTime;
        if (timeInCollider > timeRequired)
        {
            if(!triggeredDialogue)
            {
                triggeredDialogue = true;
                DialogueManager.GetInstance().EnterDialogue(inkJSON);
                FindObjectOfType<PlayerMovement>().EnterDialogue(gameObject);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
