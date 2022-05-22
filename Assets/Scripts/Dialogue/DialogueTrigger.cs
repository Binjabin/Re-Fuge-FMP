using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextAsset defaultJSON;
    [SerializeField] TextAsset firstJSON;
    [SerializeField] TextAsset defaultJSON2;
    [SerializeField] bool isMerchant;
    [SerializeField] bool isMysterious;
    bool alreadySpoken;

    bool inCollider;

    void Start()
    {
        inCollider = false;
        alreadySpoken = false;
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
                if(isMerchant)
                {
                    if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                    {
                        FindObjectOfType<CinemachineTargetGroup>().m_Targets = new CinemachineTargetGroup.Target[0];
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(FindObjectOfType<PlayerMovement>().gameObject.transform, 2f, 15f);
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(transform, 2f, 10f);
                        
                        FindObjectOfType<PlayerMovement>().EnterDialogue(gameObject);
                        if(GetComponent<Rigidbody>() != null)
                        {
                            GetComponent<Rigidbody>().drag = 3.0f;
                        }
                        if(!alreadySpoken)
                        {
                            DialogueManager.GetInstance().EnterDialogue(firstJSON);
                            alreadySpoken = true;
                        }
                        else
                        {
                            DialogueManager.GetInstance().EnterDialogue(defaultJSON);
                        }
                    }
                }
                else if(isMysterious)
                {
                    if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                    {
                        FindObjectOfType<CinemachineTargetGroup>().m_Targets = new CinemachineTargetGroup.Target[0];
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(FindObjectOfType<PlayerMovement>().gameObject.transform, 2f, 15f);
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(transform, 2f, 10f);
                        
                        FindObjectOfType<PlayerMovement>().EnterDialogue(gameObject);
                        if(GetComponent<Rigidbody>() != null)
                        {
                            GetComponent<Rigidbody>().drag = 3.0f;
                        }
                        if(!alreadySpoken)
                        {
                            DialogueManager.GetInstance().EnterDialogue(firstJSON);
                            alreadySpoken = true;
                        }
                        else if(FindObjectOfType<Inventory>().hasID)
                        {
                            DialogueManager.GetInstance().EnterDialogue(defaultJSON2);
                        }
                        else
                        {
                            DialogueManager.GetInstance().EnterDialogue(defaultJSON);
                        }
                    }
                }
                else
                {
                    if (!FindObjectOfType<DialogueManager>().dialogueIsPlaying)
                    {
                        FindObjectOfType<CinemachineTargetGroup>().m_Targets = new CinemachineTargetGroup.Target[0];
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(FindObjectOfType<PlayerMovement>().gameObject.transform, 2f, 15f);
                        FindObjectOfType<CinemachineTargetGroup>().AddMember(transform, 2f, 10f);
                        
                        FindObjectOfType<PlayerMovement>().EnterDialogue(gameObject);
                        if(GetComponent<Rigidbody>() != null)
                        {
                            GetComponent<Rigidbody>().drag = 3.0f;
                        }
                        if(!alreadySpoken)
                        {
                            DialogueManager.GetInstance().EnterDialogue(firstJSON);
                            alreadySpoken = true;
                        }
                        else if(FindObjectOfType<Inventory>().helpedRefugee)
                        {
                            DialogueManager.GetInstance().EnterDialogue(defaultJSON2);
                        }
                        else
                        {
                            DialogueManager.GetInstance().EnterDialogue(defaultJSON);
                        }
                    }
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
