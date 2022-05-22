using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerChecker : MonoBehaviour
{
    int triggerNumber;
    [SerializeField] GameObject dialogePrompt;
    
    // Start is called before the first frame update
    void Start()
    {
        triggerNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerNumber < 1f || FindObjectOfType<DialogueManager>().dialogueIsPlaying || FindObjectOfType<Inventory>().invOpen)
        {
            dialogePrompt.SetActive(false);
        }
        else
        {
            dialogePrompt.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DialogueTrigger>() != null)
        {
            triggerNumber += 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DialogueTrigger>() != null)
        {
            triggerNumber -= 1;
        }
    }
}
