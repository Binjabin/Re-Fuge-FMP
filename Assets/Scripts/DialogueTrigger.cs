using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextAsset inkJSON;

    bool inCollider;

    void Start()
    {
        inCollider = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inCollider = true;
            
        }
    }

    private void Update()
    {
        if(inCollider)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                {
                    FindObjectOfType<CinemachineTargetGroup>().m_Targets = new CinemachineTargetGroup.Target[0];
                    FindObjectOfType<CinemachineTargetGroup>().AddMember(FindObjectOfType<PlayerMovement>().gameObject.transform, 2f, 15f);
                    FindObjectOfType<CinemachineTargetGroup>().AddMember(transform, 2f, 10f);
                    DialogueManager.GetInstance().EnterDialogue(inkJSON);
                    FindObjectOfType<PlayerMovement>().EnterDialogue(gameObject);
                }

            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inCollider = false;
        }
        
    }
}
